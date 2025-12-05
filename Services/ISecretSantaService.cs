using System;
using SecretSantaBackend.Models;

public interface ISecretSantaService
{
    Task<SecretSantaList> GeneratePairsAsync();
}