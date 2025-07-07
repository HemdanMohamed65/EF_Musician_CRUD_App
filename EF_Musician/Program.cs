using EF_Musician.Models;
using Microsoft.EntityFrameworkCore;

namespace EF_Musician
{
    public class Operation
    {
        private readonly MusicianContext _music = new MusicianContext();

        public void GetMusicianInfo()
        {
            var musicians = _music.Musicians.ToList();
            foreach (var musician in musicians)
            {
                Console.WriteLine($"Musician_ID: {musician.Musician_ID} ->>> Musician_Name: {musician.Name} ->>> Street: {musician.Street} ->>> City: {musician.City} ->>> Phone_Number: {musician.phone_Number}");
            }
        }

        public void GetAlbumInfo()
        {
            var albums = _music.Albums.ToList();
            foreach (var album in albums)
            {
                Console.WriteLine($"Album_ID: {album.Album_ID} ->>> Album_Title: {album.Title} ->>> CopyRight_Date: {album.Copyright_Date} ->>> Producer_ID: {album.Producer_ID}");
            }
        }

        public void AddMusician()
        {
            var musician = new Musician();
            Console.Write("Enter Musician_ID: ");
            musician.Musician_ID = int.Parse(Console.ReadLine());
            Console.Write("Enter Musician_Name: ");
            musician.Name = Console.ReadLine();
            Console.Write("Enter Musician_Street: ");
            musician.Street = Console.ReadLine();
            Console.Write("Enter Musician_City: ");
            musician.City = Console.ReadLine();
            Console.Write("Enter Musician_PhoneNumber: ");
            musician.phone_Number = Console.ReadLine();

            _music.Musicians.Add(musician);
            _music.SaveChanges();
        }

        public void AddAlbum()
        {
            var album = new Album();
            Console.Write("Enter Album_ID: ");
            album.Album_ID = int.Parse(Console.ReadLine());
            Console.Write("Enter Album_Title: ");
            album.Title = Console.ReadLine();
            Console.Write("Enter CopyRight_Date (leave empty for current date): ");
            string input = Console.ReadLine();
            album.Copyright_Date = string.IsNullOrEmpty(input) ? DateTime.Now : DateTime.Parse(input);
            Console.Write("Enter Producer_ID: ");
            album.Producer_ID = int.Parse(Console.ReadLine());

            _music.Albums.Add(album);
            _music.SaveChanges();
        }

        public void DeleteMusician()
        {
            Console.Write("Enter Musician_ID: ");
            int id = int.Parse(Console.ReadLine());
            var musician = _music.Musicians.SingleOrDefault(n => n.Musician_ID == id);
            if (musician != null)
            {
                _music.Musicians.Remove(musician);
                _music.SaveChanges();
                Console.WriteLine("Musician removed successfully.");
            }
            else
            {
                Console.WriteLine("Musician not found.");
            }
        }

        public void DeleteAlbum()
        {
            Console.Write("Enter Album_ID: ");
            int id = int.Parse(Console.ReadLine());
            var album = _music.Albums.SingleOrDefault(n => n.Album_ID == id);
            if (album != null)
            {
                _music.Albums.Remove(album);
                _music.SaveChanges();
                Console.WriteLine("Album removed successfully.");
            }
            else
            {
                Console.WriteLine("Album not found.");
            }
        }

        public void UpdateMusician()
        {
            Console.Write("Enter Musician_ID: ");
            int id = int.Parse(Console.ReadLine());
            var musician = _music.Musicians.SingleOrDefault(m => m.Musician_ID == id);

            if (musician != null)
            {
                Console.WriteLine("Choose the property you want to update:");
                Console.WriteLine("1. Musician_Name");
                Console.WriteLine("2. Musician_Street");
                Console.WriteLine("3. Musician_City");
                Console.WriteLine("4. Musician_PhoneNumber");
                int update_musician = int.Parse(Console.ReadLine());

                switch (update_musician)
                {
                    case 1:
                        Console.Write("Enter New Musician_Name: ");
                        musician.Name = Console.ReadLine();
                        break;
                    case 2:
                        Console.Write("Enter New Musician_Street: ");
                        musician.Street = Console.ReadLine();
                        break;
                    case 3:
                        Console.Write("Enter New Musician_City: ");
                        musician.City = Console.ReadLine();
                        break;
                    case 4:
                        Console.Write("Enter New Musician_PhoneNumber: ");
                        musician.phone_Number = Console.ReadLine();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. No changes made.");
                        return;
                }

                _music.SaveChanges();
                Console.WriteLine("Musician updated successfully.");
            }
            else
            {
                Console.WriteLine("Musician not found.");
            }
        }

        public void UpdateAlbum()
        {
            Console.Write("Enter Album_ID that you want to update: ");
            int id = int.Parse(Console.ReadLine());
            var album = _music.Albums.SingleOrDefault(n => n.Album_ID == id);

            if (album != null)
            {
                Console.WriteLine("Choose the property you want to update:");
                Console.WriteLine("1. Album_Title");
                Console.WriteLine("2. CopyRight_Date");
                Console.WriteLine("3. Producer_ID");
                int update_album = int.Parse(Console.ReadLine());

                switch (update_album)
                {
                    case 1:
                        Console.Write("Enter New Album_Title: ");
                        album.Title = Console.ReadLine();
                        break;
                    case 2:
                        Console.Write("Enter New CopyRight_Date (leave empty for current date): ");
                        string input = Console.ReadLine();
                        album.Copyright_Date = string.IsNullOrEmpty(input) ? DateTime.Now : DateTime.Parse(input);
                        break;
                    case 3:
                        Console.Write("Enter New Producer_ID: ");
                        album.Producer_ID = int.Parse(Console.ReadLine());
                        break;
                    default:
                        Console.WriteLine("Invalid choice. No changes made.");
                        return;
                }

                _music.SaveChanges();
                Console.WriteLine("Album updated successfully.");
            }
            else
            {
                Console.WriteLine("Album not found.");
            }
        }

        internal class Program
        {
            private static void Main(string[] args)
            {
                Operation operation = new Operation();
                Console.WriteLine("********** MUSICIAN INFORMATION **********");
                operation.GetMusicianInfo();
                Console.WriteLine("\n********** ALBUM INFORMATION **********");
                operation.GetAlbumInfo();

                Console.WriteLine("Choose a category:");
                Console.WriteLine("1. Musician");
                Console.WriteLine("2. Album");
                int Choices = int.Parse(Console.ReadLine());

                switch (Choices)
                {
                    case 1:
                        Console.WriteLine("You chose Musician. Choose an action [ ADD || DELETE || UPDATE ] : ");
                        Console.WriteLine("1. Add Musician");
                        Console.WriteLine("2. Delete Musician");
                        Console.WriteLine("3. Update Musician");
                        int musicianChoice = int.Parse(Console.ReadLine());

                        switch (musicianChoice)
                        {
                            case 1:
                                Console.WriteLine("Your Choice is Adding Musician...");
                                operation.AddMusician();
                                operation.GetMusicianInfo();
                                break;
                            case 2:
                                Console.WriteLine("Your Choice is Deleting Musician...");
                                operation.DeleteMusician();
                                operation.GetMusicianInfo();
                                break;
                            case 3:
                                Console.WriteLine("Your Choice is Updating Musician...");
                                operation.UpdateMusician();
                                operation.GetMusicianInfo();
                                break;
                            default:
                                Console.WriteLine("Invalid action for Musician.");
                                break;
                        }
                        break;
                    case 2:
                        Console.WriteLine("You chose Album. Choose an action [ ADD || DELETE || UPDATE ] :");
                        Console.WriteLine("1. Add Album");
                        Console.WriteLine("2. Delete Album");
                        Console.WriteLine("3. Update Album");
                        int albumChoice = int.Parse(Console.ReadLine());

                        switch (albumChoice)
                        {
                            case 1:
                                Console.WriteLine("Your Choice is Adding Album...");
                                operation.AddAlbum();
                                operation.GetAlbumInfo();
                                break;
                            case 2:
                                Console.WriteLine("Your Choice is Deleting Album...");
                                operation.DeleteAlbum();
                                operation.GetAlbumInfo();
                                break;
                            case 3:
                                Console.WriteLine("Your Choice is Updating Album...");
                                operation.UpdateAlbum();
                                operation.GetAlbumInfo();
                                break;
                            default:
                                Console.WriteLine("Invalid action for Album.");
                                break;
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid category choice.");
                        break;
                }
            }
        }
    }
}
