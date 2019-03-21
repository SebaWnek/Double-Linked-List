using System;

/* Double linked list  
 * Methods: 
 * - Sort,
 * - Add,
 * - AddAt,
 * - RemoveAt,
 * - RemoveElement,
 * - IndexOf,
 * - Contains,
 * - Access by index,
 * - Clear,
 * Properties: 
 * - Count,
 * 
 * Written from ground up with help from "Fundamentals of computer programing with C#" by Svetlin Nakov, Veselin Kolev & Co.
 */


namespace ConsoleApp21
{
    class Program
    {
        static void Main(string[] args)
        {
            DoubleLinkedList<string> shoppingList = new DoubleLinkedList<string>();
            shoppingList.Add("Milk");
            shoppingList.RemoveElement("Milk"); // Empty list
            shoppingList.Add("Honey");
            shoppingList.Add("Olives");
            shoppingList.Add("Water");
            shoppingList[2] = "A lot of " + shoppingList[2];
            shoppingList.Add("Fruits");
            shoppingList.RemoveAt(0); // Removes "Honey" (first)
            shoppingList.RemoveAt(2); // Removes "Fruits" (last)
            shoppingList.Add(null);
            shoppingList.Add("Beer");
            shoppingList.RemoveElement(null);
            Console.WriteLine("We need to buy:");
            for (int i = 0; i < shoppingList.Count; i++)
            {
                Console.WriteLine(" - " + shoppingList[i]);
            }
            Console.WriteLine("Position of 'Beer' = {0}",
            shoppingList.IndexOf("Beer"));
            Console.WriteLine("Position of 'Water' = {0}",
            shoppingList.IndexOf("Water"));
            Console.WriteLine("Do we have to buy Bread? " +
            shoppingList.Contains("Bread"));
            for (int i = 0; i < shoppingList.Count; i++)
            {
                Console.WriteLine(" - " + shoppingList[i]);
            }

            shoppingList.Sort();
            shoppingList.AddAt("Zebra", 1);
            Console.WriteLine("We need to buy:");
            for (int i = 0; i < shoppingList.Count; i++)
            {
                Console.WriteLine(" - " + shoppingList[i]);
            }

        }

        public class DoubleLinkedList<T>
        {
            public void Sort()
            {
                bool noChange = false;
                Node currentNode = head;
                string str1, str2;
                while (true)
                {

                    if (currentNode != tail)
                    {
                        str1 = currentNode.element.ToString();
                        str2 = currentNode.nextNode.element.ToString();
                        if (str1.CompareTo(str2) > 0)
                        {
                            T tmp = currentNode.element;
                            currentNode.element = currentNode.nextNode.element;
                            currentNode.nextNode.element = tmp;
                            noChange = false;
                        }
                        currentNode = currentNode.nextNode;
                    }
                    if (currentNode == tail && noChange == false)
                    {
                        currentNode = head;
                        noChange = true;
                    }
                    else if (currentNode == tail && noChange == true)
                    {
                        break;
                    }

                }
            }

            public class Node
            {
                public T element { get; set; }
                public Node nextNode { get; set; }
                public Node prevNode { get; set; }

                public Node(T element)
                {
                    this.element = element;
                    nextNode = prevNode = null;
                }

                public Node(T element, Node prevNode)
                {
                    this.element = element;
                    this.prevNode = prevNode;
                    prevNode.nextNode = this;
                }

            }

            private Node head = null;
            private Node tail = null;
            private int count = 0;

            public void Add(T element)
            {
                if (count == 0)
                {
                    head = new Node(element);
                    tail = head;
                    count++;
                }
                else
                {
                    Node newNode = new Node(element, tail);
                    tail.nextNode = newNode;
                    tail = newNode;
                    count++;
                }
            }

            public void AddAt(T element, int index)
            {
                if (index == count)
                {
                    Add(element);
                    return;
                }


                if (index == 0)
                {
                    Node newNode = new Node(element);
                    head.prevNode = newNode;
                    head = newNode;
                    count++;
                    return;
                }

                if (index > 0 && index < count)
                {
                    Node newNode = new Node(element);
                    Node currentNode = head;
                    for (int i = 0; i < index; i++)
                    {
                        currentNode = currentNode.nextNode;
                    }
                    currentNode.prevNode.nextNode = newNode;
                    currentNode.prevNode = newNode;
                    newNode.prevNode = currentNode.prevNode;
                    newNode.nextNode = currentNode;
                    count++;
                }

            }

            public T RemoveAt(int index)
            {

                if (index == count - 1)
                {
                    T tmp = tail.element;
                    tail.prevNode.nextNode = null;
                    tail = tail.prevNode;
                    count--;
                    return tmp;
                }
                if (index == 0)
                {
                    T tmp = head.element;
                    head.nextNode.prevNode = null;
                    head = head.nextNode;
                    count--;
                    return tmp;
                }

                Node currentNode = head;
                Node prevNode = null;
                int position = 0;

                while (position < index - 1)
                {
                    prevNode = currentNode;
                    currentNode = prevNode.nextNode;
                    position++;
                }

                RemoveNode(currentNode, index);


                return currentNode.element;
            }

            public void RemoveNode(Node currentNode, int index)
            {
                if (index >= count)
                {
                    throw new ArgumentOutOfRangeException();
                }
                else if (index == 0 && count == 1)
                {
                    head = null;
                    tail = null;
                    count = 0;
                }
                else if (index == 0 && count > 1)
                {
                    head = head.nextNode;
                    currentNode.nextNode.prevNode = null;
                    count--;
                }

                else if (index == count - 1 && count > 1)
                {
                    tail = tail.prevNode;
                    currentNode.prevNode.nextNode = null;
                    count--;
                }

                else if (index > 0 && index < count - 1)
                {
                    currentNode.prevNode.nextNode = currentNode.nextNode;
                    currentNode.nextNode.prevNode = currentNode.prevNode;
                    count--;
                }
            }

            public void RemoveElement(T search)
            {

                Node currentNode = head;
                int position = 0;
                while (position <= count)
                {
                    if (position == count)
                    {
                        throw new ArgumentException();
                    }
                    if (search != null)
                    {
                        if (object.Equals(currentNode.element, search))
                        {
                            RemoveNode(currentNode, position);
                            return;
                        }
                    }
                    else
                    {
                        if (currentNode.element == null)
                        {
                            RemoveNode(currentNode, position);
                            return;
                        }
                    }
                    position++;
                    currentNode = currentNode.nextNode;
                }
            }

            public int IndexOf(T search)
            {
                Node currentNode = head;
                int position = 0;
                while (position <= count)
                {
                    if (position == count)
                    {
                        return -1;
                    }
                    if (object.Equals(currentNode.element, search))
                    {
                        break;
                    }
                    position++;
                    currentNode = currentNode.nextNode;
                }
                return position;
            }

            public bool Contains(T search)
            {
                int index = IndexOf(search);
                if (index != -1) return true;
                else return false;
            }

            public T this[int index]
            {
                get
                {
                    if (index >= count || index < 0)
                        throw new ArgumentOutOfRangeException();

                    Node currentNode = head;
                    for (int i = 0; i < index; i++)
                        currentNode = currentNode.nextNode;
                    return currentNode.element;
                }
                set
                {
                    if (index >= count || index < 0)
                        throw new ArgumentOutOfRangeException();

                    Node currentNode = head;
                    for (int i = 0; i < index; i++)
                        currentNode = currentNode.nextNode;
                    currentNode.element = value;
                }
            }

            public void Clear()
            {
                head = null;
                tail = null;
                count = 0;
            }

            public int Count
            {
                get { return count; }
            }
        }
    }
}
