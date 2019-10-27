using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;

namespace HW6.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            var libraryUI = new LibraryUI();
            libraryUI.Action();
        }
    }
}
