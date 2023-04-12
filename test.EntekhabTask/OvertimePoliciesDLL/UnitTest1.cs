namespace test.EntekhabTask.OvertimePoliciesDLL
{
    public class UnitTest1
    {
        [Fact]
        public void Test_10()
        {
            //Arrange
            OvertimePolicies.OvertimeCalculator overtimeCalculator = new OvertimePolicies.OvertimeCalculator();

            //Act
            var actual = overtimeCalculator.CalcurlatorA(1, 1);

            var excepted = 0;

            //Assert
            Assert.Equal(actual: actual, expected: excepted);
        }

        [Fact]
        public void Test_20()
        {
            //Arrange
            OvertimePolicies.OvertimeCalculator overtimeCalculator = new OvertimePolicies.OvertimeCalculator();

            //Act
            var actual = overtimeCalculator.CalcurlatorB(1, 1);

            var excepted = 0;

            //Assert
            Assert.Equal(actual: actual, expected: excepted);
        }


        [Fact]
        public void Test_30()
        {
            //Arrange
            OvertimePolicies.OvertimeCalculator overtimeCalculator = new OvertimePolicies.OvertimeCalculator();

            //Act
            var actual = overtimeCalculator.CalcurlatorC(1, 1);

            var excepted = 0;

            //Assert
            Assert.Equal(actual: actual, expected: excepted);
        }
    }
}