using System;
using System.IO;

namespace Lab.TreeTest
{
    public class Main
    {
        public static void Run()
        {

            Console.WriteLine("End");
        }

        private static void RunTreeDic()
        {
            var root = new TreeNodeInstance(TreeNodeType.AppData)
            {
                new TreeNodeInstance("微信", () => new TreeNode("微信", ResType.AppIcons_WeChat))
                {
                    {
                        "账号信息",
                        () => new TreeNode("账号信息", ResType.TreeIcons_Account)
                        {
                            ViewService = new object(),
                        }
                    },
                    {
                        "登录设备",
                        () => new TreeNode("登录设备", ResType.TreeIcons_LoginDevice)
                        {
                            ViewService = new object(),
                        }
                    },
                    new TreeNodeInstance("钱包", () => new TreeNode("钱包", ResType.TreeIcons_Wallet))
                    {
                        {
                            "银行卡",
                            () => new TreeNode("银行卡", ResType.TreeIcons_BankCard)
                            {
                                ViewService = new object(),
                            }
                        },
                        {
                            "零钱",
                            () => new TreeNode("零钱", ResType.TreeIcons_BankCard)
                            {
                                ViewService = new object(),
                            }
                        }
                    },
                }
            };




        }

        private static void RunSaveTree()
        {

            var root = new TreeNode
            {
                Name = "Name1",
                Header = "Header1",
            };
            var item2 = new TreeNode
            {
                Name = "Name2",
                Header = "Header2"
            };
            var item3 = new TreeNode
            {
                Name = "Name3",
                Header = "Header3"
            };
            var item4 = new TreeNode
            {
                Name = "Name4",
                Header = "Header4"
            };
            var item5 = new TreeNode
            {
                Name = "Name5",
                Header = "Header5"
            };
            root.AddItem(item2);
            root.AddItem(item3);
            item3.AddItem(item4);
            item3.AddItem(item5);

            SaveTree(root);

        }

        private static void SaveTree(TreeNode root)
        {
            using var file = File.Open("tree.bin", FileMode.Create);
            using var writer = new BinaryWriter(file);
            SaveTree(writer, root);
        }

        private static void SaveTree(BinaryWriter writer, TreeNode node)
        {
            // 当前节点ID | 父节点下标 | 数据域大小 | 数据
            //writer.Write(node.Id);
            //writer.Write(node.Parent?.Id ?? 0);
            writer.Write(node.Name);
            writer.Write(node.Header);
            node.Items?.ForEach(o => SaveTree(writer, o));
        }

    }
}
