﻿using MaterialData.models;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System.Collections.Generic;
using System.Linq;

namespace MaterialData.repository
{
    public abstract class BaseRepository<T> where T : Material
    {
        public DcvEntities Entities;
        public BaseRepository(DcvEntities entities)
        {
            Entities = entities;
        }

        public abstract void GetRelation();
        public void Delete(int id)
        {
            var t = Entities.Find<T>(id);
            Entities.Remove(t);
            Entities.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            GetRelation();
            return Entities.Set<T>().ToList();
        }

        public T GetAny(int id)
        {
            GetRelation();
            var item = Entities.Find<T>(id);
            return item;
        }

        public void Save(T t)
        {
            Entities.Add(t);
            Entities.SaveChanges();
        }

        public void Update(int id, T t)
        {

            T item = Entities.Find<T>(id);

            if (item != null)
            {

                /*Entities.Update(item);
                t.id = id;
                item = t;*/

                Entities.Remove(item);
                Entities.Add(t);
                Entities.SaveChanges();
            }           
        }

    }
}
