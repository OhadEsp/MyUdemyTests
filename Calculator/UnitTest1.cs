// 8. Need to using Domain for creating Calculator object. Need also a reference.
using Domain;

namespace CalculatorTest
{
    public class UnitTest1
    {
        [Fact]
        public void Sum_Of_2_And_2_Should_Be_4()
        {
            // 7. Create an object of Domain.Calculator in order to use Sum functionality.
            var calculator = new Calculator();

            //// 1. Call function that doesn't exist yet.
            //var result = Sum(2, 2);

            // 8. Use calculator object to call Sum functionality.
            var result = calculator.Sum(2, 2);

            // 9. Step 9 in CalculateController.cs

            // 3. The Sum function should return 4. So let's throw exception if it does not.
            if (result != 4)
            {
                throw new Exception();
            }

            // 4. Ctrl R + T to run the test. It will fail.
            //    If we'll take a look in test run description we'll see that "Not Implemented". We need to implement some logic.
        }


        // 6. We'll move this function into Domain.
        //// 2. Add function to resolve the error in step 1 but still not implemented, just for the project compilation.
        //int Sum(int left, int right)
        //{
        //    // From step 2.
        //    // throw new NotImplementedException();

        //    // 5. We'll add implementation. Now if we'll run the test it will pass.
        //    return left + right;
        //}
    }
}