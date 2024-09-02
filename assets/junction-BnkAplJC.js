const n=`---
title: Junction Models\r
slug: junction\r
description: Junction Models Description\r
position: 6
---

<p><img src="/images/junction.png" alt="Junction model" title="Junction model"></p>
<p>The junction models implement the use cases of two entities having a
junction or bridging entity. Root entities: Group and Person, junction
entity: GroupPersons.</p>
<h3 id="junction-view">Junction View</h3>
<p>The junction view template implements a read-only business object with
a read-only member collection.</p>
<table>
<thead>
<tr>
<th>Component</th>
<th>Description</th>
</tr>
</thead>
<tbody><tr>
<td>GroupView</td>
<td>read-only root object</td>
</tr>
<tr>
<td>GroupViewPersons</td>
<td>read-only child collection</td>
</tr>
<tr>
<td>GroupViewPerson</td>
<td>read-only child object</td>
</tr>
</tbody></table>
<p>Endpoint:</p>
<ul>
<li><input disabled="" type="checkbox"> GET /api/junction/:id/view --- <em>Gets the specified group details to display.</em></li>
</ul>
<h3 id="junction-edit">Junction Edit</h3>
<p>The junction template implements an editable business object with
an editable member collection.</p>
<table>
<thead>
<tr>
<th>Component</th>
<th>Description</th>
</tr>
</thead>
<tbody><tr>
<td>Group</td>
<td>editable root object</td>
</tr>
<tr>
<td>GroupPersons</td>
<td>editable child collection</td>
</tr>
<tr>
<td>GroupPerson</td>
<td>editable child object</td>
</tr>
</tbody></table>
<p>Endpoints:</p>
<ul>
<li><input disabled="" type="checkbox"> GET /api/junction/new --- <em>Gets a new group to edit.</em></li>
<li><input disabled="" type="checkbox"> POST /api/junction --- <em>Creates a new group.</em></li>
<li><input disabled="" type="checkbox"> GET /api/junction/:id --- <em>Gets the specified group to edit.</em></li>
<li><input disabled="" type="checkbox"> PUT /api/junction --- <em>Updates the specified group.</em></li>
<li><input disabled="" type="checkbox"> DELETE /api/junction --- <em>Deletes the specified group.</em></li>
</ul>
`;export{n as default};
