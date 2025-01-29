using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Test
{
    public static class WorldsDumbestFunctionTest
    {
        // naming convention - Classname_Methodname_ExpectedResult
        public static void WorldsDumbestFunction_ReturnsPikachuIfZero_ReturnString()
        {
            try
            {
                //Arrange - Go get your variables, whatever you need, your classes, your functions
                int num = 0;
                WorldsDumbestFunction worldsDumbest = new WorldsDumbestFunction();

                //Act - Execute this function
                string result = worldsDumbest.ReturnsPikachuIfZero(num);

                //Assert - whatever is returned is what you want.
                if (result == "Pikachu")
                {
                    Console.WriteLine("PASSED: WorldsDumbestFunction_ReturnsPikachuIfZero_ReturnString");
                }
                else
                {
                    Console.WriteLine("FAILED: WorldsDumbestFunction_ReturnsPikachuIfZero_ReturnString");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
