using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Test.Model.Classes;
using System.Data.Entity;

namespace Test.Model.DAL
{
    public static class Dal
    {
        public static List<PersonColours> GetPeople()
        {
            try
            {
                var people = new List<PersonColours>();

                using(var db = new TechTestEntities())
                {
                    db.Configuration.ProxyCreationEnabled = false;
                    people = (from p in db.People
                                      select new
                                      {
                                          Person = p,
                                          Colours = p.Colours.Where(x => x.IsEnabled == true).Select(x => x.Name)
                                      })
                                        .ToList()
                                        .Select(x => new PersonColours
                                        {
                                            Person = x.Person,
                                            Colours = String.Join(",",x.Colours)
                                        }).ToList();
                 }
            
                 return people;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public static Person GetPersonById(int personid)
        {
            var person = new Person();
            try
            {
                if(personid > 0)
                {
                    using(var db = new TechTestEntities())
                    {
                        db.Configuration.ProxyCreationEnabled = false;
                        person = db.People.FirstOrDefault( x=> x.PersonId == personid);
                    }
                    return person;
                }
                else
                {
                    throw new ArgumentException("Person ID paramter not valid");
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public static List<Colour> GetColoursByPerson(int personid)
        {
            var person = new List<Colour>();
            try
            {
                if(personid > 0)
                {
                    using(var db = new TechTestEntities())
                    {
                        db.Configuration.ProxyCreationEnabled = false;
                        person = db.Colours.Where(x => x.People.Any(y => y.PersonId == personid)).ToList();
                    }
                    return person;
                }
                else
                {
                    throw new ArgumentException("Person ID paramter not valid");
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public static void UpdatePeople(PersonEditViewModel personColours)
        {
            try
            {
                using(var db = new TechTestEntities())
                {
                    if(personColours != null)
                    {
                        var entity = db.People.FirstOrDefault(x => x.PersonId == personColours.Person.PersonId);

                        if (entity != null)
                        {
                            entity.IsAuthorised = personColours.Person.IsAuthorised;
                            entity.IsEnabled = personColours.Person.IsEnabled;

                            entity.Colours.Clear();

                            if (personColours.Colours != null)
                            {
                                foreach (Colour c in personColours.Colours)
                                {
                                    entity.Colours.Add(db.Colours.First(x => x.ColourId == c.ColourId));
                                }
                            }

                            db.People.Attach(entity);
                            db.Entry(entity).State = EntityState.Modified;
                            db.SaveChanges();
                        }

                    }
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

    }
}