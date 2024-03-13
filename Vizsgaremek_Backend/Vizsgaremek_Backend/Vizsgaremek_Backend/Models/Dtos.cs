namespace Vizsgaremek_Backend.Models
{
    public record FelhasznaloDto(Guid Id,string Felhasznalonev,string JelszoHash, string Salt,string Email, byte[] Avatar, string Telefonszam,string Leiras,DateTime Letrehozva,int SzerepId,int VarosId,int LikeSzamlalo);
}
