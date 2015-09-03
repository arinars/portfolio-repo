using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Lemon.Lib.Easyui
{
    #region
    /* DB에서 가져온 데이터를 아래와 같은 계층형 구조로 만든다.
     * http://www.jeasyui.com/documentation/index.php 에서 tree 메뉴 참조.
     [{
        "id":1,
        "text":"Folder1",
        "iconCls":"icon-save",
        "children":[{
            "text":"File1",
            "checked":true
        },{
            "text":"Books",
            "state":"open",
            "attributes":{
                "url":"/demo/book/abc",
                "price":100
            },
            "children":[{
                "text":"PhotoShop",
                "checked":true
            },{
                "id": 8,
                "text":"Sub Bookds",
                "state":"closed"
            }]
        }]
    },{
        "text":"Languages",
        "state":"closed",
        "children":[{
            "text":"Java"
        },{
            "text":"C#"
        }]
    }]
    */
    #endregion

    public class Node<T>
    {
        /// <summary>
        /// Node의 키값.
        /// </summary>
        public object id { get; set; }
        /// <summary>
        /// sets the item's label.
        /// </summary>
        public object label { get; set; }
        /// <summary>
        /// sets the item's value. 계층 구조로 변경 전의 객체.
        /// </summary>
        public T value { get; set; }
        /// <summary>
        /// item's html. The html to be displayed in the item.
        /// </summary>
        public string html { get; set; }
        /// <summary>
        /// sets whether the item is enabled/disabled.
        /// </summary>
        public bool disabled { get; set; }
        /// <summary>
        /// sets whether the item is checked/unchecked(when checkboxes are enabled).
        /// </summary>
        public bool @checked { get; set; }
        /// <summary>
        /// sets whether the item is expanded or collapsed.
        /// </summary>
        public bool expanded { get; set; }
        /// <summary>
        /// sets whether the item is selected.
        /// </summary>
        public bool selected { get; set; }
        /// <summary>
        /// sets the item's icon(url is expected).
        /// </summary>
        public string icon { get; set; }
        /// <summary>
        /// sets the size of the item's icon.
        /// </summary>
        public int iconsize { get; set; }
        /// <summary>
        /// sets an array of sub items.
        /// </summary>
        public List<Node<T>> items { get; set; }
    }

    public class EasyuiNode<T>
    {
        public object id { get; set; }
        public object parentId { get; set; }
        public string text { get; set; }
        public T attributes { get; set; }
        public string iconCls { get; set; }
        /// <summary>
        /// open, closed
        /// </summary>
        public string state { get; set; }
        public bool @checked { get; set; }
        public List<EasyuiNode<T>> children;
    }

    public static class IEnumerableExpansion
    {
        private static Node<T> GetChild<T>(this IEnumerable<T> list, Node<T> node, PropertyInfo idProp, PropertyInfo parentIdProp,
            PropertyInfo textProp, object aSelectedId, int aExpandLevel, int aLevel)
        {
            if (aLevel > 10)
            {
                return node;
            }
            IEnumerable<T> childList = list.Where(x => Convert.ToString(node.id) == Convert.ToString(parentIdProp.GetValue(x, null))).ToArray(); //현재 node id의 자식 node들을 가져온다
            node.items = new List<Node<T>>();
            if (childList.Count() != 0)
            { //자식이 있는 경우
                foreach (T data in childList)
                {
                    Node<T> lNode = new Node<T>();
                    lNode.id = Convert.ToString(idProp.GetValue(data, null));
                    lNode.value = data;
                    lNode.label = Convert.ToString(textProp.GetValue(data, null));
                    if (aExpandLevel >= aLevel)
                    {
                        lNode.expanded = true;
                    }
                    if (Convert.ToString(aSelectedId) == Convert.ToString(lNode.id))
                    {
                        lNode.selected = true;
                    }
                    node.items.Add(list.GetChild(lNode, idProp, parentIdProp, textProp, aSelectedId, aExpandLevel, aLevel + 1));
                }
            }
            else
            { // 자식이 없는 경우
                //node.iconCls = fileIconClass;
            }
            return node;
        }
        /// <summary>
        /// 제네릭 타입의 리스트를 jQWidgets 포멧의 트리구조로 변경
        /// </summary>
        /// <param name="idFieldName">현재 노드의 값이 들어갈 필드 이름 : ex) "MAINCODE"</param>
        /// <param name="parentIdFieldName">부모 노드의 값이 들어갈 필드 이름 : ex) "SUBCODE"</param>
        /// <param name="textFieldName">현재 노드의 내용이 들어갈 필드 이름 ex) "CODENAME"</param>
        /// <param name="folderIconClass">Easyui Tree에서 상위 노드 아이콘 클래스 이름. ""인 경우 디폴트 이미지로 설정됨: ex) "icon-ok" </param>
        /// <param name="fileIconClass">Easyui Tree에서 말단 노드 아이콘 클래스 이름. ""인 경우 디폴트 이미지로 설정됨 : ex) "icon-reload" </param>
        /// <param name="bEncode"></param>
        /// <returns></returns>
        public static IEnumerable<Node<T>> ToJQWidgetsTree<T>(this IEnumerable<T> aList, string aIdFieldName, string aParentIdFieldName,
            string aLabelFieldName, int aExpandLevel, T aFirstRow, object aSelectedId)
        {
            PropertyInfo lIdProp = typeof(T).GetProperty(aIdFieldName);
            PropertyInfo lParentidProp = typeof(T).GetProperty(aParentIdFieldName);
            PropertyInfo lLabelProp = typeof(T).GetProperty(aLabelFieldName);
            int lLevel = 1;

            List<T> lRootList = new List<T>();
            foreach (var row in aList)
            {
                if (aList.Count(x => !x.Equals(row) && Convert.ToString(lParentidProp.GetValue(row, null)) == Convert.ToString(lIdProp.GetValue(x, null))) == 0)
                {
                    lRootList.Add(row);
                }
            }

            List<Node<T>> rtnList = new List<Node<T>>();
            if (aFirstRow != null)
            {
                Node<T> lFirstNode = new Node<T>();
                lFirstNode.id = Convert.ToString(lIdProp.GetValue(aFirstRow, null));
                lFirstNode.value = aFirstRow;
                if (aSelectedId == null)
                {
                    lFirstNode.selected = true;
                }
                lFirstNode.label = Convert.ToString(lLabelProp.GetValue(aFirstRow, null));
                rtnList.Add(lFirstNode);
            }
            foreach (T root in lRootList)
            {
                Node<T> lRootNode = new Node<T>();
                lRootNode.id = Convert.ToString(lIdProp.GetValue(root, null));
                lRootNode.value = root;
                lRootNode.label = Convert.ToString(lLabelProp.GetValue(root, null));
                if (aExpandLevel >= lLevel)
                {
                    lRootNode.expanded = true;
                }
                if (Convert.ToString(aSelectedId) == Convert.ToString(lRootNode.id))
                {
                    lRootNode.selected = true;
                }
                rtnList.Add(aList.GetChild(lRootNode, lIdProp, lParentidProp, lLabelProp, aSelectedId, aExpandLevel, lLevel + 1));
            }

            return rtnList.ToArray();
        }

        public static IEnumerable<T> AddJQWidgetsTitle<T>(this IEnumerable<T> aList, string aTitleFieldName, string aTitle)
        {
            T lTitleObject = Activator.CreateInstance<T>();
            typeof(T).GetProperty(aTitleFieldName).SetValue(lTitleObject, aTitle, null);
            IEnumerable<T> lFirstList = new T[] { lTitleObject };
            return lFirstList.Concat(aList);
        }

        private static EasyuiNode<T> GetChild<T>(this IEnumerable<T> list, EasyuiNode<T> node, PropertyInfo idProp, PropertyInfo parentIdProp,
            PropertyInfo textProp, PropertyInfo aIconProp, PropertyInfo aCheckedProp, int aExpandLevel, int aLevel)
        {
            if (aLevel > 10)
            {
                return node;
            }

            IEnumerable<T> childList = list.Where(x => Convert.ToString(node.id) == Convert.ToString(parentIdProp.GetValue(x, null))).ToArray(); //현재 node id의 자식 node들을 가져온다
            node.children = new List<EasyuiNode<T>>();
            if (childList.Count() != 0)
            { //자식이 있는 경우
                foreach (T data in childList)
                {
                    EasyuiNode<T> _node = new EasyuiNode<T>();
                    _node.id = Convert.ToString(idProp.GetValue(data, null));
                    _node.parentId = Convert.ToString(parentIdProp.GetValue(data, null));
                    if (aIconProp != null)
                    {
                        _node.iconCls = Convert.ToString(aIconProp.GetValue(data, null));
                    }
                    if (aCheckedProp != null)
                    {
                        _node.@checked = Convert.ToBoolean(aCheckedProp.GetValue(data, null));
                    }
                    _node.attributes = data;
                    _node.text = Convert.ToString(textProp.GetValue(data, null));
                    if (aExpandLevel > 0)
                    {
                        if (aExpandLevel > aLevel)
                        {
                            _node.state = "open";
                        }
                        else
                        {
                            _node.state = "closed";
                        }
                    }
                    node.children.Add(list.GetChild(_node, idProp, parentIdProp, textProp, aIconProp, aCheckedProp, aExpandLevel, aLevel + 1));
                }
            }
            else
            { // 자식이 없는 경우
                node.state = "open";
                //node.iconCls = fileIconClass;
            }
            return node;
        }
        /// <summary>
        /// 제네릭 타입의 리스트를 Easy UI 포멧의 트리구조로 변경
        /// </summary>
        /// <param name="idFieldName">현재 노드의 값이 들어갈 필드 이름 : ex) "MAINCODE"</param>
        /// <param name="parentIdFieldName">부모 노드의 값이 들어갈 필드 이름 : ex) "SUBCODE"</param>
        /// <param name="textFieldName">현재 노드의 내용이 들어갈 필드 이름 ex) "CODENAME"</param>
        /// <param name="folderIconClass">Easyui Tree에서 상위 노드 아이콘 클래스 이름. ""인 경우 디폴트 이미지로 설정됨: ex) "icon-ok" </param>
        /// <param name="fileIconClass">Easyui Tree에서 말단 노드 아이콘 클래스 이름. ""인 경우 디폴트 이미지로 설정됨 : ex) "icon-reload" </param>
        /// <param name="bEncode"></param>
        /// <returns></returns>
        public static IEnumerable<EasyuiNode<T>> ToEasyuiTree<T>(this IEnumerable<T> aList, string aIdPropertyName, string aParentIdPropertyName,
            string aTextPropertyName, string aIconPropertyName, string aCheckedPropertyName, int aExpandLevel, string aTitle)
        {
            PropertyInfo lIdProp = typeof(T).GetProperty(aIdPropertyName ?? "");
            PropertyInfo lParentidProp = typeof(T).GetProperty(aParentIdPropertyName ?? "");
            PropertyInfo lTextProp = typeof(T).GetProperty(aTextPropertyName ?? "");
            PropertyInfo lIconProp = typeof(T).GetProperty(aIconPropertyName ?? "");
            PropertyInfo lCheckedProp = typeof(T).GetProperty(aCheckedPropertyName ?? "");
            int lLevel = 1;

            List<T> lRootList = new List<T>();
            foreach (var row in aList)
            {
                if (aList.Count(x => !x.Equals(row) && Convert.ToString(lParentidProp.GetValue(row, null)) == Convert.ToString(lIdProp.GetValue(x, null))) == 0)
                {
                    lRootList.Add(row);
                }
            }

            List<EasyuiNode<T>> rtnList = new List<EasyuiNode<T>>();
            if (aTitle != null)
            {
                EasyuiNode<T> lFirstNode = new EasyuiNode<T>();
                lFirstNode.id = -1;
                lFirstNode.iconCls = "icon-hide";
                lFirstNode.text = aTitle;
                rtnList.Add(lFirstNode);
            }
            foreach (T root in lRootList)
            {
                EasyuiNode<T> lRootNode = new EasyuiNode<T>();
                lRootNode.id = Convert.ToString(lIdProp.GetValue(root, null));
                lRootNode.parentId = Convert.ToString(lParentidProp.GetValue(root, null));
                if (lIconProp != null)
                {
                    lRootNode.iconCls = Convert.ToString(lIconProp.GetValue(root, null));
                }
                if (lCheckedProp != null)
                {
                    lRootNode.@checked = Convert.ToBoolean(lCheckedProp.GetValue(root, null));
                }
                lRootNode.attributes = root;
                lRootNode.text = Convert.ToString(lTextProp.GetValue(root, null));
                if (aExpandLevel > 0)
                {
                    if (aExpandLevel >= lLevel)
                    {
                        lRootNode.state = "open";
                    }
                    else
                    {
                        lRootNode.state = "closed";
                    }
                }
                rtnList.Add(aList.GetChild(lRootNode, lIdProp, lParentidProp, lTextProp, lIconProp, lCheckedProp, aExpandLevel, lLevel + 1));
            }
            return rtnList.ToArray();
        }

    }
}
