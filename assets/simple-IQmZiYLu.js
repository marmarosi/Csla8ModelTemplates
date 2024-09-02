const e=`---
title: Simple Models\r
slug: simple\r
description: Simple Models Description\r
position: 2
---

<p><img src="/images/simple.png" alt="Simple model" title="Simple model"></p>
<p>The simple models implement the use cases of a one-level entity.
Root entity: Team.</p>
<h3 id="simple-list">Simple List</h3>
<p>The simple list template implements a read-only business collection.</p>
<table>
<thead>
<tr>
<th>Component</th>
<th>Description</th>
</tr>
</thead>
<tbody><tr>
<td>SimpleTeamList</td>
<td>read-only root collection</td>
</tr>
<tr>
<td>SimpleTeamListItem</td>
<td>read-only child object</td>
</tr>
</tbody></table>
<p>Endpoint:</p>
<ul>
<li><input disabled="" type="checkbox"> GET /api/simple --- <em>Gets a list of teams.</em></li>
</ul>
<h3 id="simple-view">Simple View</h3>
<p>The simple view template implements a read-only business object.</p>
<table>
<thead>
<tr>
<th>Component</th>
<th>Description</th>
</tr>
</thead>
<tbody><tr>
<td>SimpleTeamView</td>
<td>read-only root object</td>
</tr>
</tbody></table>
<p>Endpoint:</p>
<ul>
<li><input disabled="" type="checkbox"> GET /api/simple/:id/view --- <em>Gets the specified team details to display.</em></li>
</ul>
<h3 id="simple-edit">Simple Edit</h3>
<p>The simple edit template implements an editable business object.</p>
<table>
<thead>
<tr>
<th>Component</th>
<th>Description</th>
</tr>
</thead>
<tbody><tr>
<td>SimpleTeam</td>
<td>read-only editable object</td>
</tr>
</tbody></table>
<p>Endpoints:</p>
<ul>
<li><input disabled="" type="checkbox"> GET /api/simple/new --- <em>Gets e new team to edit.</em><br/></li>
<li><input disabled="" type="checkbox"> POST /api/simple --- <em>Creates a new team.</em><br/></li>
<li><input disabled="" type="checkbox"> GET /api/simple/:id --- <em>Gets the specified team to edit.</em><br/></li>
<li><input disabled="" type="checkbox"> PUT /api/simple --- <em>Updates the specified team.</em><br/></li>
<li><input disabled="" type="checkbox"> DELETE /api/simple --- <em>Deletes the specified team.</em></li>
</ul>
<h3 id="simple-set">Simple Set</h3>
<p>The simple set template implements an editable business collection.</p>
<table>
<thead>
<tr>
<th>Component</th>
<th>Description</th>
</tr>
</thead>
<tbody><tr>
<td>SimpleTeamSet</td>
<td>editable root collection</td>
</tr>
<tr>
<td>SimpleTeamSetItem</td>
<td>editable child object</td>
</tr>
</tbody></table>
<p>Endpoints:</p>
<ul>
<li><input disabled="" type="checkbox"> GET /api/simple/set --- <em>Gets the specified team set to edit.</em><br/></li>
<li><input disabled="" type="checkbox"> PUT /api/simple/set --- <em>Updates the specified team set.</em></li>
</ul>
<h3 id="simple-command">Simple Command</h3>
<p>The simple command template implements a command object.</p>
<table>
<thead>
<tr>
<th>Component</th>
<th>Description</th>
</tr>
</thead>
<tbody><tr>
<td>RenameTeam</td>
<td>command object</td>
</tr>
</tbody></table>
<p>Endpoint:</p>
<ul>
<li><input disabled="" type="checkbox"> PATCH /api/simple --- <em>Renames the specified team.</em></li>
</ul>
`;export{e as default};
