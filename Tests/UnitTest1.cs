using ToDo;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        private static string GetDataLocation()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ToDoList\\todos.xml");
        }

        [TestMethod]
        public void SerializationTest()
        {
            ICollection<IToDo> todos = new IToDo[] { API.CreateToDo("Get Milk"), API.CreateToDo("Make Breakfast") };

            API.Serialize(GetDataLocation(), todos);

            ICollection<IToDo> deserialized = API.Deserialize(GetDataLocation());

            Assert.IsTrue(Enumerable.SequenceEqual(todos, deserialized, API.GetRecordStyleEqualityComparer()));
        }
    }
}