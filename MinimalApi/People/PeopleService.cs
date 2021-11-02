using MinimalApi.People.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalApi.People
{
    public interface IPeopleService
    {
        IEnumerable<Person> GetPeople();
        Person? GetPersonById(Guid id);
        Person? GetPersonByName(string name);
        Person CreatePerson(Person person);
        Person? UpdatePerson(Person person);
        void DeletePerson(Guid id);
    }

    public class PeopleService : IPeopleService
    {
        private readonly Dictionary<Guid, Person> _people = new Dictionary<Guid, Person>();

        public Person CreatePerson(Person person)
        {
            var id = Guid.NewGuid();
            person.Id = id;
            person.Created = DateTime.UtcNow;
            _people.Add(id, person);
            return person;
        }

        public void DeletePerson(Guid id)
        {
            if (!_people.TryGetValue(id, out var existingPerson))
            {
                return;
            }
            _people.Remove(id);
        }

        public IEnumerable<Person> GetPeople()
        {
            return _people.Values;
        }

        public Person? GetPersonById(Guid id)
        {
            return _people.TryGetValue(id, out var person) ? person : null;
        }

        public Person? GetPersonByName(string name) => _people.Values.FirstOrDefault(p => p.Name == name);

        public Person? UpdatePerson(Person person)
        {
            if (!_people.TryGetValue(person.Id, out var existingPerson))
            {
                return null;
            }

            person.Id = existingPerson.Id;
            person.Created = existingPerson.Created;
            _people[existingPerson.Id] = person;
            return person;
        }
    }
}
