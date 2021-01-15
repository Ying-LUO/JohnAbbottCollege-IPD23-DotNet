using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Day01PeopleListInFile;
using System.Linq;

namespace Day01PeopleListInFile.UnitTests
{

    [TestClass]
    public class PersonTests
    {
        [TestInitialize]
        public void Initialize()
        {
            
        }

        [TestMethod]
        public void Constructor_Parameters_ReturnTypeTrue()
        {
            // Arrange
            string name = "Test";
            int age = 12;
            string city = "City";

            // Act
            var person = new Person(name, age, city);

            // Assert
            Assert.IsInstanceOfType(person, typeof(Person));

        }

        [TestMethod]
        public void Getter_NameGetValue_ReturnTrue()
        {
            // Arrange
            string name = "Test";
            int age = 12;
            string city = "City";

            // Act
            var person = new Person(name, age, city);

            // Assert
            Assert.AreEqual(name, person.Name);

        }

        [TestMethod]
        public void Getter_AgeGetValue_ReturnTrue()
        {
            // Arrange
            string name = "Test";
            int age = 12;
            string city = "City";

            // Act
            var person = new Person(name, age, city);

            // Assert
            Assert.AreEqual(age, person.Age);

        }

        [TestMethod]
        public void Getter_CityGetValue_ReturnTrue()
        {
            // Arrange
            string name = "Test";
            int age = 12;
            string city = "City";

            // Act
            var person = new Person(name, age, city);

            // Assert
            Assert.AreEqual(city, person.City);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Setter_NameMinLength_ThrowException()
        {
            // Arrange
            string name = "T";
            int age = 12;
            string city = "City";

            // Act
            var person = new Person(name, age, city);

            // Assert
            // should throw exception
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Setter_NameMaxLength_ThrowException()
        {
            // Arrange
            string name = string.Join("", Enumerable.Repeat(0, 101).Select(n => (char)new Random().Next(127)));
            int age = 12;
            string city = "City";

            // Act
            var person = new Person(name, age, city);

            // Assert
            // should throw exception
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Setter_NameSpecialCharacters_ThrowException()
        {
            // Arrange
            string name = "Test;test";
            int age = 12;
            string city = "City";

            // Act
            var person = new Person(name, age, city);

            // Assert
            // should throw exception
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Setter_AgeMaxValue_ThrowException()
        {
            // Arrange
            string name = "Test";
            int age = 151;
            string city = "City";

            // Act
            var person = new Person(name, age, city);

            // Assert
            // should throw exception
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Setter_AgeMinValue_ThrowException()
        {
            // Arrange
            string name = "Test";
            int age = -1;
            string city = "City";

            // Act
            var person = new Person(name, age, city);

            // Assert
            // should throw exception
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Setter_CityMinLength_ThrowException()
        {
            // Arrange
            string name = "Test";
            int age = 12;
            string city = "C";

            // Act
            var person = new Person(name, age, city);

            // Assert
            // should throw exception
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Setter_CityMaxLength_ThrowException()
        {
            // Arrange
            string name = "Test";
            int age = 12;
            string city = string.Join("", Enumerable.Repeat(0, 101).Select(n => (char)new Random().Next(127)));

            // Act
            var person = new Person(name, age, city);

            // Assert
            // should throw exception
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Setter_CitySpecialCharacters_ThrowException()
        {
            // Arrange
            string name = "Test";
            int age = 12;
            string city = "City;city";

            // Act
            var person = new Person(name, age, city);

            // Assert
            // should throw exception
        }

        [TestMethod]
        public void ToString_Value_ReturnTrue()
        {
            // Arrange
            string name = "Test";
            int age = 12;
            string city = "City";

            // Act
            var person = new Person(name, age, city);

            // Assert
            StringAssert.Equals("Person name Test with age 12 in city City", person.ToString());
        }
    }
}
