﻿using Csla;
using Csla8ModelTemplates.Contracts.Junction.Edit;
using Csla8RestApi.Models;

namespace Csla8ModelTemplates.Models.Junction.Edit
{
    /// <summary>
    /// Represents an editable group-person collection.
    /// </summary>
    [Serializable]
    public class GroupPersons : EditableList<GroupPersons, GroupPerson, GroupPersonDto>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(GroupPersons),
        //        new IsInRole(
        //            AuthorizationActions.GetObject,
        //            "Manager"
        //            )
        //        );
        //}

        #endregion

        #region Data Access

        [FetchChild]
        private void Fetch(
            List<GroupPersonDao> list,
            [Inject] IChildDataPortal<GroupPerson> itemPortal
            )
        {
            foreach (var item in list)
                Add(itemPortal.FetchChild(item));
        }

        #endregion
    }
}
