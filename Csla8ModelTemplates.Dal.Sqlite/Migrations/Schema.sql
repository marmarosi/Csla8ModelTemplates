CREATE TABLE "Folders" (
    "FolderKey" INTEGER NOT NULL CONSTRAINT "PK_Folders" PRIMARY KEY AUTOINCREMENT,
    "ParentKey" INTEGER NULL,
    "RootKey" INTEGER NULL,
    "FolderOrder" INTEGER NULL,
    "FolderName" TEXT NULL,
    "Timestamp" TEXT NOT NULL,
    CONSTRAINT "FK_Folders_Folders_ParentKey" FOREIGN KEY ("ParentKey") REFERENCES "Folders" ("FolderKey")
);
CREATE TABLE "Groups" (
    "GroupKey" INTEGER NOT NULL CONSTRAINT "PK_Groups" PRIMARY KEY AUTOINCREMENT,
    "GroupCode" TEXT NULL,
    "GroupName" TEXT NULL,
    "Timestamp" TEXT NOT NULL
);
CREATE TABLE "Persons" (
    "PersonKey" INTEGER NOT NULL CONSTRAINT "PK_Persons" PRIMARY KEY AUTOINCREMENT,
    "PersonCode" TEXT NULL,
    "PersonName" TEXT NULL,
    "Timestamp" TEXT NOT NULL
);
 CREATE TABLE "Teams" (
    "TeamKey" INTEGER NOT NULL CONSTRAINT "PK_Teams" PRIMARY KEY AUTOINCREMENT,
    "TeamGuid" TEXT NULL,
    "TeamCode" TEXT NULL,
    "TeamName" TEXT NULL,
    "Timestamp" TEXT NOT NULL
);
CREATE TABLE "GroupPersons" (
    "GroupKey" INTEGER NOT NULL,
    "PersonKey" INTEGER NOT NULL,
    CONSTRAINT "PK_GroupPersons" PRIMARY KEY ("GroupKey", "PersonKey"),
    CONSTRAINT "FK_GroupPersons_Groups_GroupKey" FOREIGN KEY ("GroupKey") REFERENCES "Groups" ("GroupKey") ON DELETE CASCADE,
    CONSTRAINT "FK_GroupPersons_Persons_PersonKey" FOREIGN KEY ("PersonKey") REFERENCES "Persons" ("PersonKey") ON DELETE CASCADE
);
CREATE TABLE "Players" (
    "PlayerKey" INTEGER NOT NULL CONSTRAINT "PK_Players" PRIMARY KEY AUTOINCREMENT,
    "TeamKey" INTEGER NULL,
    "PlayerCode" TEXT NULL,
    "PlayerName" TEXT NULL,
    CONSTRAINT "FK_Players_Teams_TeamKey" FOREIGN KEY ("TeamKey") REFERENCES "Teams" ("TeamKey")
);
CREATE INDEX "IX_Folders_ParentKey_FolderOrder" ON "Folders" ("ParentKey", "FolderOrder");
CREATE INDEX "IX_GroupPersons_PersonKey" ON "GroupPersons" ("PersonKey");
CREATE UNIQUE INDEX "IX_Groups_GroupCode" ON "Groups" ("GroupCode");
CREATE UNIQUE INDEX "IX_Persons_PersonCode" ON "Persons" ("PersonCode");
CREATE UNIQUE INDEX "IX_Players_TeamKey_PlayerCode" ON "Players" ("TeamKey", "PlayerCode");
CREATE UNIQUE INDEX "IX_Teams_TeamCode" ON "Teams" ("TeamCode");
