using MaterialData.exceptions;
using MaterialData.models;
using MaterialData.models.material;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MaterialData.repositories
{
    public class SearchRepository
    {
        private DcvEntities Entities;

        public SearchRepository(DcvEntities entities)
        {
            Entities = entities;
        }

        public Dictionary<string, List<Material>> GetResult(search search)
        {
            string searchString = search.searchKeyWord;

            string[] test = { "" };
            if (searchString.Contains(" "))
            {
                test = searchString.Split(' ');
            }
            else
            {
                test[0] = searchString;
            }

            Dictionary<string, List<Material>> searchList = new Dictionary<string, List<Material>>();
            List<Material> notebooks = new List<Material>();
            List<Material> displays = new List<Material>();
            List<Material> furnitures = new List<Material>();
            List<Material> books = new List<Material>();
            List<Material> equipments = new List<Material>();



            notebooks.AddRange(Entities.notebook.Where(x => x.make == searchString || x.model == searchString || x.serial_number == searchString || x.person.name1 == searchString || x.person.name2 == searchString || x.classroom.room == searchString || x.classroom.addressloc.address.place == searchString || x.classroom.addressloc.address.street == searchString || x.classroom.addressloc.address.zip.ToString() == searchString || x.classroom.addressloc.address.country == searchString).AsNoTracking().ToList());
            displays.AddRange(Entities.display.Where(x => x.make == searchString || x.model == searchString || x.serial_number == searchString || x.classroom.room == searchString || x.classroom.addressloc.address.place == searchString || x.classroom.addressloc.address.street == searchString || x.classroom.addressloc.address.zip.ToString() == searchString || x.classroom.addressloc.address.country == searchString).AsNoTracking().ToList());
            furnitures.AddRange(Entities.furniture.Where(x => x.type == searchString || x.classroom.room == searchString || x.classroom.addressloc.address.place == searchString || x.classroom.addressloc.address.street == searchString || x.classroom.addressloc.address.zip.ToString() == searchString || x.classroom.addressloc.address.country == searchString).AsNoTracking().ToList());
            books.AddRange(Entities.book.Where(x => x.title == searchString || x.isbn == searchString || x.classroom.room == searchString || x.classroom.addressloc.address.place == searchString || x.classroom.addressloc.address.street == searchString || x.classroom.addressloc.address.zip.ToString() == searchString || x.classroom.addressloc.address.country == searchString).AsNoTracking().ToList());
            equipments.AddRange(Entities.equipment.Where(x => x.type == searchString || x.make == searchString || x.model == searchString || x.classroom.room == searchString || x.classroom.addressloc.address.place == searchString || x.classroom.addressloc.address.street == searchString || x.classroom.addressloc.address.zip.ToString() == searchString || x.classroom.addressloc.address.country == searchString).AsNoTracking().ToList());
            if (test != null)
            {
                foreach (var str in test)
                {
                    var notebook = Entities.notebook.Where(x => x.person.name1 == str || x.person.name2 == str).ToList();
                    var book = Entities.book.Where(x => x.title.Contains(str) || x.person.name1 == str || x.person.name2 == str).ToList();
                    var equipment = Entities.equipment.Where(x => x.person.name1 == str || x.person.name2 == str).ToList();

                    foreach (var n in notebook)
                    {
                        if (notebooks.FirstOrDefault(x => x.id == n.id) == null)
                            notebooks.Add(n);
                    }
                    foreach (var b in book)
                    {
                        if (books.FirstOrDefault(x => x.id == b.id) == null)
                            books.Add(b);
                    }
                    foreach (var e in equipment)
                    {
                        if (equipments.FirstOrDefault(x => x.id == e.id) == null)
                            equipments.Add(e);
                    }
                }
            }

            searchList.Add("notebook", notebooks);
            searchList.Add("display", displays);
            searchList.Add("furniture", furnitures);
            searchList.Add("book", books);
            searchList.Add("equipment", equipments);


            return searchList;
        }
    }
}