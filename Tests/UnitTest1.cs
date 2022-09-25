using ToDoList;

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
        public void TestMethod1()
        {
            ICollection<IToDo> todos = API.CreateToDos("Get Milk", "Make Breakfast");

            API.Serialize(GetDataLocation(), todos);
        }
    }
}