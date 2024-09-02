const t=`---
title: Arrangement\r
slug: arrangement\r
description: Arrangement Description\r
position: 3
---

<p>The following templates provides variations of the simple list.</p>
<h3 id="sorted-list">Sorted List</h3>
<p>The sorted list template implements a read-only business collection
with sort options in the criteria.</p>
<table>
<thead>
<tr>
<th>Component</th>
<th>Description</th>
</tr>
</thead>
<tbody><tr>
<td>SortedTeamList</td>
<td>read-only root collection</td>
</tr>
<tr>
<td>SortedTeamListItem</td>
<td>read-only child object</td>
</tr>
</tbody></table>
<p>Endpoint:</p>
<ul>
<li><input disabled="" type="checkbox"> GET /api/pagination/sorted --- <em>Gets the specified teams sorted.</em></li>
</ul>
<h3 id="paginated-list">Paginated List</h3>
<p>The paginated list template implements a read-only business collection
with pagination options in the criteria. The Data property of the root
object contains a page of the list, while the TotalCount property returns
the count of all items that match the criteria.</p>
<table>
<thead>
<tr>
<th>Component</th>
<th>Description</th>
</tr>
</thead>
<tbody><tr>
<td>PaginatedTeamList</td>
<td>read-only root object</td>
</tr>
<tr>
<td>PaginatedTeamListItems</td>
<td>read-only child collection</td>
</tr>
<tr>
<td>PaginatedTeamListItem</td>
<td>read-only child object</td>
</tr>
</tbody></table>
<p>Endpoint:</p>
<ul>
<li><input disabled="" type="checkbox"> GET /api/pagination/paginated --- <em>Gets the specified page of teams.</em></li>
</ul>
<h3 id="arranged-list">Arranged List</h3>
<p>The arranged list template implements a read-only business collection with
pagination and sort options in the criteria. The Data property of the root
object contains a page of the list, while the TotalCount property returns
the count of all items that match the criteria.</p>
<table>
<thead>
<tr>
<th>Component</th>
<th>Description</th>
</tr>
</thead>
<tbody><tr>
<td>ArrangedTeamList</td>
<td>read-only root object</td>
</tr>
<tr>
<td>ArrangedTeamListItems</td>
<td>read-only child collection</td>
</tr>
<tr>
<td>ArrangedTeamListItem</td>
<td>read-only child object</td>
</tr>
</tbody></table>
<p>Endpoint:</p>
<ul>
<li><input disabled="" type="checkbox"> GET /api/pagination/arranged --- <em>Gets the specified page of sorted teams.</em></li>
</ul>
`;export{t as default};
