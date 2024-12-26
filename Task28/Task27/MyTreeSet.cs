using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Task27;

namespace Task24
{
    class MyTreeSet<T> : MyIterator1<T>, MyNavigableSet<T>
         where T : IComparable<T>
    {
        private Node<T> root;
        private int size;
        private Comparer<T> comparator;
        private IEnumerator<T> cursor;
        public bool HasNext()
        {
            IEnumerator<T> tempCursor = cursor;
            return tempCursor.MoveNext();
        }
        public T Next()
        {
            if (!HasNext())
                throw new InvalidOperationException
                    ("Cursor reached the end");
            cursor.MoveNext();
            return cursor.Current;
        }
        public void Remove()
        {
            Remove(cursor.Current);
        }
        public IEnumerator<T> Iterator()
        {
            return DescendingIterator().GetEnumerator();
        }
        private void IteratorReset()
        {
            cursor = Iterator();
        }
        public MyTreeSet()
        {
            root = null;
            size = 0;
            comparator = Comparer<T>.Default;
            cursor = Iterator();
        }
        public MyTreeSet(Comparer<T> comparator)
        {
            root = null;
            size = 0;
            this.comparator = comparator;
            cursor = Iterator();
        }
        public MyTreeSet(MyCollection<T> collection)
        {
            T[] array = collection.ToArray();
            root = null;
            size = 0;
            comparator = Comparer<T>.Default;
            foreach (T element in array)
                Add(element);
            cursor = Iterator();
        }
        public MyTreeSet(SortedSet<T> set)
        {
            foreach (T element in set)
                Add(element);
            cursor = Iterator();
        }
        public void Add(T value)
        {
            Node<T> newNode = new Node<T>(value);
            if (root == null)
            {
                root = newNode;
                IteratorReset();
                return;
            }
            Node<T> parentNode = null;
            Node<T> currentNode = root;
            while (currentNode != null)
            {
                parentNode = currentNode;
                if (comparator.Compare(value, currentNode.data) == 0)
                    return;
                if (comparator.Compare(value, currentNode.data) < 0)
                    currentNode = currentNode.left;
                else
                    currentNode = currentNode.right;
            }
            if (comparator.Compare(value, parentNode.data) < 0)
                parentNode.left = newNode;
            else
                parentNode.right = newNode;
            newNode.parent = parentNode;
            newNode.color = Color.Red;
            Balance(newNode);
        }
        public void AddAll(MyCollection<T> collection)
        {
            T[] array = collection.ToArray();
            foreach (T element in array)
                Add(element);
        }
        public void Clear()
        {
            root = null;
        }
        public bool Contains(object obj)
        {
            Node<T> currentNode = root;
            while (currentNode != null)
            {
                if (comparator.Compare((T)obj, currentNode.data) == 0)
                    return true;
                if (comparator.Compare((T)obj, currentNode.data) < 0)
                    currentNode = currentNode.left;
                else
                    currentNode = currentNode.right;
            }
            return false;
        }
        public bool ContainsAll(MyCollection<T> collection)
        {
            T[] array = collection.ToArray();
            foreach (T element in array)
                if (!Contains(element))
                    return false;
            return true;
        }
        public bool IsEmpty()
        {
            return root == null;
        }
        public void Remove(object element)
        {
            Node<T> currentNode = root;
            while (currentNode != null &&
                comparator.Compare((T)element, currentNode.data) != 0)
            {
                if (comparator.Compare((T)element, currentNode.data) == 0)
                    continue;
                if (comparator.Compare((T)element, currentNode.data) < 0)
                    currentNode = currentNode.left;
                else
                    currentNode = currentNode.right;
            }
            Node<T> nodeToDelete = currentNode;
            if (nodeToDelete == null)
                return;
            if (nodeToDelete.left == null && nodeToDelete.right == null)
                DeleteNodeWithNoChildren(nodeToDelete);
            else if (nodeToDelete.left == null)
                DeleteNodeWithOneChild(nodeToDelete, nodeToDelete.right);
            else if (nodeToDelete.right == null)
                DeleteNodeWithOneChild(nodeToDelete, nodeToDelete.left);
            else
                DeleteNodeWithTwoChildren(nodeToDelete);
            IteratorReset();
        }
        public void RemoveAll(MyCollection<T> collection)
        {
            T[] array = collection.ToArray();
            foreach (T element in array)
                Remove(element);
        }
        public void RetainAll(MyCollection<T> collection)
        {
            T[] array = collection.ToArray();
            T[] elements = ToArray();
            bool flag;
            foreach (T element in elements)
            {
                flag = false;
                for (int i = 0; i < array.Length && !flag; i++)
                    if (comparator.Compare(element, array[i]) == 0)
                        flag = true;
                if (!flag)
                    Remove(element);
            }
        }
        public int Size()
        {
            return size;
        }
        public T[] ToArray()
        {
            T[] array = new T[size];
            int index = 0;
            ToArray(root, array, ref index);
            return array;
        }
        private void ToArray(Node<T> node, T[] array, ref int index)
        {
            if (node != null)
            {
                ToArray(node.left, array, ref index);
                array[index] = node.data;
                index++;
                ToArray(node.right, array, ref index);
            }
        }
        public void ToArray(ref T[] array)
        {
            if (array == null)
            {
                array = ToArray();
                return;
            }
            int index = 0;
            if (array.Length == size)
            {
                ToArray(root, array, ref index);
                return;
            }
            array = new T[size];
            ToArray(root, array, ref index);
        }
        public T First()
        {
            if (size == 0)
                throw new InvalidOperationException("Set is empty");
            Node<T> node = root;
            while (node.left != null)
                node = node.left;
            return node.data;
        }
        public T Last()
        {
            if (size == 0)
                throw new InvalidOperationException("Set is empty");
            Node<T> node = root;
            while (node.right != null)
                node = node.right;
            return node.data;
        }
        public MyTreeSet<T> SubSet(T fromElement, T toElement)
        {
            MyTreeSet<T> subSet = new MyTreeSet<T>();
            T[] array = ToArray();
            foreach (T element in array)
                if (comparator.Compare(element, fromElement) >= 0 &&
                    comparator.Compare(element, toElement) < 0)
                    subSet.Add(element);
            return subSet;
        }
        public MyTreeSet<T> HeadSet(T toElement)
        {
            MyTreeSet<T> headSet = new MyTreeSet<T>();
            T[] array = ToArray();
            foreach (T element in array)
                if (comparator.Compare(element, toElement) < 0)
                    headSet.Add(element);
            return headSet;
        }
        public MyTreeSet<T> TailSet(T fromElement)
        {
            MyTreeSet<T> tailSet = new MyTreeSet<T>();
            T[] array = ToArray();
            foreach (T element in array)
                if (comparator.Compare(element, fromElement) >= 0)
                    tailSet.Add(element);
            return tailSet;
        }
        public T Ceiling(T obj)
        {
            T[] array = ToArray();
            foreach (T element in array)
                if (comparator.Compare(element, obj) >= 0)
                    return element;
            return default;
        }
        public T Floor(T obj)
        {
            T floorElement = default;
            T[] array = ToArray();
            foreach (T element in array)
                if (comparator.Compare(element, obj) <= 0)
                    floorElement = element;
            return floorElement;
        }
        public T Higher(T obj)
        {
            T higherElement = default;
            T[] array = ToArray();
            foreach (T element in array)
                if (comparator.Compare(element, obj) > 0)
                    higherElement = element;
            return higherElement;
        }
        public T Lower(T obj)
        {
            T[] array = ToArray();
            foreach (T element in array)
                if (comparator.Compare(element, obj) < 0)
                    return element;
            return default;
        }
        public MyTreeSet<T> HeadSet(T upperBound, bool incl)
        {
            MyTreeSet<T> subset = new MyTreeSet<T>();
            T[] array = ToArray();
            foreach (T element in array)
                if (comparator.Compare(element, upperBound) < 0 ||
                    comparator.Compare(element, upperBound) == 0 && incl)
                    subset.Add(element);
            return subset;
        }
        public MyTreeSet<T> SubSet(T lowerBound, bool lowIncl, T upperBound, bool highIncl)
        {
            MyTreeSet<T> subset = new MyTreeSet<T>();
            T[] array = ToArray();
            foreach (T element in array)
                if ((comparator.Compare(element, lowerBound) > 0 ||
                    comparator.Compare(element, lowerBound) == 0 && lowIncl) &&
                    (comparator.Compare(element, upperBound) < 0 ||
                    comparator.Compare(element, upperBound) == 0 && highIncl))
                    subset.Add(element);
            return subset;
        }
        public MyTreeSet<T> TailSet(T fromElement, bool inclusive)
        {
            MyTreeSet<T> subset = new MyTreeSet<T>();
            T[] array = ToArray();
            foreach (T element in array)
                if (comparator.Compare(element, fromElement) > 0 ||
                    comparator.Compare(element, fromElement) == 0 && inclusive)
                    subset.Add(element);
            return subset;
        }
        public T PollLast()
        {
            if (size == 0)
                return default;
            Node<T> node = root;
            while (node.right != null)
                node = node.right;
            T element = node.data;
            Remove(element);
            return element;
        }
        public T PollFirst()
        {
            if (size == 0)
                return default;
            Node<T> node = root;
            while (node.left != null)
                node = node.left;
            T element = node.data;
            Remove(element);
            return element;
        }
        public IEnumerable<T> DescendingIterator()
        {
            if (root == null) yield break;
            Stack<Node<T>> stack = new Stack<Node<T>>();
            Node<T> current = root;
            while (current != null || stack.Count > 0)
            {
                while (current != null)
                {
                    stack.Push(current);
                    current = current.right;
                }
                current = stack.Pop();
                yield return current.data;
                current = current.left;
            }
        }
        public SortedSet<T> DescendingSet()
        {
            SortedSet<T> descendingSet =
                new SortedSet<T>(Comparer<T>.Create((x, y) => comparator.Compare(y, x)));
            IEnumerable<T> enumerator = DescendingIterator();
            foreach (var item in enumerator)
                descendingSet.Add(item);
            return descendingSet;
        }
        private void Balance(Node<T> node)
        {
            while (node != root && node.parent.color == Color.Red)
                if (node.parent == node.parent.parent.left)
                {
                    Node<T> uncleNode = node.parent.parent.right;
                    if (uncleNode != null && uncleNode.color == Color.Red)
                    {
                        node.parent.color = Color.Black;
                        uncleNode.color = Color.Black;
                        node.parent.parent.color = Color.Red;
                        node = node.parent.parent;
                    }
                    else
                    {
                        if (node == node.parent.right)
                        {
                            node = node.parent;
                            LeftRotate(node);
                        }
                        node.parent.color = Color.Black;
                        node.parent.parent.color = Color.Red;
                        RightRotate(node.parent.parent);
                    }
                }
                else
                {
                    Node<T> uncleNode = node.parent.parent.left;
                    if (uncleNode != null && uncleNode.color == Color.Red)
                    {
                        node.parent.color = Color.Black;
                        uncleNode.color = Color.Black;
                        node.parent.parent.color = Color.Red;
                        node = node.parent.parent;
                    }
                    else
                    {
                        if (node == node.parent.left)
                        {
                            node = node.parent;
                            RightRotate(node);
                        }
                        node.parent.color = Color.Black;
                        node.parent.parent.color = Color.Red;
                        LeftRotate(node.parent.parent);
                    }
                }
            root.color = Color.Black;
            IteratorReset();
        }
        private void LeftRotate(Node<T> node)
        {
            Node<T> rightNode = node.right;
            node.right = rightNode.left;
            if (rightNode.left != null)
                rightNode.left.parent = node;
            rightNode.parent = node.parent;
            if (node.parent == null)
                root = rightNode;
            else if (node == node.parent.left)
                node.parent.left = rightNode;
            else
                node.parent.right = rightNode;
            rightNode.left = node;
            node.parent = rightNode;
        }
        private void RightRotate(Node<T> node)
        {
            Node<T> leftNode = node.left;
            node.left = leftNode.right;
            if (leftNode.right != null)
                leftNode.right.parent = node;
            leftNode.parent = node.parent;
            if (node.parent == null)
                root = leftNode;
            else if (node == node.parent.right)
                node.parent.right = leftNode;
            else
                node.parent.left = leftNode;
            leftNode.right = node;
            node.parent = leftNode;
        }
        private void DeleteNodeWithNoChildren(Node<T> nodeToDelete)
        {
            if (nodeToDelete == root)
                root = null;
            else if (nodeToDelete == nodeToDelete.parent.left)
                nodeToDelete.parent.left = null;
            else
                nodeToDelete.parent.right = null;
        }
        private void DeleteNodeWithOneChild(Node<T> nodeToDelete, Node<T> childNode)
        {
            if (nodeToDelete == root)
            {
                root = childNode;
                childNode.parent = null;
            }
            else if (nodeToDelete == nodeToDelete.parent.left)
                nodeToDelete.parent.left = childNode;
            else
                nodeToDelete.parent.right = childNode;
            childNode.parent = nodeToDelete.parent;
        }
        private void DeleteNodeWithTwoChildren(Node<T> nodeToDelete)
        {
            Node<T> successorNode = nodeToDelete;
            while (successorNode.left != null)
                successorNode = successorNode.left;
            nodeToDelete.data = successorNode.data;
            if (successorNode.left == null && successorNode.right == null)
                DeleteNodeWithNoChildren(successorNode);
            else if (successorNode.left == null)
                DeleteNodeWithOneChild(successorNode, successorNode.right);
            else
                DeleteNodeWithOneChild(successorNode, successorNode.left);
        }
        public void Print()
        {
            RecursionPrint(root);
            Console.WriteLine();
        }
        private void RecursionPrint(Node<T> node)
        {
            if (node != null)
            {
                RecursionPrint(node.left);
                Console.Write(node.data + " ");
                RecursionPrint(node.right);
            }
        }
        public void PrintWithColors()
        {
            RecursionPrintWithColors(root);
            Console.WriteLine();
        }
        private void RecursionPrintWithColors(Node<T> node)
        {
            if (node != null)
            {
                RecursionPrintWithColors(node.left);
                Console.WriteLine(node.data + " " + node.color);
                RecursionPrintWithColors(node.right);
            }
        }
    }
    class Node<T>
    {
        public T data;
        public Node<T> left;
        public Node<T> right;
        public Node<T> parent;
        public Color color;

        public Node(T data)
        {
            this.data = data;
            left = null;
            right = null;
            parent = null;
            color = Color.Black;
        }
    }
    enum Color
    {
        Red,
        Black
    }
}
