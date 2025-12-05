using System;
using SecretSantaBackend.Data;
using SecretSantaBackend.Models;
using SecretSantaBackend.Services;
using Microsoft.EntityFrameworkCore;


namespace SecretSantaBackend.Services
{
    public class SecretSantaService : ISecretSantaService
    {
        private readonly AppDbContext _context;
        private readonly Random _random = new();

        public SecretSantaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<SecretSantaList> GeneratePairsAsync()
        {
            var employees = await _context.Employees.ToListAsync();

            var availableReceivers = new List<Employee>(employees);
            var pairs = new List<Pair>();

            var newSecretSantaList = new SecretSantaList();

            foreach (var giver in employees.OrderBy(e => _random.Next()))
            {
                var possibleReceivers = availableReceivers.Where(r => r.Id != giver.Id).ToList();

                if (possibleReceivers.Count == 0)
                {
                    newSecretSantaList.Pairs = pairs;
                    newSecretSantaList.UnpairedEmployee = giver;

                    _context.SecretSantaLists.Add(newSecretSantaList);
                    await _context.SaveChangesAsync();

                    return newSecretSantaList;
                }

                var receiver = possibleReceivers[_random.Next(possibleReceivers.Count)];
                pairs.Add(new Pair { GiverId = giver.Id, ReceiverId = receiver.Id });

                availableReceivers.Remove(receiver);
            }

            newSecretSantaList.Pairs = pairs;

            _context.SecretSantaLists.Add(newSecretSantaList);
            await _context.SaveChangesAsync();

            return newSecretSantaList;
        }
    }
}
