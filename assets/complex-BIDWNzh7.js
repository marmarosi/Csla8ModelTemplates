const t=`---
title: Complex Models\r
slug: complex\r
description: Complex Models Description\r
position: 5
---

<p><img src="/images/complex.png" alt="Complex model" title="Complex model"></p>
<p>The complex models implement the use cases of a multi-level entity.
Root entity: Team, child entity: Player.</p>
<h3 id="complex-list">Complex List</h3>
<p>The complex list template implements a read-only business collection where
all collection elements have a read-only child collection.</p>
<table>
<thead>
<tr>
<th>Component</th>
<th>Description</th>
</tr>
</thead>
<tbody><tr>
<td>TeamList</td>
<td>read-only root collection</td>
</tr>
<tr>
<td>TeamListItem</td>
<td>read-only child object</td>
</tr>
<tr>
<td>TeamListPlayers</td>
<td>read-only child collection</td>
</tr>
<tr>
<td>TeamListPlayer</td>
<td>read-only child object</td>
</tr>
</tbody></table>
<p>Endpoint:</p>
<ul>
<li><input disabled="" type="checkbox"> GET /api/complex --- <em>Gets a list of teams.</em></li>
</ul>
<h3 id="complex-view">Complex View</h3>
<p>The complex view template implements a read-only business object with
a read-only child collection.</p>
<table>
<thead>
<tr>
<th>Component</th>
<th>Description</th>
</tr>
</thead>
<tbody><tr>
<td>TeamView</td>
<td>read-only root object</td>
</tr>
<tr>
<td>TeamViewPlayers</td>
<td>read-only child collection</td>
</tr>
<tr>
<td>TeamViewPlayer</td>
<td>read-only child object</td>
</tr>
</tbody></table>
<p>Endpoint:</p>
<ul>
<li><input disabled="" type="checkbox"> GET /api/complex/:id/view --- <em>Gets the specified team details to display.</em></li>
</ul>
<h3 id="complex-edit">Complex Edit</h3>
<p>The complex edit template implements an editable business object with
an editable child collection.</p>
<table>
<thead>
<tr>
<th>Component</th>
<th>Description</th>
</tr>
</thead>
<tbody><tr>
<td>Team</td>
<td>editable root object</td>
</tr>
<tr>
<td>TeamPlayers</td>
<td>editable child collection</td>
</tr>
<tr>
<td>TeamPlayer</td>
<td>editable child object</td>
</tr>
</tbody></table>
<p>Endpoints:</p>
<ul>
<li><input disabled="" type="checkbox"> GET /api/complex/new --- <em>Gets e new team to edit.</em></li>
<li><input disabled="" type="checkbox"> POST /api/complex --- <em>Creates a new team.</em></li>
<li><input disabled="" type="checkbox"> GET /api/complex/:id --- <em>Gets the specified team to edit.</em></li>
<li><input disabled="" type="checkbox"> PUT /api/complex --- <em>Updates the specified team.</em></li>
<li><input disabled="" type="checkbox"> DELETE /api/complex --- <em>Deletes the specified team.</em></li>
</ul>
<h3 id="complex-set">Complex Set</h3>
<p>The complex set template implements an editable business collection
where all collection elements have an editable child collection.</p>
<table>
<thead>
<tr>
<th>Component</th>
<th>Description</th>
</tr>
</thead>
<tbody><tr>
<td>TeamSet</td>
<td>editable root collection</td>
</tr>
<tr>
<td>TeamSetItem</td>
<td>editable child object</td>
</tr>
<tr>
<td>TeamSetPlayers</td>
<td>editable child collection</td>
</tr>
<tr>
<td>TeamSetPlayer</td>
<td>editable child object</td>
</tr>
</tbody></table>
<p>Endpoints:</p>
<ul>
<li><input disabled="" type="checkbox"> GET /api/complex/set --- <em>Gets the specified team set to edit.</em></li>
<li><input disabled="" type="checkbox"> PUT /api/complex/set --- <em>Updates the specified team set.</em></li>
</ul>
<h3 id="complex-command">Complex Command</h3>
<p>The complex command template implements a command object with a resulting
read-only child collection.</p>
<table>
<thead>
<tr>
<th>Component</th>
<th>Description</th>
</tr>
</thead>
<tbody><tr>
<td>CountTeams</td>
<td>command object</td>
</tr>
<tr>
<td>CountTeamsResults</td>
<td>read-only child collection</td>
</tr>
<tr>
<td>CountTeamsResult</td>
<td>read-only child object</td>
</tr>
</tbody></table>
<p>Endpoint:</p>
<ul>
<li><input disabled="" type="checkbox"> PATCH /api/complex --- <em>Counts the teams grouped by the number of their items.</em></li>
</ul>
`;export{t as default};
