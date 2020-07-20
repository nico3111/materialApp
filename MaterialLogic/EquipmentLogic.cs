using MaterialData.exceptions;
using MaterialData.interfaces;
using MaterialData.models;
using MaterialData.repository;
using System.Collections.Generic;
using System.Linq;

namespace MaterialLogic
{
    public class EquipmentLogic : BaseLogic<equipment>, IMaterialLogic
    {
        public EquipmentLogic(BaseRepository<equipment> Repo) : base(Repo)
        {
        }

        public override void IsValid(equipment item)
        {
            List<string> errList = new List<string>();
            if (string.IsNullOrEmpty(item.type))
                errList.Add("𝗔𝗿𝘁");

            if (item.quantity == null)
                errList.Add("𝗔𝗻𝘇𝗮𝗵𝗹");

            if (errList.Count > 0)
            {
                string err = BuildErrorMessage(errList);
                throw new InvalidInputException(err);
            }

            if (item.quantity < 1)
                throw new InvalidInputException("Anzahl darf nicht kleiner als 1 sein!");
        }

        public override equipment SetLocation(equipment item)
        {
            bool done = false;
            if (item.location_id == null && item.person_id == null)
                item.location_id = Repo.defaultLocation;

            if (item.location_id != null && item.person_id != null)
                throw new DuplicateEntryException("Bitte Zubehör einer Person ODER einem Standort zuweisen!");

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
                    var existingEquipment = Repo.Entities.Set<equipment>().FirstOrDefault(x => x.type == item.type && item.make == x.make && item.model == x.model);
                    if (existingEquipment != null && existingEquipment.id != item.id && existingEquipment.location_id == item.location_id)
                    {
                        existingEquipment.quantity += item.quantity;
                        Repo.Entities.equipment.Update(existingEquipment);
                        Repo.Entities.SaveChanges();
                        return null;
                    }
                }
            }

            return item;
        }

        private equipment ReturnItem(equipment equipment)
        {
            equipment existingEquipment = Repo.Entities.equipment.FirstOrDefault(x => x.type == equipment.type);
            equipment sameEquipmentInDb = Repo.Entities.equipment.FirstOrDefault(x => x.id == equipment.id);

            if (existingEquipment != null && existingEquipment.id == equipment.id)
                return equipment;

            if (sameEquipmentInDb.quantity < equipment.quantity)
                throw new InvalidInputException("Es kann nicht mehr Zubehör zurückgegeben werden als ausgeliehen wurde!");

            if (existingEquipment != null && existingEquipment.location_id == equipment.location_id)
            {
                existingEquipment.quantity += equipment.quantity;
                Repo.Entities.equipment.Remove(sameEquipmentInDb);
                Repo.Entities.SaveChanges();
            }
            else
            {
                equipment.id = 0;
                Repo.Entities.equipment.Remove(sameEquipmentInDb);
                Repo.Entities.equipment.Add(equipment);
                Repo.Entities.SaveChanges();
                return null;
            }

            return existingEquipment;
        }

        private equipment RentItem(equipment equipment)
        {
            equipment existingEquipment = Repo.Entities.equipment.FirstOrDefault(x => x.id == equipment.id);
            equipment alreadyBorrowedEquipment = Repo.Entities.equipment.FirstOrDefault(x => x.person_id == equipment.person_id);

            if (alreadyBorrowedEquipment != null)
            {
                Repo.GetRelation();
                throw new InvalidInputException($"Zubehör \"{equipment.type}\" wurde bereits an \"{alreadyBorrowedEquipment.person.name1} {alreadyBorrowedEquipment.person.name2}\" verliehen!");
            }

            if (existingEquipment != null)
            {
                if (existingEquipment.quantity < equipment.quantity)
                    throw new InvalidInputException($"Die Anzahl des ausgeliehenem Zubehörs darf nicht den Lagerbestand überschreiten!");
                else
                {
                    equipment.quantity = 1;
                    equipment.id = 0;
                    equipment.location_id = null;

                    Repo.Entities.equipment.Add(equipment);

                    existingEquipment.quantity -= equipment.quantity;
                }

                if (existingEquipment.quantity <= 0)
                {
                    Repo.Entities.equipment.Remove(existingEquipment);
                    existingEquipment = null;
                }

                Repo.Entities.SaveChanges();

                return existingEquipment;
            }
            else
                return equipment;
        }

        private bool RebookItem(equipment item)
        {
            if (item.id == 0)
                return false;
            if (item.person_id != null)
                return false;
            var existingEquipment = Repo.Entities.Set<equipment>().FirstOrDefault(x => x.id == item.id);
            var otherEquipment = Repo.Entities.Set<equipment>().FirstOrDefault(x => x.type == item.type && x.location_id == item.location_id);

            if (existingEquipment.quantity < item.quantity)
                throw new InvalidInputException($"Es kann nicht {item.quantity}x {item.type} umgebucht werden, da nur {existingEquipment.quantity} lagernd sind!");

            if (otherEquipment == null)
            {
                existingEquipment.quantity -= item.quantity;

                if (existingEquipment.quantity <= 0)
                    Repo.Entities.equipment.Remove(existingEquipment);
                else
                    Repo.Entities.equipment.Update(existingEquipment);

                item.id = 0;
                Repo.Entities.equipment.Add(item);
                Repo.Entities.SaveChanges();
                return true;
            }

            if (otherEquipment != null && otherEquipment.id != item.id)
            {
                otherEquipment.quantity += item.quantity;
                existingEquipment.quantity -= item.quantity;

                if (existingEquipment.quantity <= 0)
                    Repo.Entities.equipment.Remove(existingEquipment);
                else
                    Repo.Entities.equipment.Update(existingEquipment);

                Repo.Entities.equipment.Update(otherEquipment);
                Repo.Entities.SaveChanges();
                return true;
            }
            return false;
        }
    }
}