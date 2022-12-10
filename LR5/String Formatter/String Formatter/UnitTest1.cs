namespace String_Formatter
{
    public class UnitTest1
    {
        
        User user = new User("John", "White");
        private Product product = new Product(1, "Potato", 12.99);
        
        
        [Fact]
        public void userGreetingTest()
        {
            string expected = "Hi, John White!";
            string actual = user.GetGreeting();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void productInfoTest()
        {
            string expected = "Product {id : 1 , name : Potato, price : 12,99}";
            string actual = product.getProductInfo();
            
            Assert.Equal(expected, actual);
        }
    }
}