namespace MeteMVC.Models
{
    public interface IKiralamaRepository : IRepository<Kiralama>
    {
        void Guncelle(Kiralama kiralama);
        void Kaydet();
    }
}
