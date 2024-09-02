const e=`---
title: Tree Model\r
slug: tree\r
description: Tree Model Description\r
position: 7
---

<h1 id="tree-model">Tree Model</h1>
<p><img src="/images/tree.png" alt="Tree model" title="Tree model"></p>
<h3 id="tree">Tree</h3>
<p>The tree templates implements a special version of the complex list where
all child objects are the same as the root object.</p>
<table>
<thead>
<tr>
<th>Component</th>
<th>Description</th>
</tr>
</thead>
<tbody><tr>
<td>FolderTree</td>
<td>read-only root object</td>
</tr>
<tr>
<td>FolderNodes</td>
<td>read-only child collection</td>
</tr>
<tr>
<td>FolderNode</td>
<td>read-only child object</td>
</tr>
</tbody></table>
<p>Endpoint:</p>
<ul>
<li><input disabled="" type="checkbox"> GET /api/tree/:id --- <em>Gets the specified folder tree.</em></li>
</ul>
`;export{e as default};
