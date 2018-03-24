﻿using AutoDI;
using AutoDI.Container.Examples;
using System;

namespace Console.Framework.NoFody
{
    class Program
    {
        static void Main(string[] args)
        {
            AutoDI.Generated.AutoDI.Init();
            var quoteBoard = GlobalDI.GetService<QuoteBoard>();
            quoteBoard.ShowQuotes();
            System.Console.ReadLine();
        }
    }

    public class QuoteBoard
    {
        private readonly IQuoteService _service;

        public QuoteBoard(IQuoteService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public void ShowQuotes()
        {
            foreach (Quote quote in _service.GetQuotes())
            {
                System.Console.WriteLine($"{quote.Text}");
                System.Console.WriteLine($"   -{quote.Author}");
                System.Console.WriteLine();
            }
        }
    }
}
