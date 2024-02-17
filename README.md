# CSLA version 8 Model Templates for Web APIs

## Development in progress...

Provides CSLA model templates to quickly setup new models, data access and contracts
for .NET Core 8 REST APIs. Provide data access templates for __DB2__, __Firebird__,
__MySQL__, __Oracle__, __PostgreSQL__, __SQLite__ and __SQL Server__ databases.

Category | Models
---|---
[Simple Models](#simple-models) | [Simple List](#simple-list), [Simple View](#simple-view), [Simple Edit](#simple-edit), [Simple Set](#simple-set), [Simple Command](#simple-command)
[Arrangement](#arrangement) | [Sorted List](#sorted-list), [Paginated List](#paginated-list), [Arranged List](#arranged-list)
[Selection](#selection) | [Selection With Key](#selection-with-key), [Selection With ID](#selection-with-id), [Selection With Code](#selection-with-code)
[Complex Models](#complex-models) | [Complex List](#complex-list), [Complex View](#complex-view), [Complex Edit](#complex-edit), [Complex Set](#complex-set), [Complex Command](#complex-command)
[Tree Model](#tree-model) | [Tree](#tree)
[Junction Models](#junction-models) | [Junction View](#junction-view), [Junction](#junction)

## Simple Models

![Simple model](https://raw.githubusercontent.com/marmarosi/Csla8ModelTemplates/main/Images/Simple.png "Simple model")

The simple models implement the use cases of a one-level entity.
Root entity: Team.

### Simple List

The simple list template implements a read-only business collection.

Component | Description
--------- | -----------
SimpleTeamList | read-only root collection
SimpleTeamListItem | read-only child object

Endpoint:

- [ ] GET ​/api​/simple --- *Gets a list of teams.*

### Simple View

The simple view template implements a read-only business object.

Component | Description
--------- | -----------
SimpleTeamView | read-only root object

Endpoint:

- [ ] GET ​/api​/simple​/:id/view --- *Gets the specified team details to display.**

### Simple Edit

The simple edit template implements an editable business object.

Component | Description
--------- | -----------
SimpleTeam | read-only editable object

Endpoints:

- [ ] GET ​/api​/simple​/new --- *Gets e new team to edit.*<br>
- [ ] POST ​/api​/simple --- *Creates a new team.*<br>
- [ ] GET ​/api​/simple​/:id --- *Gets the specified team to edit.*<br>
- [ ] PUT ​/api​/simple --- *Updates the specified team.*<br>
- [ ] DELETE ​/api​/simple --- *Deletes the specified team.*

### Simple Set

The simple set template implements an editable business collection.

Component | Description
--------- | -----------
SimpleTeamSet | editable root collection
SimpleTeamSetItem | editable child object

Endpoints:

- [ ] GET ​/api​/simple​/set --- *Gets the specified team set to edit.*<br>
- [ ] PUT ​/api​/simple​/set --- *Updates the specified team set.*

### Simple Command

The simple command template implements a command object.

Component | Description
--------- | -----------
RenameTeam | command object

Endpoint:

- [ ] PATCH ​/api​/simple --- *Renames the specified team.*

## Arrangement

The following templates provides variations of the simple list.

### Sorted List

The sorted list template implements a read-only business collection
with sort options in the criteria.

Component | Description
--------- | -----------
SortedTeamList | read-only root collection
SortedTeamListItem | read-only child object

Endpoint:

- [ ] GET ​/api​/pagination​/sorted --- *Gets the specified teams sorted.**

### Paginated List

The paginated list template implements a read-only business collection
with pagination options in the criteria. The Data property of the root
object contains a page of the list, while the TotalCount property returns
the count of all items that match the criteria.

Component | Description
--------- | -----------
PaginatedTeamList | read-only root object
PaginatedTeamListItems | read-only child collection
PaginatedTeamListItem | read-only child object

Endpoint:

- [ ] GET ​/api​/pagination​/paginated --- *Gets the specified page of teams.**

### Arranged List

The arranged list template implements a read-only business collection with
pagination and sort options in the criteria. The Data property of the root
object contains a page of the list, while the TotalCount property returns
the count of all items that match the criteria.

Component | Description
--------- | -----------
ArrangedTeamList | read-only root object
ArrangedTeamListItems | read-only child collection
ArrangedTeamListItem | read-only child object

Endpoint:

- [ ] GET ​/api​/pagination​/arranged --- *Gets the specified page of sorted teams.**

## Selection

The selection templates provides more simplified versions of the simple list
that can be used e.g. in drop-down lists. The items of the selection list
have properties for a value and a description only.

### Selection With Key

The template implements the selection list with a value property named Key
whose data type is number.

Component | Description
--------- | -----------
TeamKeyChoice | read-only root collection
KeyNameOption | read-only child object

Endpoint:

- [ ] GET ​/api​/selection​/with-key --- *Gets the key-name choice of the teams.**

### Selection With ID

The template implements the selection list with a value property name Id
whose data type is string. The Id property contains the encrypted Key value.

Component | Description
--------- | -----------
TeamIdChoice | read-only root collection
IdNameOption | read-only child object

Endpoint:

- [ ] GET ​/api​/selection​/with-id --- *Gets the id-name choice of the teams.**

### Selection With Code

The template implements the selection list with a value property name Code
whose data type is string.

Component | Description
--------- | -----------
TeamCodeChoice | read-only root collection
CodeNameOption | read-only child object

Endpoint:

- [ ] GET ​/api​/selection​/with-code --- *Gets the code-name choice of the teams.**

## Complex Models

![Complex model](https://raw.githubusercontent.com/marmarosi/Csla8ModelTemplates/main/Images/Complex.png "Complex model")

The complex models implement the use cases of a multi-level entity.
Root entity: Team, child entity: Player.

### Complex List

The complex list template implements a read-only business collection where
all collection elements have a read-only child collection.

Component | Description
--------- | -----------
TeamList | read-only root collection
TeamListItem | read-only child object
TeamListPlayers | read-only child collection
TeamListPlayer | read-only child object

Endpoint:

- [ ] GET ​/api​/complex --- *Gets a list of teams.*

### Complex View

The complex view template implements a read-only business object with
a read-only child collection.

Component | Description
--------- | -----------
TeamView | read-only root object
TeamViewPlayers | read-only child collection
TeamViewPlayer | read-only child object

Endpoint:

- [ ] GET ​/api​/complex​/:id/view --- *Gets the specified team details to display.*

### Complex Edit

The complex edit template implements an editable business object with
an editable child collection.

Component | Description
--------- | -----------
Team | editable root object
TeamPlayers | editable child collection
TeamPlayer | editable child object

Endpoints:

- [ ] GET ​/api​/complex​/new --- *Gets e new team to edit.*
- [ ] POST ​/api​/complex --- *Creates a new team.*
- [ ] GET ​/api​/complex​/:id --- *Gets the specified team to edit.*
- [ ] PUT ​/api​/complex --- *Updates the specified team.*
- [ ] DELETE ​/api​/complex --- *Deletes the specified team.*

### Complex Set

The complex set template implements an editable business collection
where all collection elements have an editable child collection.

Component | Description
--------- | -----------
TeamSet | editable root collection
TeamSetItem | editable child object
TeamSetPlayers | editable child collection
TeamSetPlayer | editable child object

Endpoints:

- [ ] GET ​/api​/complex​/set --- *Gets the specified team set to edit.*
- [ ] PUT ​/api​/complex​/set --- *Updates the specified team set.*

### Complex Command

The complex command template implements a command object with a resulting
read-only child collection.

Component | Description
--------- | -----------
CountTeams | command object
CountTeamsResults | read-only child collection
CountTeamsResult | read-only child object

Endpoint:

- [ ] PATCH ​/api​/complex --- *Counts the teams grouped by the number of their items.*

## Tree Model

![Tree model](https://raw.githubusercontent.com/marmarosi/Csla8ModelTemplates/main/Images/Tree.png "Tree model")

### Tree

The tree templates implements a special version of the complex list where
all child objects are the same as the root object.

Component | Description
--------- | -----------
FolderTree | read-only root object
FolderNodes | read-only child collection
FolderNode | read-only child object

Endpoint:

- [ ] GET ​/api​/tree​/:id --- *Gets the specified folder tree.*

## Junction Models

![Junction model](https://raw.githubusercontent.com/marmarosi/Csla8ModelTemplates/main/Images/Junction.png "Junction model")

The junction models implement the use cases of two entities having a
junction or bridging entity. Root entities: Group and Person, junction
entity: GroupPersons.

### Junction View

The junction view template implements a read-only business object with
a read-only member collection.

Component | Description
--------- | -----------
GroupView | read-only root object
GroupViewPersons | read-only child collection
GroupViewPerson | read-only child object

Endpoint:

- [ ] GET ​/api​/junction​/:id/view --- *Gets the specified group details to display.**

### Junction

The junction template implements an editable business object with
an editable member collection.

Component | Description
--------- | -----------
Group | editable root object
GroupPersons | editable child collection
GroupPerson | editable child object

Endpoints:

- [ ] GET ​/api​/junction​/new --- *Gets a new group to edit.*
- [ ] POST ​/api​/junction --- *Creates a new group.*
- [ ] GET ​/api​/junction​/:id --- *Gets the specified group to edit.*
- [ ] PUT ​/api​/junction --- *Updates the specified group.*
- [ ] DELETE ​/api​/junction --- *Deletes the specified group.*
