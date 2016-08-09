using System;
using System.Threading;
using DrawerBackup.PerformanceVariables;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing
{
    [TestClass]
    public class PerfVarsTest
    {
        private PerfFactory factory;

        public PerfVarsTest( )
        {
            this.factory = new PerfFactory( );
            this.factory.Start( );

            // Insert the variables
            insertVariables( );

        }

        private void insertVariables( )
        {
            this.factory.Update(new PerfVariable( )
            {
                Kind = PerfVarKind.Single,
                Lifetime = new TimeSpan(0, 1, 0),
                Changetime = new TimeSpan(1, 0, 0),
                Name = "testvar1"
            });

            this.factory.Update(new PerfVariable( )
            {
                Kind = PerfVarKind.Multiple,
                Lifetime = new TimeSpan(0, 0, 30),
                Changetime = new TimeSpan(1, 0, 0),
                Name = "testvar2"
            });

            this.factory.Update(new PerfVariable( )
            {
                Kind = PerfVarKind.Single,
                Lifetime = new TimeSpan(0, 5, 0),
                Changetime = new TimeSpan(1, 0, 0),
                Name = "testvar3"
            });

            this.factory.Update(new PerfVariable( )
            {
                Kind = PerfVarKind.Multiple,
                Lifetime = new TimeSpan(1, 0, 0),
                Changetime = new TimeSpan(0, 0, 1),
                Name = "testvar4"
            });
        }

        [TestMethod]
        public void GenericTest( )
        {
            // Test the var testvar1
            Testvar1( );


            // Test the var testvar2
            Testvar2( );

            // Test the var testvar4
            Testvar4( );

            // Remove the testvar3
            Removevar3( );

            // Test the lifetime for the var testvar2
            Testvar2lifetime( );
        }

        private void Removevar3( )
        {
            IPerfVarManager manager = this.factory.Get("testvar3");
            manager.Insert(1);
            manager.Insert(5);

            this.factory.Remove("testvar3");
        }

        private void Testvar2lifetime( )
        {
            IPerfVarManager manager = this.factory.Get("testvar2");

            manager.Insert(10);
            manager.Insert(25);

            Thread.Sleep(60000);

            manager.Insert(30);

            Assert.AreEqual(30, manager.Max( ));
            Assert.AreEqual(30, manager.Min( ));
            Assert.AreEqual(30, manager.Avg( ));
            Assert.AreEqual(30, manager.Last( ));
            Assert.AreEqual(30, manager.First( ));
        }

        private void Testvar1( )
        {
            IPerfVarManager manager = this.factory.Get("testvar1");

            manager.Insert(10);
            manager.Insert(25);
            manager.Insert(30);

            Assert.AreEqual(65, manager.Max( ));
            Assert.AreEqual(65, manager.Min( ));
            Assert.AreEqual(65, manager.Avg( ));
            Assert.AreEqual(65, manager.Last( ));
            Assert.AreEqual(65, manager.First( ));
        }


        private void Testvar4( )
        {
            IPerfVarManager manager = this.factory.Get("testvar4");

            manager.Insert(10);
            manager.Insert(25);

            Thread.Sleep(1000);
            manager.Insert(30);

            Thread.Sleep(1000);
            manager.Insert(10);

            Assert.AreEqual(30, manager.Max( ));
            Assert.AreEqual(10, manager.Min( ));
            Assert.AreEqual(25, manager.Avg( ));
            Assert.AreEqual(10, manager.Last( ));
            Assert.AreEqual(35, manager.First( ));
        }
        private void Testvar2( )
        {
            IPerfVarManager manager = this.factory.Get("testvar2");

            manager.Insert(10);
            Thread.Sleep(1000);
            manager.Insert(25);
            Thread.Sleep(1000);
            manager.Insert(30);

            Assert.AreEqual(65, manager.Max( ));
            Assert.AreEqual(65, manager.Min( ));
            Assert.AreEqual(65, (long)manager.Avg( ));
            Assert.AreEqual(65, manager.Last( ));
            Assert.AreEqual(65, manager.First( ));
        }
    }
}
