const t=`---
title: Selection\r
slug: selection\r
description: Selection Description\r
position: 4
---

<h1 id="selection">Selection</h1>
<p>The selection templates provides simplified versions of the simple list that
can be used e.g. in drop-down lists. The items of the selection list have
properties for a value and a description only.</p>
<h3 id="choice-by-key">Choice by Key</h3>
<p>The template implements the selection list with a value property named Key
whose data type is number.</p>
<table>
<thead>
<tr>
<th>Component</th>
<th>Description</th>
</tr>
</thead>
<tbody><tr>
<td>TeamKeyChoice</td>
<td>read-only root collection</td>
</tr>
<tr>
<td>KeyNameOption</td>
<td>read-only child object</td>
</tr>
</tbody></table>
<p>Endpoint:</p>
<ul>
<li><input disabled="" type="checkbox"> GET /api/selection/with-key --- <em>Gets the key-name choice of the teams.</em></li>
</ul>
<h3 id="choice-by-id">Choice by ID</h3>
<p>The template implements the selection list with a value property name Id
whose data type is string. The Id property contains the encrypted Key value.</p>
<table>
<thead>
<tr>
<th>Component</th>
<th>Description</th>
</tr>
</thead>
<tbody><tr>
<td>TeamIdChoice</td>
<td>read-only root collection</td>
</tr>
<tr>
<td>IdNameOption</td>
<td>read-only child object</td>
</tr>
</tbody></table>
<p>Endpoint:</p>
<ul>
<li><input disabled="" type="checkbox"> GET /api/selection/with-id --- <em>Gets the id-name choice of the teams.</em></li>
</ul>
<h3 id="choice-by-guid">Choice by GUID</h3>
<p>The template implements the selection list with a value property name Guid
whose data type is Guid.</p>
<table>
<thead>
<tr>
<th>Component</th>
<th>Description</th>
</tr>
</thead>
<tbody><tr>
<td>TeamGuidChoice</td>
<td>read-only root collection</td>
</tr>
<tr>
<td>GuidNameOption</td>
<td>read-only child object</td>
</tr>
</tbody></table>
<p>Endpoint:</p>
<ul>
<li><input disabled="" type="checkbox"> GET /api/selection/with-guid --- <em>Gets the guid-name choice of the teams.</em></li>
</ul>
<h3 id="choice-by-code">Choice by Code</h3>
<p>The template implements the selection list with a value property name Code
whose data type is string.</p>
<table>
<thead>
<tr>
<th>Component</th>
<th>Description</th>
</tr>
</thead>
<tbody><tr>
<td>TeamCodeChoice</td>
<td>read-only root collection</td>
</tr>
<tr>
<td>CodeNameOption</td>
<td>read-only child object</td>
</tr>
</tbody></table>
<p>Endpoint:</p>
<ul>
<li><input disabled="" type="checkbox"> GET /api/selection/with-code --- <em>Gets the code-name choice of the teams.</em></li>
</ul>
`;export{t as default};
