using Csla;
using Csla.Data;
using Csla8RestApi.Dal.Contracts;
using Csla8RestApi.Models;
using Csla8RestApi.Tests.Contracts;
using Csla8RestApi.Tests.Contracts.Tree.View;

namespace Csla8RestApi.Tests.Models.Tree.View
{
    /// <summary>
    /// Represents an item in a read-only message tree.
    /// </summary>
    [Serializable]
    public class MessageNode : ReadOnlyModel<MessageNode>
    {
        #region Properties

        public static readonly PropertyInfo<long?> MessageKeyProperty = RegisterProperty<long?>(nameof(MessageKey));
        public long? MessageKey
        {
            get => GetProperty(MessageKeyProperty);
            private set => LoadProperty(MessageKeyProperty, value);
        }

        public static readonly PropertyInfo<long?> MessageIdProperty = RegisterProperty<long?>(nameof(MessageId), RelationshipTypes.PrivateField);
        public string MessageId
        {
            get => KeyHash.Encode(ID.Message, MessageKey);
            private set => MessageKey = KeyHash.Decode(ID.Message, value);
        }

        public static readonly PropertyInfo<long?> ParentKeyProperty = RegisterProperty<long?>(nameof(ParentKey));
        public long? ParentKey
        {
            get => GetProperty(ParentKeyProperty);
            private set => LoadProperty(ParentKeyProperty, value);
        }

        public static readonly PropertyInfo<long?> ParentIdProperty = RegisterProperty<long?>(nameof(ParentId), RelationshipTypes.PrivateField);
        public string ParentId
        {
            get => KeyHash.Encode(ID.Message, ParentKey);
            private set => ParentKey = KeyHash.Decode(ID.Message, value);
        }

        public static readonly PropertyInfo<string> MessageNameProperty = RegisterProperty<string>(nameof(MessageName));
        public string MessageName
        {
            get => GetProperty(MessageNameProperty);
            private set => LoadProperty(MessageNameProperty, value);
        }

        public static readonly PropertyInfo<int?> LevelProperty = RegisterProperty<int?>(nameof(Level));
        public int? Level
        {
            get => GetProperty(LevelProperty);
            private set => LoadProperty(LevelProperty, value);
        }

        public static readonly PropertyInfo<MessageNodes> ChildrenProperty = RegisterProperty<MessageNodes>(nameof(Children));
        public MessageNodes Children
        {
            get => GetProperty(ChildrenProperty);
            private set => LoadProperty(ChildrenProperty, value);
        }

        #endregion

        #region Business Rules

        //protected override void AddBusinessRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        new IsInRole(
        //            AuthorizationActions.ReadProperty,
        //            MessageNameProperty,
        //            "Manager"
        //            )
        //        );
        //}

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(MessageNode),
        //        new IsInRole(
        //            AuthorizationActions.GetObject,
        //            "Manager"
        //            )
        //        );
        //}

        #endregion

        #region Factory Methods

        //internal static MessageNode Fetch(MessageNodeDao dao)
        //{
        //    return DataPortal.FetchChild<MessageNode>(dao);
        //}

        #endregion

        #region Data Access

        [FetchChild]
        private void Fetch(
            MessageNodeDao dao,
            [Inject] IChildDataPortal<MessageNodes> itemsPortal
            )
        {
            // Load values from persistent storage.
            DataMapper.Map(dao, this, "Children", "MessageOrder");
            Children = itemsPortal.FetchChild(dao.Children);
        }

        #endregion
    }
}
