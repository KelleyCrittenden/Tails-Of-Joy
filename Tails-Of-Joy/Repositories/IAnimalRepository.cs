﻿using System.Collections.Generic;
using Tails_Of_Joy.Models;

namespace Tails_Of_Joy.Repositories
{
    public interface IAnimalRepository
    {
        void Add(Animal animal);
        void Delete(int id);
        List<Animal> GetAllAdoptedAnimals();
        List<Animal> GetAllAvailableAnimals();
        List<Animal> GetAllUnavailable();
        Animal GetById(int id);
        void Update(Animal animal);
        void Reactivate(int id);
    }
}