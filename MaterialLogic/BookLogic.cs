using MaterialData.exceptions;
using MaterialData.interfaces;
using MaterialData.models;
using MaterialData.repository;
using System.Collections.Generic;
using System.Linq;

namespace MaterialLogic
{
    public class BookLogic : BaseLogic<book>, IMaterialLogic
    {
        public BookLogic(BaseRepository<book> Repo) : base(Repo)
        {
        }

        public override void IsValid(book item)
        {
            List<string> errList = new List<string>();
            if (string.IsNullOrEmpty(item.title))
                errList.Add("𝗧𝗶𝘁𝗲𝗹");

            if (string.IsNullOrEmpty(item.isbn))
                errList.Add("𝗜𝗦𝗕𝗡");

            if (item.quantity == null)
                errList.Add("𝗔𝗻𝘇𝗮𝗵𝗹");

            var existingIsbn = Repo.Entities.Set<book>().FirstOrDefault(x => x.title != item.title && x.isbn == item.isbn);
            if (existingIsbn != null && existingIsbn.id != item.id)
                throw new DuplicateEntryException($"Buch mit der selben ISBN \"{item.isbn}\" unter dem Titel \"{existingIsbn.title}\" bereits vorhanden!");

            if (errList.Count > 0)
            {
                string err = BuildErrorMessage(errList);
                throw new InvalidInputException(err);
            }

            if (item.quantity < 1)
                throw new InvalidInputException("Anzahl darf nicht kleiner als 1 sein!");
        }

        public override book SetLocation(book item)
        {
            bool done = false;
            if (item.location_id == null && item.person_id == null)
                item.location_id = Repo.defaultLocation;

            if (item.location_id != null && item.person_id != null)
                throw new DuplicateEntryException("Bitte Buch einer Person ODER einem Standort zuweisen!");

            done = RebookItem(item);
            if (done)
                item = null;

            if (!done)
            {
                if (item.location_id != null && item.id > 0)
                    item = ReturnItem(item);

                if (item.person_id != null)
                    item = RentItem(item);
            }

            if (item != null)
            {
                if (item.id == 0)
                {
                    var existingBook = Repo.Entities.Set<book>().FirstOrDefault(x => x.title == item.title && x.isbn == item.isbn);
                    if (existingBook != null && existingBook.id != item.id && existingBook.location_id == item.location_id)
                    {
                        existingBook.quantity += item.quantity;
                        Repo.Entities.book.Update(existingBook);
                        Repo.Entities.SaveChanges();
                        return null;
                    }
                }
            }

            return item;
        }

        private book ReturnItem(book book)
        {
            book existingBook = Repo.Entities.book.FirstOrDefault(x => x.isbn == book.isbn && x.title == book.title);
            book sameBookInDb = Repo.Entities.book.FirstOrDefault(x => x.id == book.id);

            if (existingBook != null && existingBook.id == book.id)
                return book;

            if (sameBookInDb.quantity < book.quantity)
                throw new InvalidInputException("Es können nicht mehr Bücher zurückgegeben werden als ausgeliehen wurden!");

            if (existingBook != null && existingBook.location_id == book.location_id)
            {
                existingBook.quantity += book.quantity;
                Repo.Entities.book.Remove(sameBookInDb);
                Repo.Entities.SaveChanges();
            }
            else
            {
                book newBook = new book { id = 0, title = book.title, isbn = book.isbn, quantity = book.quantity, location_id = book.location_id }; //maybe now redundant
                Repo.Entities.book.Remove(sameBookInDb);
                Repo.Entities.book.Add(newBook);
                Repo.Entities.SaveChanges();
                return null;
            }

            return existingBook;
        }

        private book RentItem(book book)
        {
            book existingBook = Repo.Entities.book.FirstOrDefault(x => x.id == book.id);
            book alreadyBorrowedBook = Repo.Entities.book.FirstOrDefault(x => x.person_id == book.person_id);

            if (alreadyBorrowedBook != null)
            {
                Repo.GetRelation();
                throw new InvalidInputException($"Buch \"{book.title}\" wurde bereits an \"{alreadyBorrowedBook.person.name1} {alreadyBorrowedBook.person.name2}\" verliehen!");
            }

            if (existingBook != null)
            {
                if (existingBook.quantity < book.quantity)
                    throw new InvalidInputException($"Die Anzahl ausgeliehender Bücher darf nicht den Lagerbestand überschreiten!");
                else
                {
                    book.quantity = 1;
                    book.id = 0;
                    book.location_id = null;

                    Repo.Entities.book.Add(book);

                    existingBook.quantity -= book.quantity;
                }

                if (existingBook.quantity <= 0)
                {
                    Repo.Entities.book.Remove(existingBook);
                    existingBook = null;
                }

                Repo.Entities.SaveChanges();

                return existingBook;
            }
            else
                return book;
        }

        private bool RebookItem(book item)
        {
            if (item.id == 0)
                return false;
            if (item.person_id != null)
                return false;
            var existingBook = Repo.Entities.Set<book>().FirstOrDefault(x => x.id == item.id);
            var otherBook = Repo.Entities.Set<book>().FirstOrDefault(x => x.isbn == item.isbn && x.location_id == item.location_id);

            if (existingBook.quantity < item.quantity)
                throw new InvalidInputException($"Es können nicht {item.quantity} Bücher umgebucht werden, da nur {existingBook.quantity} lagernd sind!");

            if (otherBook == null)
            {
                existingBook.quantity -= item.quantity;

                if (existingBook.quantity <= 0)
                    Repo.Entities.book.Remove(existingBook);
                else
                    Repo.Entities.book.Update(existingBook);

                item.id = 0;
                Repo.Entities.book.Add(item);
                Repo.Entities.SaveChanges();
                return true;
            }

            if (otherBook != null && otherBook.id != item.id)
            {
                otherBook.quantity += item.quantity;
                existingBook.quantity -= item.quantity;

                if (existingBook.quantity <= 0)
                    Repo.Entities.book.Remove(existingBook);
                else
                    Repo.Entities.book.Update(existingBook);

                Repo.Entities.book.Update(otherBook);
                Repo.Entities.SaveChanges();
                return true;
            }
            return false;
        }
    }
}