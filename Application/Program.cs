﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogComponent;

namespace LogUsers
{
    using System.Threading;

    class Program
    {
        static void Main(string[] args)
        {
            ILog  logger = new AsyncLog();

            for (int i = 0; i < 15; i++)
            {
                logger.Write("Number with Flush: " + i.ToString());
                Thread.Sleep(50);
            }

            logger.StopWithFlush();

            ILog logger2 = new AsyncLog();

            for (int i = 50; i > 0; i--)
            {
                logger2.Write("Number with No flush: " + i.ToString());
                Thread.Sleep(20);
            }

            logger2.StopWithoutFlush();

            Console.ReadLine();
        }
    }
}
