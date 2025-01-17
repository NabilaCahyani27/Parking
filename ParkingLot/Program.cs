using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    class Kendaraan
    {
        public string RegistrationNumber { get; set; } // nomor Kendaraan 
        public string Colour { get; set; } // warna Kendraan
        public string Type { get; set; } // "Mobil" or "Motor"
        public DateTime CheckInTime { get; set; } // checkin time
    }

    static int totalSlots; // totalSlot
    static Dictionary<int, Kendaraan> parkingLot = new Dictionary<int, Kendaraan>(); // untuk melacak kendaraan yang parkir di lot, key "nomor slot parkir", object "Kendaraan"

    static void Main()
    {
        string command; // perintah iput
        while (true) // perintah loop
        {
            command = Console.ReadLine();// membaca input user
            if (string.IsNullOrWhiteSpace(command)) continue; // mengecek input apakah kosong atau hanya karakter kosong, jika terpenuhi continue

            string[] inputs = command.Split(' ', 2); // memecah string input menjadi 2 bagian
            string action = inputs[0]; // input pertama berupa peintah

            if (action == "create_parking_lot")
            {
                int slots = int.Parse(inputs[1]); // input pengguna
                CreateParkingLot(slots); // memanggil fungsi CreateParkingLot
            }
            else if (action == "park")
            {
                var details = inputs[1].Split(' '); // input pengguna
                string regNumber = details[0]; // nomor kendaraan
                string colour = details[1]; // warna kendaraan
                string type = details[2]; // tipe kendaraan
                ParkingKedaraan(regNumber, colour, type); // memanggil fungsi ParkingKedaraan
            }
            else if (action == "leave")
            {
                int slot = int.Parse(inputs[1]); // input pengguna
                LeaveSlot(slot); // memanggil fungsi LeaveSlot
            }
            else if (action == "status")
            {
                DisplayStatus(); // memanggil fungsi DisplayStatus
            }
            else if (action == "exit")
            {
                break;
            }
        }
    }

    static void CreateParkingLot(int slots)
    {
        totalSlots = slots; // total slot
        parkingLot.Clear(); // membersihkan lot
        Console.WriteLine($"Created a parking lot with {slots} slots"); // menampilkan pesan
    }

    static void ParkingKedaraan(string regNumber, string colour, string type)
    {
        if (type != "Mobil" && type != "Motor")
        {
            Console.WriteLine("Only Mobil and Motor are allowed"); // menampilkan pesan tipe yang bisa diparkir
            return;
        }

        int availableSlot = Enumerable.Range(1, totalSlots).FirstOrDefault(slot => !parkingLot.ContainsKey(slot)); // mencari slot yang tersedia
        if (availableSlot == 0) // jika slot tidak tersedia
        {
            Console.WriteLine("Sorry, parking lot is full");
        }
        else
        {
            // menambahkan kendaraan ke lot
            parkingLot[availableSlot] = new Kendaraan { RegistrationNumber = regNumber, Colour = colour, Type = type, CheckInTime = DateTime.Now };
            Console.WriteLine($"Allocated slot number: {availableSlot}");
        }
    }

    static void LeaveSlot(int slot)
    {
        if (parkingLot.ContainsKey(slot)) // jika slot ada
        {
            parkingLot.Remove(slot); // menghapus slot
            Console.WriteLine($"Slot number {slot} is free");
        }
        else
        {
            // jika slot tidak ada
            Console.WriteLine($"Slot number {slot} is already empty");
        }
    }

    static void DisplayStatus()
    {
        Console.WriteLine("Slot\tNo.\t\tType\tRegistration No\tColour"); // menampilkan header
        foreach (var entry in parkingLot.OrderBy(e => e.Key)) // menampilkan semua kendaraan
        {
            // menampilkan kendaraan
            Console.WriteLine($"{entry.Key}\t{entry.Value.RegistrationNumber}\t{entry.Value.Type}\t{entry.Value.Colour}");
        }
    }
}