using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuffmanAlgorithm
{
    class Tree
    {
        public byte key;
        public int value; // value = sum pt tree
        public Tree right;
        public Tree left;
        public Tree()
        {
            this.right = null;
            this.left = null;
        }
    }

    class Huffman
    {

        public struct NodeType
        {
            public const bool TREE = false, CHARACTER = true;
            public bool type;
            public int index;
            public int value; // value = sum la tree
            public NodeType(bool type, int index, int value)
            {
                this.type = type;
                this.index = index;
                this.value = value;
            }
        }

        private List<byte> TreeCodesInorder;
        private Dictionary<byte, List<byte>> model;
        private Dictionary<byte, int> stats; 
		
		public Dictionary<byte, int> GetStatistic()
		{
			return this.stats;
		}
		
		public Dictionary<byte, List<byte>> GetModel()
		{
			return this.model;
		}

        private Dictionary<byte, int> CreateStatistic(byte[] input)
        {
            Dictionary<byte, int> statistica = new Dictionary<byte, int>();

            foreach (var item in input)
            {
                if (!statistica.ContainsKey(item))
                    statistica[item] = 0;

                statistica[item]++;

                if (statistica[item] > 255) // scriu pe 8 biti valoarea cat am voie sa pun numaratoare (daca depaseste pun val. 2^8)
                    statistica[item] = 255;
            }

            statistica = statistica.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
            statistica = statistica.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            stats = new Dictionary<byte, int>(statistica);
            return statistica;
        }

        private Tree CreateTree(Dictionary<byte, int> statistica)
        {
            Tree tr = new Tree();
            List<Tree> listTree = new List<Tree>();
            List<NodeType> nodeTypeList = new List<NodeType>();

            tr.left = new Tree();
            tr.left.key = statistica.ElementAt(0).Key;
            tr.left.value = statistica.ElementAt(0).Value;

            if (statistica.Count.Equals(0))
                return tr;

            tr.right = new Tree();
            tr.right.key = statistica.ElementAt(1).Key;
            tr.right.value = statistica.ElementAt(1).Value;
            tr.value = tr.left.value + tr.right.value;

            listTree.Add(tr);

            nodeTypeList.Add(new NodeType(NodeType.TREE, 0, tr.value));
            for (int i = 2; i < statistica.Count; i++)
                nodeTypeList.Add(new NodeType(NodeType.CHARACTER, i, statistica.ElementAt(i).Value));


            while (nodeTypeList.Count > 1)
            {
                nodeTypeList.OrderBy(x => x.value).ToList();
                tr = new Tree();
                tr.left = new Tree();
                tr.right = new Tree();
                int index0 = nodeTypeList[0].index;
                int index1 = nodeTypeList[1].index;
                if (nodeTypeList[0].type == NodeType.TREE)
                {
                    tr.left.value = listTree[index0].value;
                    tr.left.left = listTree[index0].left;
                    tr.left.right = listTree[index0].right;
                }
                else
                {
                    tr.left.key = statistica.ElementAt(index0).Key;
                    tr.left.value = statistica.ElementAt(index0).Value;
                }

                if (nodeTypeList[1].type == NodeType.TREE)
                {
                    tr.right.value = listTree[index1].value;
                    tr.right.left = listTree[index1].left;
                    tr.right.right = listTree[index1].right;
                }
                else
                {
                    tr.right.key = statistica.ElementAt(index1).Key;
                    tr.right.value = statistica.ElementAt(index1).Value;
                }

                tr.value = tr.left.value + tr.right.value;

                nodeTypeList.RemoveAt(0);
                nodeTypeList.RemoveAt(0);

                listTree.Add(tr);
                nodeTypeList.Add(new NodeType(NodeType.TREE, listTree.Count - 1, listTree.ElementAt(listTree.Count - 1).value));
            }    
            return listTree.ElementAt(nodeTypeList[0].index);
        }

        private void CreateModelInorder(Tree node)
        {
            if (node == null)
            {
                if (TreeCodesInorder.Count > 0)
                    TreeCodesInorder.RemoveAt(TreeCodesInorder.Count - 1);
                return;
            }

            TreeCodesInorder.Add(0);
            CreateModelInorder(node.left);

            if (node.left == null && node.right == null)
            {
                //Console.Write(node.key + " -> ");
                //foreach (var item in TreeCodesInorder)
                //    Console.Write(item + " ");
                //Console.WriteLine();
                model.Add(node.key, new List<byte>(TreeCodesInorder));
            }

            TreeCodesInorder.Add(1);
            CreateModelInorder(node.right);

            if (TreeCodesInorder.Count > 0)
                TreeCodesInorder.RemoveAt(TreeCodesInorder.Count - 1);
        }

        public List<byte> Encode(byte[] input)
        {
            this.TreeCodesInorder = new List<byte>();
            this.model = new Dictionary<byte, List<byte>>();

            Dictionary<byte, int> statistica = CreateStatistic(input);
            Tree tree = CreateTree(statistica);
            CreateModelInorder(tree);

            List<byte> codedText = new List<byte>();
            foreach (byte item in input)
            {
                foreach (var dicItem in model)
                {
                    if (dicItem.Key == item)
                    {
                        foreach (byte bits in dicItem.Value)
                            codedText.Add(bits);
                    }
                }
            }

            return codedText;
        }

        public List<byte> Decode(List<byte> input, Dictionary<byte, int> statistica)
        {
            List<byte> codes = new List<byte>();
            var statis = statistica.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            Tree tree = CreateTree(statis); // statistica aici

            Tree newTr = tree;
            for (int i = 0; i < input.Count; i++)
            {
                Tree newTree = new Tree();
                if (input[i] == 0)
                {
                    if (newTr.left != null)
                        newTr = newTr.left;
                }
                else if (input[i] == 1)
                {
                    if (newTr.right != null)
                        newTr = newTr.right;
                }
                if (newTr.left == null && newTr.right == null)
                {
                    codes.Add(newTr.key);
                    newTr = tree;
                }
            }
            return codes;
        }
    }

    class TesterInput
    {
        public byte[] input1 = { 65, 66, 67, 68, 69, 67, 68, 69, 67, 68 };
        public byte[] input2 = { 65, 66, 67, 68, 69, 67, 65, 66, 67, 68, 69, 67, 65, 66, 67, 68, 69, 67 };
        public byte[] input3 = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        public byte[] input4 = { 1, 0, 1, 0, 1, 0, 10, 12 };
        public byte[] input5 = { 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 4, 1, 1, 1 };
        public byte[] input6 = { 2, 1, 2, 1, 2, 2, 2, 1, 1, 2 };
        public byte[] input7 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 1 };
        public byte[] input8 = { 68, 65, 66, 66, 66, 66, 68, 66, 66, 66, 90, 66, 68, 65, 69 };
        public byte[] input9 = { 65, 66, 67, 68, 65, 66, 67, 68, 65, 66, 68, 67, 65, 66, 67, 68, 65, 66, 68, 68, 67, 68 };
    }
}