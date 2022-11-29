using ToDo;

namespace Tests
{
    [TestClass]
    public class Test
    {
        private static string GetDataLocation()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ToDoList\\todos.xml");
        }

        [TestMethod]
        public void SerializationTest()
        {
            ICollection<IToDo> todos = new IToDo[] { ToDoApi.CreateToDo("Get Milk"), ToDoApi.CreateToDo("Make Breakfast") };

            ToDoApi.Serialize(GetDataLocation(), todos);

            ICollection<IToDo> deserialized = ToDoApi.Deserialize(GetDataLocation());

            Assert.IsTrue(Enumerable.SequenceEqual(todos, deserialized, ToDoApi.GetRecordStyleEqualityComparer()));
        }
    }
}