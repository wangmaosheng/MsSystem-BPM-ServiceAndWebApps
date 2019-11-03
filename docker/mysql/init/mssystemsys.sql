CREATE DATABASE /*!32312 IF NOT EXISTS*/`mssystemsys` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `mssystemsys`;

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;


-- ----------------------------
-- Table structure for sys_button
-- ----------------------------
DROP TABLE IF EXISTS `sys_button`;
CREATE TABLE `sys_button`  (
  `ButtonId` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `ButtonName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '菜单名称',
  `Memo` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  PRIMARY KEY (`ButtonId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 10 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_button
-- ----------------------------
INSERT INTO `sys_button` VALUES (1, '查看', '查看');
INSERT INTO `sys_button` VALUES (2, '新增', '新增');
INSERT INTO `sys_button` VALUES (3, '编辑', '编辑');
INSERT INTO `sys_button` VALUES (4, '删除', '删除');
INSERT INTO `sys_button` VALUES (5, '打印', '打印');
INSERT INTO `sys_button` VALUES (6, '审核', '审核');
INSERT INTO `sys_button` VALUES (7, '作废', '作废');
INSERT INTO `sys_button` VALUES (8, '结束', '结束');
INSERT INTO `sys_button` VALUES (9, '扩展', '扩展');

-- ----------------------------
-- Table structure for sys_data_privileges
-- ----------------------------
DROP TABLE IF EXISTS `sys_data_privileges`;
CREATE TABLE `sys_data_privileges`  (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `UserId` bigint(20) NOT NULL COMMENT '用户ID',
  `DeptId` bigint(20) NOT NULL COMMENT '部门ID',
  `SystemId` bigint(20) NOT NULL COMMENT '系统ID',
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `UserId`(`UserId`) USING BTREE,
  INDEX `DeptId`(`DeptId`) USING BTREE,
  INDEX `SystemId`(`SystemId`) USING BTREE,
  CONSTRAINT `sys_data_privileges_ibfk_1` FOREIGN KEY (`UserId`) REFERENCES `sys_user` (`UserId`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `sys_data_privileges_ibfk_2` FOREIGN KEY (`DeptId`) REFERENCES `sys_dept` (`DeptId`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `sys_data_privileges_ibfk_3` FOREIGN KEY (`SystemId`) REFERENCES `sys_system` (`SystemId`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 12 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '数据权限' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_data_privileges
-- ----------------------------
INSERT INTO `sys_data_privileges` VALUES (8, 1, 1, 1);
INSERT INTO `sys_data_privileges` VALUES (9, 1, 2, 1);
INSERT INTO `sys_data_privileges` VALUES (10, 1, 3, 1);
INSERT INTO `sys_data_privileges` VALUES (11, 1, 4, 1);

-- ----------------------------
-- Table structure for sys_dept
-- ----------------------------
DROP TABLE IF EXISTS `sys_dept`;
CREATE TABLE `sys_dept`  (
  `DeptId` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '部门ID',
  `DeptName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '部门名称',
  `DeptCode` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '部门编号',
  `ParentId` bigint(20) NOT NULL COMMENT '父级ID',
  `Path` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '路径',
  `IsDel` tinyint(4) NOT NULL DEFAULT 0 COMMENT '是否删除',
  `Memo` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  `CreateUserId` bigint(20) NOT NULL COMMENT '创建人id',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间戳',
  PRIMARY KEY (`DeptId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 5 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '部门表（作用于全部系统）' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_dept
-- ----------------------------
INSERT INTO `sys_dept` VALUES (1, 'MS软件', '001', 0, '1', 0, 'MS软件', 1, 1517812123);
INSERT INTO `sys_dept` VALUES (2, '总经室', '002', 1, '1:2', 0, '总经室', 1, 1517812220);
INSERT INTO `sys_dept` VALUES (3, '研发部', '003', 1, '1:3', 0, '研发部', 1, 1517814189);
INSERT INTO `sys_dept` VALUES (4, '销售部', '004', 1, '1:4', 0, '销售部', 1, 1517814213);

-- ----------------------------
-- Table structure for sys_dept_leader
-- ----------------------------
DROP TABLE IF EXISTS `sys_dept_leader`;
CREATE TABLE `sys_dept_leader`  (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `DeptId` bigint(20) NOT NULL COMMENT '部门ID',
  `UserId` bigint(20) NOT NULL COMMENT '用户ID',
  `LeaderType` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '领导类型',
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `DeptId`(`DeptId`) USING BTREE,
  INDEX `UserId`(`UserId`) USING BTREE,
  INDEX `LeaderType`(`LeaderType`) USING BTREE,
  CONSTRAINT `sys_dept_leader_ibfk_1` FOREIGN KEY (`DeptId`) REFERENCES `sys_dept` (`DeptId`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `sys_dept_leader_ibfk_2` FOREIGN KEY (`UserId`) REFERENCES `sys_user` (`UserId`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `sys_dept_leader_ibfk_3` FOREIGN KEY (`LeaderType`) REFERENCES `sys_leader` (`Shorter`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '部门领导关系表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_dept_leader
-- ----------------------------
INSERT INTO `sys_dept_leader` VALUES (1, 3, 1, 'bmfzr');

-- ----------------------------
-- Table structure for sys_leader
-- ----------------------------
DROP TABLE IF EXISTS `sys_leader`;
CREATE TABLE `sys_leader`  (
  `Shorter` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '缩写，公司领导应根据此字段获取',
  `LeaderName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '级别名称',
  `Remark` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  PRIMARY KEY (`Shorter`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '公司领导类型' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_leader
-- ----------------------------
INSERT INTO `sys_leader` VALUES ('bmfjl', '部门副经理', '部门副经理');
INSERT INTO `sys_leader` VALUES ('bmfzr', '部门负责人', '部门负责人/部门经理');
INSERT INTO `sys_leader` VALUES ('boss', '董事长', '董事长/公司老板');

-- ----------------------------
-- Table structure for sys_release_log
-- ----------------------------
DROP TABLE IF EXISTS `sys_release_log`;
CREATE TABLE `sys_release_log`  (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `VersionNumber` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '版本号',
  `Memo` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '描述',
  `CreateTime` bigint(20) NOT NULL COMMENT '发布时间',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 27 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_release_log
-- ----------------------------
INSERT INTO `sys_release_log` VALUES (1, '1', '123', 1498481230);

-- ----------------------------
-- Table structure for sys_resource
-- ----------------------------
DROP TABLE IF EXISTS `sys_resource`;
CREATE TABLE `sys_resource`  (
  `ResourceId` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '资源ID',
  `SystemId` bigint(20) NOT NULL COMMENT '所属系统',
  `ResourceName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '资源名称',
  `ParentId` bigint(11) NOT NULL DEFAULT 0 COMMENT '父级ID',
  `ResourceUrl` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '资源地址',
  `Sort` int(11) NOT NULL COMMENT '同级排序',
  `ButtonClass` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '按钮样式',
  `Icon` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '样式图标ICON',
  `IsShow` tinyint(4) NOT NULL COMMENT '是否显示到菜单',
  `CreateUserId` bigint(11) NOT NULL COMMENT '创建人',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间戳',
  `Memo` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  `IsDel` tinyint(4) NOT NULL DEFAULT 0 COMMENT '是否删除',
  `IsButton` tinyint(4) NOT NULL DEFAULT 0 COMMENT '是否是按钮',
  `ButtonType` tinyint(4) NULL DEFAULT NULL COMMENT '按钮类型',
  `Path` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '资源路径',
  PRIMARY KEY (`ResourceId`) USING BTREE,
  INDEX `ParentId`(`ParentId`) USING BTREE,
  INDEX `SystemId`(`SystemId`) USING BTREE,
  CONSTRAINT `sys_resource_ibfk_1` FOREIGN KEY (`SystemId`) REFERENCES `sys_system` (`SystemId`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 277 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '资源表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_resource
-- ----------------------------
INSERT INTO `sys_resource` VALUES (1, 1, '系统管理', 0, '/', 1, NULL, 'fa fa-cogs', 1, 1, 1, '123', 0, 0, NULL, '1');
INSERT INTO `sys_resource` VALUES (2, 1, '菜单管理', 1, '/Sys/Resource/Index', 5, NULL, 'fa fa-bars', 1, 1, 1, NULL, 0, 0, NULL, '1:2');
INSERT INTO `sys_resource` VALUES (4, 1, '菜单管理编辑页', 2, '/Sys/Resource/Show', 0, NULL, 'fa fa-balance-scale', 0, 1, 1, NULL, 0, 0, NULL, '2:4');
INSERT INTO `sys_resource` VALUES (48, 1, '查看', 4, NULL, 4, NULL, 'fa fa-search', 0, 0, 1503957966, NULL, 0, 1, 1, NULL);
INSERT INTO `sys_resource` VALUES (49, 1, '新增', 4, NULL, 1, NULL, 'fa fa-plus', 0, 0, 1503957966, NULL, 0, 1, 2, NULL);
INSERT INTO `sys_resource` VALUES (50, 1, '编辑', 4, NULL, 2, NULL, 'fa fa-edit', 0, 0, 1503957966, NULL, 0, 1, 3, NULL);
INSERT INTO `sys_resource` VALUES (51, 1, '删除', 4, NULL, 3, NULL, 'fa fa-trash', 0, 0, 1503957966, NULL, 0, 1, 4, NULL);
INSERT INTO `sys_resource` VALUES (53, 1, '角色管理', 1, '/Sys/Role/Index', 2, NULL, 'fa fa-group', 1, 1, 1503958727, '123', 0, 0, NULL, '1:53');
INSERT INTO `sys_resource` VALUES (54, 1, '查看', 53, NULL, 4, NULL, 'fa fa-search', 0, 0, 1503958727, NULL, 0, 1, 1, NULL);
INSERT INTO `sys_resource` VALUES (55, 1, '新增', 53, NULL, 1, NULL, 'fa fa-plus', 0, 0, 1503958727, NULL, 0, 1, 2, NULL);
INSERT INTO `sys_resource` VALUES (56, 1, '编辑', 53, NULL, 2, NULL, 'fa fa-edit', 0, 0, 1503958727, NULL, 0, 1, 3, NULL);
INSERT INTO `sys_resource` VALUES (57, 1, '删除', 53, NULL, 3, NULL, 'fa fa-trash', 0, 0, 1503958727, NULL, 0, 1, 4, NULL);
INSERT INTO `sys_resource` VALUES (58, 1, '子系统管理', 1, '/Sys/System/Index', 4, NULL, 'fa fa-cog', 1, 1, 1503958798, '123', 0, 0, NULL, '1:58');
INSERT INTO `sys_resource` VALUES (59, 1, '查看', 58, NULL, 4, NULL, 'fa fa-search', 0, 0, 1503958825, NULL, 0, 1, 1, NULL);
INSERT INTO `sys_resource` VALUES (60, 1, '新增', 58, NULL, 1, NULL, 'fa fa-plus', 0, 0, 1503958825, NULL, 0, 1, 2, NULL);
INSERT INTO `sys_resource` VALUES (61, 1, '编辑', 58, NULL, 2, NULL, 'fa fa-edit', 0, 0, 1503958825, NULL, 0, 1, 3, NULL);
INSERT INTO `sys_resource` VALUES (62, 1, '删除', 58, NULL, 3, NULL, 'fa fa-trash', 0, 0, 1503958825, NULL, 0, 1, 4, NULL);
INSERT INTO `sys_resource` VALUES (64, 1, '编辑', 2, NULL, 2, NULL, 'fa fa-edit', 0, 0, 1503958921, NULL, 0, 1, 3, NULL);
INSERT INTO `sys_resource` VALUES (65, 1, '删除', 2, NULL, 3, NULL, 'fa fa-trash', 0, 0, 1503958921, NULL, 0, 1, 4, NULL);
INSERT INTO `sys_resource` VALUES (66, 1, '角色编辑页', 53, '/Sys/Role/Show', 0, NULL, NULL, 0, 1, 1503959673, NULL, 0, 0, NULL, '53:66');
INSERT INTO `sys_resource` VALUES (67, 1, '查看', 66, NULL, 4, NULL, 'fa fa-search', 0, 0, 1503959674, NULL, 0, 1, 1, NULL);
INSERT INTO `sys_resource` VALUES (68, 1, '新增', 66, NULL, 1, NULL, 'fa fa-plus', 0, 0, 1503959674, NULL, 0, 1, 2, NULL);
INSERT INTO `sys_resource` VALUES (69, 1, '编辑', 66, NULL, 2, NULL, 'fa fa-edit', 0, 0, 1503959674, NULL, 0, 1, 3, NULL);
INSERT INTO `sys_resource` VALUES (70, 1, '删除', 66, NULL, 3, NULL, 'fa fa-trash', 0, 0, 1503959674, NULL, 0, 1, 4, NULL);
INSERT INTO `sys_resource` VALUES (71, 1, '系统编辑页', 58, '/Sys/System/Show', 0, NULL, NULL, 0, 1, 1503959721, NULL, 0, 0, NULL, '58:71');
INSERT INTO `sys_resource` VALUES (72, 1, '查看', 71, NULL, 4, NULL, 'fa fa-search', 0, 0, 1503959721, NULL, 0, 1, 1, NULL);
INSERT INTO `sys_resource` VALUES (73, 1, '新增', 71, NULL, 1, NULL, 'fa fa-plus', 0, 0, 1503959721, NULL, 0, 1, 2, NULL);
INSERT INTO `sys_resource` VALUES (74, 1, '编辑', 71, NULL, 2, NULL, 'fa fa-edit', 0, 0, 1503959721, NULL, 0, 1, 3, NULL);
INSERT INTO `sys_resource` VALUES (75, 1, '删除', 71, NULL, 3, NULL, 'fa fa-trash', 0, 0, 1503959721, NULL, 0, 1, 4, NULL);
INSERT INTO `sys_resource` VALUES (76, 1, '用户管理', 1, '/Sys/User/Index', 1, NULL, 'fa fa-user', 1, 1, 1503959782, NULL, 0, 0, NULL, '1:76');
INSERT INTO `sys_resource` VALUES (77, 1, '查看', 76, NULL, 4, NULL, 'fa fa-search', 0, 0, 1503959782, NULL, 0, 1, 1, NULL);
INSERT INTO `sys_resource` VALUES (78, 1, '新增', 76, NULL, 1, NULL, 'fa fa-plus', 0, 0, 1503959782, NULL, 0, 1, 2, NULL);
INSERT INTO `sys_resource` VALUES (79, 1, '编辑', 76, NULL, 2, NULL, 'fa fa-edit', 0, 0, 1503959782, NULL, 0, 1, 3, NULL);
INSERT INTO `sys_resource` VALUES (80, 1, '删除', 76, NULL, 3, NULL, 'fa fa-trash', 0, 0, 1503959782, NULL, 0, 1, 4, NULL);
INSERT INTO `sys_resource` VALUES (81, 1, '用户编辑页', 76, '/Sys/User/Show', 0, NULL, NULL, 0, 1, 1503959818, NULL, 0, 0, NULL, '76:81');
INSERT INTO `sys_resource` VALUES (82, 1, '查看', 81, NULL, 4, NULL, 'fa fa-search', 0, 0, 1503959818, NULL, 0, 1, 1, NULL);
INSERT INTO `sys_resource` VALUES (83, 1, '新增', 81, NULL, 1, NULL, 'fa fa-plus', 0, 0, 1503959818, NULL, 0, 1, 2, NULL);
INSERT INTO `sys_resource` VALUES (84, 1, '编辑', 81, NULL, 2, NULL, 'fa fa-edit', 0, 0, 1503959818, NULL, 0, 1, 3, NULL);
INSERT INTO `sys_resource` VALUES (85, 1, '删除', 81, NULL, 3, NULL, 'fa fa-trash', 0, 0, 1503959818, NULL, 0, 1, 4, NULL);
INSERT INTO `sys_resource` VALUES (86, 1, '查看', 2, NULL, 4, NULL, 'fa fa-search', 0, 0, 1503990906, NULL, 0, 1, 1, NULL);
INSERT INTO `sys_resource` VALUES (87, 1, '新增', 2, NULL, 1, NULL, 'fa fa-plus', 0, 0, 1503990906, NULL, 0, 1, 2, NULL);
INSERT INTO `sys_resource` VALUES (88, 2, '工作流', 0, '/', 3, NULL, 'fa fa-sitemap', 1, 1, 1504013557, NULL, 0, 0, NULL, '88');
INSERT INTO `sys_resource` VALUES (91, 2, '流程设计', 88, '/WF/WorkFlow/Index', 3, NULL, 'fa fa-legal', 1, 1, 1504439709, '流程设计列表', 0, 0, NULL, '88:91');
INSERT INTO `sys_resource` VALUES (92, 2, '查看', 91, NULL, 4, NULL, 'fa fa-search', 0, 0, 1504439709, NULL, 0, 1, 1, NULL);
INSERT INTO `sys_resource` VALUES (93, 2, '我的待办', 88, '/WF/WorkFlowInstance/TodoList', 1, NULL, 'fa fa-user', 1, 1, 1504439745, '我的待办', 0, 0, NULL, '88:93');
INSERT INTO `sys_resource` VALUES (94, 2, '审批历史', 88, '/WF/WorkFlowInstance/MyApprovalHistory', 0, NULL, 'fa fa-history', 1, 1, 1504575850, '审批历史记录', 0, 0, NULL, '88:94');
INSERT INTO `sys_resource` VALUES (95, 2, '查看', 93, NULL, 4, NULL, 'fa fa-search', 0, 0, 1504575862, NULL, 0, 1, 1, NULL);
INSERT INTO `sys_resource` VALUES (96, 2, '查看', 94, NULL, 4, NULL, 'fa fa-search', 0, 0, 1504575866, NULL, 0, 1, 1, NULL);
INSERT INTO `sys_resource` VALUES (97, 3, '微信管理', 0, '/', 2, NULL, 'fa fa-weixin', 1, 1, 1504576048, '微信管理', 0, 0, NULL, '97');
INSERT INTO `sys_resource` VALUES (98, 2, '流程分类', 88, '/WF/Category/Index', 4, NULL, 'fa fa-building-o', 1, 1, 1508764750, '流程分类', 0, 0, NULL, '88:98');
INSERT INTO `sys_resource` VALUES (99, 2, '查看', 98, NULL, 4, NULL, 'fa fa-search', 0, 0, 1508764750, NULL, 0, 1, 1, NULL);
INSERT INTO `sys_resource` VALUES (100, 5, '行政办公', 0, '/', 2, NULL, 'fa fa-book', 1, 1, 1509884326, NULL, 0, 0, NULL, '100');
INSERT INTO `sys_resource` VALUES (142, 1, '部门管理', 1, '/Sys/Dept/Index', 3, NULL, 'fa fa-sitemap', 1, 1, 1514899755, NULL, 0, 0, NULL, '1:142');
INSERT INTO `sys_resource` VALUES (143, 1, '查看', 142, NULL, 4, NULL, 'fa fa-search', 0, 0, 1514899755, NULL, 0, 1, 1, NULL);
INSERT INTO `sys_resource` VALUES (144, 1, '新增', 142, NULL, 1, NULL, 'fa fa-plus', 0, 0, 1514899755, NULL, 0, 1, 2, NULL);
INSERT INTO `sys_resource` VALUES (145, 1, '编辑', 142, NULL, 2, NULL, 'fa fa-edit', 0, 0, 1514899755, NULL, 0, 1, 3, NULL);
INSERT INTO `sys_resource` VALUES (146, 1, '删除', 142, NULL, 3, NULL, 'fa fa-trash', 0, 0, 1514899755, NULL, 0, 1, 4, NULL);
INSERT INTO `sys_resource` VALUES (152, 1, '部门编辑页', 142, '/Sys/Dept/Show', 1, NULL, '', 0, 1, 1514899820, NULL, 0, 0, NULL, '1:142:152');
INSERT INTO `sys_resource` VALUES (153, 1, '查看', 152, NULL, 4, NULL, 'fa fa-search', 0, 0, 1514899820, NULL, 0, 1, 1, NULL);
INSERT INTO `sys_resource` VALUES (154, 1, '新增', 152, NULL, 1, NULL, 'fa fa-plus', 0, 0, 1514899820, NULL, 0, 1, 2, NULL);
INSERT INTO `sys_resource` VALUES (155, 1, '编辑', 152, NULL, 2, NULL, 'fa fa-edit', 0, 0, 1514899820, NULL, 0, 1, 3, NULL);
INSERT INTO `sys_resource` VALUES (156, 1, '删除', 152, NULL, 3, NULL, 'fa fa-trash', 0, 0, 1514899820, NULL, 0, 1, 4, NULL);
INSERT INTO `sys_resource` VALUES (157, 1, '数据权限', 76, '/Sys/User/DataPrivileges', 2, NULL, NULL, 0, 1, 1515548644, NULL, 0, 0, NULL, '1:76:157');
INSERT INTO `sys_resource` VALUES (158, 1, '查看', 157, NULL, 4, NULL, 'fa fa-search', 0, 0, 1515548644, NULL, 0, 1, 1, NULL);
INSERT INTO `sys_resource` VALUES (159, 1, '新增', 157, NULL, 1, NULL, 'fa fa-plus', 0, 0, 1515548644, NULL, 0, 1, 2, NULL);
INSERT INTO `sys_resource` VALUES (160, 1, '编辑', 157, NULL, 2, NULL, 'fa fa-edit', 0, 0, 1515548644, NULL, 0, 1, 3, NULL);
INSERT INTO `sys_resource` VALUES (161, 1, '删除', 157, NULL, 3, NULL, 'fa fa-trash', 0, 0, 1515548644, NULL, 0, 1, 4, NULL);
INSERT INTO `sys_resource` VALUES (162, 5, '员工请假', 100, '/OA/Leave/Index', 1, NULL, 'fa fa-hand-paper-o', 1, 1, 1519985181, NULL, 0, 0, NULL, '100:162');
INSERT INTO `sys_resource` VALUES (163, 5, '查看', 162, NULL, 0, NULL, 'fa fa-search', 0, 0, 1519985181, NULL, 0, 1, 1, NULL);
INSERT INTO `sys_resource` VALUES (164, 5, '新增', 162, NULL, 0, NULL, 'fa fa-plus', 0, 0, 1519985181, NULL, 0, 1, 2, NULL);
INSERT INTO `sys_resource` VALUES (165, 5, '编辑', 162, NULL, 0, NULL, 'fa fa-edit', 0, 0, 1519985181, NULL, 0, 1, 3, NULL);
INSERT INTO `sys_resource` VALUES (166, 5, '删除', 162, NULL, 0, NULL, 'fa fa-trash', 0, 0, 1519985181, NULL, 0, 1, 4, NULL);
INSERT INTO `sys_resource` VALUES (167, 5, '员工请假编辑页', 162, '/OA/Leave/Show', 1, NULL, 'fa fa-cc-diners-club', 0, 1, 1520040301, NULL, 0, 0, NULL, '162:167');
INSERT INTO `sys_resource` VALUES (168, 5, '查看', 167, NULL, 0, NULL, 'fa fa-search', 0, 0, 1520040301, NULL, 0, 1, 1, NULL);
INSERT INTO `sys_resource` VALUES (169, 5, '新增', 167, NULL, 0, NULL, 'fa fa-plus', 0, 0, 1520040301, NULL, 0, 1, 2, NULL);
INSERT INTO `sys_resource` VALUES (170, 5, '编辑', 167, NULL, 0, NULL, 'fa fa-edit', 0, 0, 1520040301, NULL, 0, 1, 3, NULL);
INSERT INTO `sys_resource` VALUES (171, 5, '删除', 167, NULL, 0, NULL, 'fa fa-trash', 0, 0, 1520040301, NULL, 0, 1, 4, NULL);
INSERT INTO `sys_resource` VALUES (172, 2, '新增', 91, NULL, 0, NULL, 'fa fa-plus', 0, 0, 1544438530, NULL, 0, 1, 2, NULL);
INSERT INTO `sys_resource` VALUES (173, 2, '编辑', 91, NULL, 0, NULL, 'fa fa-edit', 0, 0, 1544438530, NULL, 0, 1, 3, NULL);
INSERT INTO `sys_resource` VALUES (174, 2, '删除', 91, NULL, 0, NULL, 'fa fa-trash', 0, 0, 1544438530, NULL, 0, 1, 4, NULL);
INSERT INTO `sys_resource` VALUES (175, 1, '调度中心', 1, '/Sys/Schedule/Index', 6, NULL, 'fa fa-joomla', 1, 1, 1545365625, NULL, 0, 0, NULL, '1:175');
INSERT INTO `sys_resource` VALUES (176, 1, '查看', 175, NULL, 0, NULL, 'fa fa-search', 0, 0, 1545365625, NULL, 0, 1, 1, NULL);
INSERT INTO `sys_resource` VALUES (177, 1, '新增', 175, NULL, 0, NULL, 'fa fa-plus', 0, 0, 1545365625, NULL, 0, 1, 2, NULL);
INSERT INTO `sys_resource` VALUES (178, 1, '编辑', 175, NULL, 0, NULL, 'fa fa-edit', 0, 0, 1545365625, NULL, 0, 1, 3, NULL);
INSERT INTO `sys_resource` VALUES (179, 1, '删除', 175, NULL, 0, NULL, 'fa fa-trash', 0, 0, 1545365625, NULL, 0, 1, 4, NULL);
INSERT INTO `sys_resource` VALUES (180, 1, '调度编辑页', 175, '/Sys/Schedule/Show', 1, NULL, NULL, 0, 1, 1545365682, NULL, 0, 0, NULL, '1:175:180');
INSERT INTO `sys_resource` VALUES (181, 1, '查看', 180, NULL, 0, NULL, 'fa fa-search', 0, 0, 1545365682, NULL, 0, 1, 1, NULL);
INSERT INTO `sys_resource` VALUES (182, 1, '新增', 180, NULL, 0, NULL, 'fa fa-plus', 0, 0, 1545365754, NULL, 0, 1, 2, NULL);
INSERT INTO `sys_resource` VALUES (183, 1, '编辑', 180, NULL, 0, NULL, 'fa fa-edit', 0, 0, 1545365754, NULL, 0, 1, 3, NULL);
INSERT INTO `sys_resource` VALUES (184, 1, '删除', 180, NULL, 0, NULL, 'fa fa-trash', 0, 0, 1545365754, NULL, 0, 1, 4, NULL);
INSERT INTO `sys_resource` VALUES (185, 1, '日志列表', 1, '/Sys/Log/Index', 7, NULL, 'fa fa-list-alt', 1, 1, 1545902555, NULL, 0, 0, NULL, '1:185');
INSERT INTO `sys_resource` VALUES (186, 1, '查看', 185, NULL, 0, NULL, 'fa fa-search', 0, 0, 1545902555, NULL, 0, 1, 1, NULL);
INSERT INTO `sys_resource` VALUES (187, 3, '规则管理', 97, '/Weixin/Rule/Index', 3, NULL, 'fa fa-hand-lizard-o', 1, 1, 1547040097, NULL, 0, 0, NULL, '97:187');
INSERT INTO `sys_resource` VALUES (188, 3, '查看', 187, NULL, 0, NULL, 'fa fa-search', 0, 0, 1547040097, NULL, 0, 1, 1, NULL);
INSERT INTO `sys_resource` VALUES (189, 3, '新增', 187, NULL, 0, NULL, 'fa fa-plus', 0, 0, 1547040097, NULL, 0, 1, 2, NULL);
INSERT INTO `sys_resource` VALUES (190, 3, '编辑', 187, NULL, 0, NULL, 'fa fa-edit', 0, 0, 1547040097, NULL, 0, 1, 3, NULL);
INSERT INTO `sys_resource` VALUES (191, 3, '删除', 187, NULL, 0, NULL, 'fa fa-trash', 0, 0, 1547040097, NULL, 0, 1, 4, NULL);
INSERT INTO `sys_resource` VALUES (192, 3, '规则编辑页', 187, '/Weixin/Rule/Show', 1, NULL, NULL, 0, 1, 1547040143, NULL, 0, 0, NULL, '97:187:192');
INSERT INTO `sys_resource` VALUES (193, 3, '查看', 192, NULL, 0, NULL, 'fa fa-search', 0, 0, 1547040143, NULL, 0, 1, 1, NULL);
INSERT INTO `sys_resource` VALUES (194, 3, '新增', 192, NULL, 0, NULL, 'fa fa-plus', 0, 0, 1547040143, NULL, 0, 1, 2, NULL);
INSERT INTO `sys_resource` VALUES (195, 3, '编辑', 192, NULL, 0, NULL, 'fa fa-edit', 0, 0, 1547040143, NULL, 0, 1, 3, NULL);
INSERT INTO `sys_resource` VALUES (196, 3, '删除', 192, NULL, 0, NULL, 'fa fa-trash', 0, 0, 1547040143, NULL, 0, 1, 4, NULL);
INSERT INTO `sys_resource` VALUES (197, 3, '自定义菜单', 97, '/Weixin/Menu/Index', 2, NULL, 'fa fa-bars', 1, 1, 1547040193, NULL, 0, 0, NULL, '97:197');
INSERT INTO `sys_resource` VALUES (198, 3, '查看', 197, NULL, 0, NULL, 'fa fa-search', 0, 0, 1547040193, NULL, 0, 1, 1, NULL);
INSERT INTO `sys_resource` VALUES (199, 3, '新增', 197, NULL, 0, NULL, 'fa fa-plus', 0, 0, 1547040193, NULL, 0, 1, 2, NULL);
INSERT INTO `sys_resource` VALUES (200, 3, '编辑', 197, NULL, 0, NULL, 'fa fa-edit', 0, 0, 1547040193, NULL, 0, 1, 3, NULL);
INSERT INTO `sys_resource` VALUES (201, 3, '删除', 197, NULL, 0, NULL, 'fa fa-trash', 0, 0, 1547040193, NULL, 0, 1, 4, NULL);
INSERT INTO `sys_resource` VALUES (202, 3, '自定义菜单编辑页', 197, '/Weixin/Menu/Show', 1, NULL, NULL, 0, 1, 1547040223, NULL, 0, 0, NULL, '97:197:202');
INSERT INTO `sys_resource` VALUES (203, 3, '查看', 202, NULL, 0, NULL, 'fa fa-search', 0, 0, 1547040223, NULL, 0, 1, 1, NULL);
INSERT INTO `sys_resource` VALUES (204, 3, '新增', 202, NULL, 0, NULL, 'fa fa-plus', 0, 0, 1547040223, NULL, 0, 1, 2, NULL);
INSERT INTO `sys_resource` VALUES (205, 3, '编辑', 202, NULL, 0, NULL, 'fa fa-edit', 0, 0, 1547040223, NULL, 0, 1, 3, NULL);
INSERT INTO `sys_resource` VALUES (206, 3, '删除', 202, NULL, 0, NULL, 'fa fa-trash', 0, 0, 1547040223, NULL, 0, 1, 4, NULL);
INSERT INTO `sys_resource` VALUES (223, 2, '新增', 98, NULL, 0, 'fa fa-plus', NULL, 0, 0, 1556070241, NULL, 0, 1, 2, NULL);
INSERT INTO `sys_resource` VALUES (224, 2, '编辑', 98, NULL, 0, 'fa fa-edit', NULL, 0, 0, 1556070241, NULL, 0, 1, 3, NULL);
INSERT INTO `sys_resource` VALUES (225, 2, '删除', 98, NULL, 0, 'fa fa-trash', NULL, 0, 0, 1556070241, NULL, 0, 1, 4, NULL);
INSERT INTO `sys_resource` VALUES (226, 2, '我的流程', 88, '/WF/WorkFlowInstance/MyFlow', 2, NULL, 'fa fa-user-plus', 1, 1, 1556096263, NULL, 0, 0, NULL, '88:226');
INSERT INTO `sys_resource` VALUES (227, 2, '查看', 226, NULL, 0, 'fa fa-search', NULL, 0, 0, 1556096263, NULL, 0, 1, 1, NULL);
INSERT INTO `sys_resource` VALUES (228, 2, '新增', 226, NULL, 0, 'fa fa-plus', NULL, 0, 0, 1556096263, NULL, 0, 1, 2, NULL);
INSERT INTO `sys_resource` VALUES (229, 2, '编辑', 226, NULL, 0, 'fa fa-edit', NULL, 0, 0, 1556096263, NULL, 0, 1, 3, NULL);
INSERT INTO `sys_resource` VALUES (230, 2, '删除', 226, NULL, 0, 'fa fa-trash', NULL, 0, 0, 1556096263, NULL, 0, 1, 4, NULL);
INSERT INTO `sys_resource` VALUES (231, 2, '流程发起', 88, '/WF/WorkFlowInstance/Start', 5, NULL, 'fa fa-location-arrow', 1, 1, 1556096629, NULL, 0, 0, NULL, '88:231');
INSERT INTO `sys_resource` VALUES (232, 2, '查看', 231, NULL, 0, 'fa fa-search', NULL, 0, 0, 1556096629, NULL, 0, 1, 1, NULL);
INSERT INTO `sys_resource` VALUES (236, 2, '表单设计', 88, '/WF/Form/Index', 6, NULL, 'fa fa-contao', 1, 1, 1556097850, '表单设计', 0, 0, NULL, '88:236');
INSERT INTO `sys_resource` VALUES (237, 2, '查看', 236, NULL, 0, 'fa fa-search', NULL, 0, 0, 1556097850, NULL, 0, 1, 1, NULL);
INSERT INTO `sys_resource` VALUES (238, 2, '新增', 236, NULL, 0, 'fa fa-plus', NULL, 0, 0, 1556097850, NULL, 0, 1, 2, NULL);
INSERT INTO `sys_resource` VALUES (239, 2, '编辑', 236, NULL, 0, 'fa fa-edit', NULL, 0, 0, 1556097850, NULL, 0, 1, 3, NULL);
INSERT INTO `sys_resource` VALUES (240, 2, '删除', 236, NULL, 0, 'fa fa-trash', NULL, 0, 0, 1556097850, NULL, 0, 1, 4, NULL);
INSERT INTO `sys_resource` VALUES (241, 2, '表单设计编辑页', 236, '/WF/Form/Show', 1, NULL, NULL, 0, 1, 1556098005, NULL, 0, 0, NULL, '88:236:241');
INSERT INTO `sys_resource` VALUES (242, 2, '查看', 241, NULL, 0, 'fa fa-search', NULL, 0, 0, 1556098005, NULL, 0, 1, 1, NULL);
INSERT INTO `sys_resource` VALUES (243, 2, '新增', 241, NULL, 0, 'fa fa-plus', NULL, 0, 0, 1556098005, NULL, 0, 1, 2, NULL);
INSERT INTO `sys_resource` VALUES (244, 2, '编辑', 241, NULL, 0, 'fa fa-edit', NULL, 0, 0, 1556098005, NULL, 0, 1, 3, NULL);
INSERT INTO `sys_resource` VALUES (245, 2, '删除', 241, NULL, 0, 'fa fa-trash', NULL, 0, 0, 1556098005, NULL, 0, 1, 4, NULL);
INSERT INTO `sys_resource` VALUES (246, 2, '流程设计编辑页', 91, '/WF/WorkFlow/Show', 1, NULL, NULL, 0, 1, 1557981722, '流程设计编辑页', 0, 0, NULL, '88:91:246');
INSERT INTO `sys_resource` VALUES (247, 2, '查看', 246, NULL, 0, 'fa fa-search', NULL, 0, 0, 1557981722, NULL, 0, 1, 1, NULL);
INSERT INTO `sys_resource` VALUES (248, 2, '新增', 246, NULL, 0, 'fa fa-plus', NULL, 0, 0, 1557981722, NULL, 0, 1, 2, NULL);
INSERT INTO `sys_resource` VALUES (249, 2, '编辑', 246, NULL, 0, 'fa fa-edit', NULL, 0, 0, 1557981722, NULL, 0, 1, 3, NULL);
INSERT INTO `sys_resource` VALUES (250, 2, '删除', 246, NULL, 0, 'fa fa-trash', NULL, 0, 0, 1557981722, NULL, 0, 1, 4, NULL);
INSERT INTO `sys_resource` VALUES (253, 5, '消息管理', 100, '/OA/Message/Index', 2, NULL, 'fa fa-envelope-o', 1, 1, 1558927920, NULL, 0, 0, NULL, '100:253');
INSERT INTO `sys_resource` VALUES (254, 5, '查看', 253, NULL, 0, 'fa fa-search', NULL, 0, 0, 1558927920, NULL, 0, 1, 1, NULL);
INSERT INTO `sys_resource` VALUES (255, 5, '新增', 253, NULL, 0, 'fa fa-plus', NULL, 0, 0, 1558927920, NULL, 0, 1, 2, NULL);
INSERT INTO `sys_resource` VALUES (256, 5, '编辑', 253, NULL, 0, 'fa fa-edit', NULL, 0, 0, 1558927920, NULL, 0, 1, 3, NULL);
INSERT INTO `sys_resource` VALUES (257, 5, '删除', 253, NULL, 0, 'fa fa-trash', NULL, 0, 0, 1558927920, NULL, 0, 1, 4, NULL);
INSERT INTO `sys_resource` VALUES (258, 5, '立即发送', 253, NULL, 0, 'fa fa-location-arrow', NULL, 0, 0, 1558927931, NULL, 0, 1, 9, NULL);
INSERT INTO `sys_resource` VALUES (259, 5, '消息编辑页', 253, '/OA/Message/Show', 1, NULL, NULL, 0, 1, 1558927982, NULL, 0, 0, NULL, '100:253:259');
INSERT INTO `sys_resource` VALUES (260, 5, '查看', 259, NULL, 0, 'fa fa-search', NULL, 0, 0, 1558927982, NULL, 0, 1, 1, NULL);
INSERT INTO `sys_resource` VALUES (261, 5, '新增', 259, NULL, 0, 'fa fa-plus', NULL, 0, 0, 1558927982, NULL, 0, 1, 2, NULL);
INSERT INTO `sys_resource` VALUES (262, 5, '编辑', 259, NULL, 0, 'fa fa-edit', NULL, 0, 0, 1558927982, NULL, 0, 1, 3, NULL);
INSERT INTO `sys_resource` VALUES (263, 5, '删除', 259, NULL, 0, 'fa fa-trash', NULL, 0, 0, 1558927982, NULL, 0, 1, 4, NULL);
INSERT INTO `sys_resource` VALUES (264, 5, '我的消息', 100, '/OA/Message/MyList', 3, NULL, 'fa fa-commenting-o', 1, 1, 1558928061, NULL, 0, 0, NULL, '100:264');
INSERT INTO `sys_resource` VALUES (265, 5, '查看', 264, NULL, 0, 'fa fa-search', NULL, 0, 0, 1558928061, NULL, 0, 1, 1, NULL);
INSERT INTO `sys_resource` VALUES (266, 5, '消息明细', 264, '/OA/Message/Detail', 1, NULL, NULL, 0, 1, 1558946268, NULL, 0, 0, NULL, '100:264:266');
INSERT INTO `sys_resource` VALUES (267, 5, '查看', 266, NULL, 0, 'fa fa-search', NULL, 0, 0, 1558946268, NULL, 0, 1, 1, NULL);
INSERT INTO `sys_resource` VALUES (268, 3, '公众号', 97, '/Weixin/Account/Index', 1, NULL, 'fa fa-user', 1, 1, 1560503093, '公众号列表', 0, 0, NULL, '97:268');
INSERT INTO `sys_resource` VALUES (269, 3, '查看', 268, NULL, 0, 'fa fa-search', NULL, 0, 0, 1560503093, NULL, 0, 1, 1, NULL);
INSERT INTO `sys_resource` VALUES (270, 3, '新增', 268, NULL, 0, 'fa fa-plus', NULL, 0, 0, 1560503093, NULL, 0, 1, 2, NULL);
INSERT INTO `sys_resource` VALUES (271, 3, '编辑', 268, NULL, 0, 'fa fa-edit', NULL, 0, 0, 1560503093, NULL, 0, 1, 3, NULL);
INSERT INTO `sys_resource` VALUES (272, 3, '删除', 268, NULL, 0, 'fa fa-trash', NULL, 0, 0, 1560503093, NULL, 0, 1, 4, NULL);
INSERT INTO `sys_resource` VALUES (273, 5, '内部聊天', 100, '/OA/Chat/Index', 4, NULL, 'fa fa-whatsapp', 1, 1, 1560740703, '即时通信聊天', 0, 0, NULL, '100:273');
INSERT INTO `sys_resource` VALUES (274, 5, '查看', 273, NULL, 0, 'fa fa-search', NULL, 0, 0, 1560740703, NULL, 0, 1, 1, NULL);
INSERT INTO `sys_resource` VALUES (275, 1, '生成代码', 1, '/Sys/CodeBuilder/Index', 8, NULL, 'fa fa-code', 1, 1, 1571682751, NULL, 0, 0, NULL, '1:275');
INSERT INTO `sys_resource` VALUES (276, 1, '查看', 275, NULL, 0, 'fa fa-search', NULL, 0, 0, 1571682751, NULL, 0, 1, 1, NULL);

-- ----------------------------
-- Table structure for sys_role
-- ----------------------------
DROP TABLE IF EXISTS `sys_role`;
CREATE TABLE `sys_role`  (
  `RoleId` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '角色ID',
  `SystemId` bigint(20) NOT NULL COMMENT '所属系统',
  `RoleName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '角色名称',
  `Memo` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  `IsDel` tinyint(4) NOT NULL DEFAULT 0 COMMENT '是否删除',
  `CreateUserId` bigint(20) NOT NULL COMMENT '创建人ID',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  `UpdateUserId` bigint(20) NULL DEFAULT NULL COMMENT '修改人',
  `UpdateTime` bigint(20) NULL DEFAULT NULL COMMENT '修改时间',
  PRIMARY KEY (`RoleId`) USING BTREE,
  INDEX `SystemId`(`SystemId`) USING BTREE,
  CONSTRAINT `sys_role_ibfk_1` FOREIGN KEY (`SystemId`) REFERENCES `sys_system` (`SystemId`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 8 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '角色表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_role
-- ----------------------------
INSERT INTO `sys_role` VALUES (1, 1, '系统管理员', '系统管理员', 0, 0, 1497889200, NULL, NULL);
INSERT INTO `sys_role` VALUES (2, 1, '微信管理员', '微信管理员', 1, 0, 1497889200, 0, 0);
INSERT INTO `sys_role` VALUES (4, 1, '测试角色', '测试角色', 1, 1, 1499488686, NULL, NULL);
INSERT INTO `sys_role` VALUES (5, 2, '流程管理员', '流程管理员', 0, 1, 1499493280, NULL, NULL);
INSERT INTO `sys_role` VALUES (6, 5, '普通用户', '普通用户123', 0, 1, 1500956464, NULL, NULL);
INSERT INTO `sys_role` VALUES (7, 3, '微信管理员', '微信管理员', 0, 1, 1547040281, 0, 0);

-- ----------------------------
-- Table structure for sys_role_resource
-- ----------------------------
DROP TABLE IF EXISTS `sys_role_resource`;
CREATE TABLE `sys_role_resource`  (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `RoleId` bigint(20) NOT NULL COMMENT '角色ID',
  `ResourceId` bigint(20) NOT NULL COMMENT '资源ID',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间戳',
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `RoleId`(`RoleId`) USING BTREE,
  INDEX `ResourceId`(`ResourceId`) USING BTREE,
  CONSTRAINT `sys_role_resource_ibfk_1` FOREIGN KEY (`RoleId`) REFERENCES `sys_role` (`RoleId`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `sys_role_resource_ibfk_2` FOREIGN KEY (`ResourceId`) REFERENCES `sys_resource` (`ResourceId`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 2371 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '角色资源关联表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_role_resource
-- ----------------------------
INSERT INTO `sys_role_resource` VALUES (2210, 5, 88, 1559053008);
INSERT INTO `sys_role_resource` VALUES (2211, 5, 94, 1559053008);
INSERT INTO `sys_role_resource` VALUES (2212, 5, 96, 1559053008);
INSERT INTO `sys_role_resource` VALUES (2213, 5, 93, 1559053008);
INSERT INTO `sys_role_resource` VALUES (2214, 5, 95, 1559053008);
INSERT INTO `sys_role_resource` VALUES (2215, 5, 226, 1559053008);
INSERT INTO `sys_role_resource` VALUES (2216, 5, 227, 1559053008);
INSERT INTO `sys_role_resource` VALUES (2217, 5, 228, 1559053008);
INSERT INTO `sys_role_resource` VALUES (2218, 5, 229, 1559053008);
INSERT INTO `sys_role_resource` VALUES (2219, 5, 230, 1559053008);
INSERT INTO `sys_role_resource` VALUES (2220, 5, 91, 1559053008);
INSERT INTO `sys_role_resource` VALUES (2221, 5, 172, 1559053008);
INSERT INTO `sys_role_resource` VALUES (2222, 5, 173, 1559053008);
INSERT INTO `sys_role_resource` VALUES (2223, 5, 174, 1559053008);
INSERT INTO `sys_role_resource` VALUES (2224, 5, 246, 1559053008);
INSERT INTO `sys_role_resource` VALUES (2225, 5, 247, 1559053008);
INSERT INTO `sys_role_resource` VALUES (2226, 5, 248, 1559053008);
INSERT INTO `sys_role_resource` VALUES (2227, 5, 249, 1559053008);
INSERT INTO `sys_role_resource` VALUES (2228, 5, 250, 1559053008);
INSERT INTO `sys_role_resource` VALUES (2229, 5, 92, 1559053008);
INSERT INTO `sys_role_resource` VALUES (2230, 5, 98, 1559053008);
INSERT INTO `sys_role_resource` VALUES (2231, 5, 223, 1559053008);
INSERT INTO `sys_role_resource` VALUES (2232, 5, 224, 1559053008);
INSERT INTO `sys_role_resource` VALUES (2233, 5, 225, 1559053008);
INSERT INTO `sys_role_resource` VALUES (2234, 5, 99, 1559053008);
INSERT INTO `sys_role_resource` VALUES (2235, 5, 231, 1559053008);
INSERT INTO `sys_role_resource` VALUES (2236, 5, 232, 1559053008);
INSERT INTO `sys_role_resource` VALUES (2237, 5, 236, 1559053008);
INSERT INTO `sys_role_resource` VALUES (2238, 5, 237, 1559053008);
INSERT INTO `sys_role_resource` VALUES (2239, 5, 238, 1559053008);
INSERT INTO `sys_role_resource` VALUES (2240, 5, 239, 1559053008);
INSERT INTO `sys_role_resource` VALUES (2241, 5, 240, 1559053008);
INSERT INTO `sys_role_resource` VALUES (2242, 5, 241, 1559053008);
INSERT INTO `sys_role_resource` VALUES (2243, 5, 242, 1559053008);
INSERT INTO `sys_role_resource` VALUES (2244, 5, 243, 1559053008);
INSERT INTO `sys_role_resource` VALUES (2245, 5, 244, 1559053008);
INSERT INTO `sys_role_resource` VALUES (2246, 5, 245, 1559053008);
INSERT INTO `sys_role_resource` VALUES (2247, 7, 97, 1560503131);
INSERT INTO `sys_role_resource` VALUES (2248, 7, 268, 1560503131);
INSERT INTO `sys_role_resource` VALUES (2249, 7, 269, 1560503131);
INSERT INTO `sys_role_resource` VALUES (2250, 7, 270, 1560503131);
INSERT INTO `sys_role_resource` VALUES (2251, 7, 271, 1560503131);
INSERT INTO `sys_role_resource` VALUES (2252, 7, 272, 1560503131);
INSERT INTO `sys_role_resource` VALUES (2253, 7, 197, 1560503131);
INSERT INTO `sys_role_resource` VALUES (2254, 7, 198, 1560503131);
INSERT INTO `sys_role_resource` VALUES (2255, 7, 199, 1560503131);
INSERT INTO `sys_role_resource` VALUES (2256, 7, 200, 1560503131);
INSERT INTO `sys_role_resource` VALUES (2257, 7, 201, 1560503131);
INSERT INTO `sys_role_resource` VALUES (2258, 7, 202, 1560503131);
INSERT INTO `sys_role_resource` VALUES (2259, 7, 203, 1560503131);
INSERT INTO `sys_role_resource` VALUES (2260, 7, 204, 1560503131);
INSERT INTO `sys_role_resource` VALUES (2261, 7, 205, 1560503131);
INSERT INTO `sys_role_resource` VALUES (2262, 7, 206, 1560503131);
INSERT INTO `sys_role_resource` VALUES (2263, 7, 187, 1560503131);
INSERT INTO `sys_role_resource` VALUES (2264, 7, 188, 1560503131);
INSERT INTO `sys_role_resource` VALUES (2265, 7, 189, 1560503131);
INSERT INTO `sys_role_resource` VALUES (2266, 7, 190, 1560503131);
INSERT INTO `sys_role_resource` VALUES (2267, 7, 191, 1560503131);
INSERT INTO `sys_role_resource` VALUES (2268, 7, 192, 1560503131);
INSERT INTO `sys_role_resource` VALUES (2269, 7, 193, 1560503131);
INSERT INTO `sys_role_resource` VALUES (2270, 7, 194, 1560503131);
INSERT INTO `sys_role_resource` VALUES (2271, 7, 195, 1560503131);
INSERT INTO `sys_role_resource` VALUES (2272, 7, 196, 1560503131);
INSERT INTO `sys_role_resource` VALUES (2273, 6, 100, 1560740715);
INSERT INTO `sys_role_resource` VALUES (2274, 6, 162, 1560740715);
INSERT INTO `sys_role_resource` VALUES (2275, 6, 163, 1560740715);
INSERT INTO `sys_role_resource` VALUES (2276, 6, 164, 1560740715);
INSERT INTO `sys_role_resource` VALUES (2277, 6, 165, 1560740715);
INSERT INTO `sys_role_resource` VALUES (2278, 6, 166, 1560740715);
INSERT INTO `sys_role_resource` VALUES (2279, 6, 167, 1560740715);
INSERT INTO `sys_role_resource` VALUES (2280, 6, 168, 1560740715);
INSERT INTO `sys_role_resource` VALUES (2281, 6, 169, 1560740715);
INSERT INTO `sys_role_resource` VALUES (2282, 6, 170, 1560740715);
INSERT INTO `sys_role_resource` VALUES (2283, 6, 171, 1560740715);
INSERT INTO `sys_role_resource` VALUES (2284, 6, 253, 1560740715);
INSERT INTO `sys_role_resource` VALUES (2285, 6, 254, 1560740715);
INSERT INTO `sys_role_resource` VALUES (2286, 6, 255, 1560740715);
INSERT INTO `sys_role_resource` VALUES (2287, 6, 256, 1560740715);
INSERT INTO `sys_role_resource` VALUES (2288, 6, 257, 1560740715);
INSERT INTO `sys_role_resource` VALUES (2289, 6, 258, 1560740715);
INSERT INTO `sys_role_resource` VALUES (2290, 6, 259, 1560740715);
INSERT INTO `sys_role_resource` VALUES (2291, 6, 260, 1560740715);
INSERT INTO `sys_role_resource` VALUES (2292, 6, 261, 1560740715);
INSERT INTO `sys_role_resource` VALUES (2293, 6, 262, 1560740715);
INSERT INTO `sys_role_resource` VALUES (2294, 6, 263, 1560740715);
INSERT INTO `sys_role_resource` VALUES (2295, 6, 264, 1560740715);
INSERT INTO `sys_role_resource` VALUES (2296, 6, 265, 1560740715);
INSERT INTO `sys_role_resource` VALUES (2297, 6, 266, 1560740715);
INSERT INTO `sys_role_resource` VALUES (2298, 6, 267, 1560740715);
INSERT INTO `sys_role_resource` VALUES (2299, 6, 273, 1560740715);
INSERT INTO `sys_role_resource` VALUES (2300, 6, 274, 1560740715);
INSERT INTO `sys_role_resource` VALUES (2301, 1, 1, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2302, 1, 76, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2303, 1, 81, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2304, 1, 83, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2305, 1, 84, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2306, 1, 85, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2307, 1, 82, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2308, 1, 78, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2309, 1, 79, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2310, 1, 157, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2311, 1, 159, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2312, 1, 160, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2313, 1, 161, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2314, 1, 158, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2315, 1, 80, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2316, 1, 77, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2317, 1, 53, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2318, 1, 66, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2319, 1, 68, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2320, 1, 69, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2321, 1, 70, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2322, 1, 67, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2323, 1, 55, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2324, 1, 56, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2325, 1, 57, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2326, 1, 54, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2327, 1, 142, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2328, 1, 144, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2329, 1, 152, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2330, 1, 154, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2331, 1, 155, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2332, 1, 156, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2333, 1, 153, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2334, 1, 145, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2335, 1, 146, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2336, 1, 143, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2337, 1, 58, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2338, 1, 71, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2339, 1, 73, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2340, 1, 74, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2341, 1, 75, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2342, 1, 72, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2343, 1, 60, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2344, 1, 61, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2345, 1, 62, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2346, 1, 59, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2347, 1, 2, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2348, 1, 4, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2349, 1, 49, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2350, 1, 50, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2351, 1, 51, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2352, 1, 48, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2353, 1, 87, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2354, 1, 64, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2355, 1, 65, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2356, 1, 86, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2357, 1, 175, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2358, 1, 176, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2359, 1, 177, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2360, 1, 178, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2361, 1, 179, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2362, 1, 180, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2363, 1, 181, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2364, 1, 182, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2365, 1, 183, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2366, 1, 184, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2367, 1, 185, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2368, 1, 186, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2369, 1, 275, 1571682784);
INSERT INTO `sys_role_resource` VALUES (2370, 1, 276, 1571682784);

-- ----------------------------
-- Table structure for sys_schedule
-- ----------------------------
DROP TABLE IF EXISTS `sys_schedule`;
CREATE TABLE `sys_schedule`  (
  `JobId` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `JobName` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '任务名称',
  `JobGroup` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL COMMENT '任务分组',
  `JobStatus` tinyint(4) NOT NULL COMMENT '任务状态， 0 暂停任务；1 启用任务',
  `TriggerType` tinyint(4) NOT NULL COMMENT '触发器类型（0、simple 1、cron）',
  `Cron` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '任务运行时间表达式',
  `AssemblyName` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '任务所在DLL对应的程序集名称',
  `ClassName` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '任务所在类',
  `Remark` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT '任务描述',
  `CreateTime` datetime(0) NOT NULL,
  `UpdateTime` datetime(0) NULL DEFAULT NULL,
  `RunTimes` int(11) NOT NULL DEFAULT 0 COMMENT '执行次数',
  `BeginTime` datetime(0) NOT NULL COMMENT '开始时间',
  `EndTime` datetime(0) NULL DEFAULT NULL COMMENT '结束时间',
  `IntervalSecond` int(11) NULL DEFAULT NULL COMMENT '执行间隔时间, 秒为单位',
  `Url` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NULL DEFAULT NULL COMMENT 'job调用外部的url',
  `Status` tinyint(4) NOT NULL DEFAULT 1 COMMENT '状态 0删除,1未删除',
  PRIMARY KEY (`JobId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_schedule
-- ----------------------------
INSERT INTO `sys_schedule` VALUES (1, '微信AccessToken自动同步任务', 'DefaultJob', 0, 0, '3 * * * * ? ', 'MsSystem.Schedule.Job', 'WxAccessTokenJob', NULL, '2019-04-17 11:05:13', NULL, 0, '2019-04-17 11:05:08', NULL, 0, NULL, 1);
INSERT INTO `sys_schedule` VALUES (2, '微信用户信息同步任务', 'DefaultJob', 0, 0, '0 0 0/1 * * ? ', 'MsSystem.Schedule.Job', 'WxUserInfoJob', NULL, '2019-04-17 11:05:13', NULL, 0, '2019-04-17 11:05:08', NULL, 0, NULL, 1);

-- ----------------------------
-- Table structure for sys_system
-- ----------------------------
DROP TABLE IF EXISTS `sys_system`;
CREATE TABLE `sys_system`  (
  `SystemId` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '系统ID',
  `SystemName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '系统名称',
  `SystemCode` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '系统编码',
  `Memo` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  `IsDel` tinyint(4) NOT NULL DEFAULT 0 COMMENT '是否删除',
  `Sort` int(11) NOT NULL DEFAULT 0 COMMENT '排序',
  `CreateUserId` bigint(20) NOT NULL COMMENT '创建人ID',
  `CreateTime` bigint(20) NULL DEFAULT NULL COMMENT '创建时间',
  `UpdateUserId` bigint(20) NULL DEFAULT NULL COMMENT '更新人',
  `UpdateTime` bigint(20) NULL DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`SystemId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 7 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '系统表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_system
-- ----------------------------
INSERT INTO `sys_system` VALUES (1, '系统管理', 'be1c63a0-63aa-11e7-a221-f97d872f551b', '权限系统', 0, 1, 1, 1498481230, 0, 1537448282);
INSERT INTO `sys_system` VALUES (2, '工作流', 'c3cd9538-63aa-11e7-a221-f97d872f551b', '工作流', 0, 4, 1, 1498481230, 0, 1537447644);
INSERT INTO `sys_system` VALUES (3, '微信系统', 'c7526ad9-63aa-11e7-a221-f97d872f551b', '微信系统', 0, 3, 1, 1498481230, 0, 1537447638);
INSERT INTO `sys_system` VALUES (4, '系统测试', '252110d2-cb56-46c8-9dc2-64c3d7e23b21', '系统测试0000', 1, 0, 1, 1499494026, 0, 0);
INSERT INTO `sys_system` VALUES (5, '行政办公系统', 'd65fb3df-d342-41c9-ad9a-3faedbb5b0dc', '行政办公系统', 0, 2, 1, 1500955747, 0, 1537448139);
INSERT INTO `sys_system` VALUES (6, '行政办公', '8130e64e-912b-4ce5-aae5-89ba3b5d97a9', '行政办公系统', 1, 0, 1, 1509884116, NULL, NULL);

-- ----------------------------
-- Table structure for sys_user
-- ----------------------------
DROP TABLE IF EXISTS `sys_user`;
CREATE TABLE `sys_user`  (
  `UserId` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `Account` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '登录账号',
  `UserName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '用户名',
  `JobNumber` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '工号',
  `Password` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '密码',
  `HeadImg` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '头像地址',
  `IsDel` tinyint(4) NOT NULL DEFAULT 0 COMMENT '是否删除 1:是，0：否',
  `CreateUserId` bigint(20) NOT NULL COMMENT '创建人ID',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  `UpdateUserId` bigint(20) NULL DEFAULT NULL COMMENT '更新人',
  `UpdateTime` bigint(20) NULL DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`UserId`) USING BTREE,
  INDEX `Account`(`Account`) USING BTREE,
  INDEX `JobNumber`(`JobNumber`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 22 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '用户表（作用于全部系统）' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_user
-- ----------------------------
INSERT INTO `sys_user` VALUES (1, 'wms', 'wms', '20180101', '40BD001563085FC35165329EA1FF5C5ECBDBBEEF', '/uploadfile/342bd59b-edf4-48cf-aa27-d13e5a0b70df.jpeg', 0, 1, 12, 1, 1542809506);
INSERT INTO `sys_user` VALUES (4, 'wangwu', '王五123', '20180102', '40BD001563085FC35165329EA1FF5C5ECBDBBEEF', '/uploadfile/342bd59b-edf4-48cf-aa27-d13e5a0b70df.jpeg', 0, 0, 1498571322, 1, 1560497334);
INSERT INTO `sys_user` VALUES (5, 'zhangsan', '张三', '20180103', '40BD001563085FC35165329EA1FF5C5ECBDBBEEF', '/uploadfile/1ca449c6-24ed-4b78-a032-6005990ff707.jpeg', 0, 0, 1499750510, 1, 1538660578);
INSERT INTO `sys_user` VALUES (6, 'lisi', '李四aa', '20180104', '40BD001563085FC35165329EA1FF5C5ECBDBBEEF', '/uploadfile/342bd59b-edf4-48cf-aa27-d13e5a0b70df.jpeg', 0, 0, 1499750523, NULL, NULL);
INSERT INTO `sys_user` VALUES (7, '123', '123', '20180105', '40BD001563085FC35165329EA1FF5C5ECBDBBEEF', '/uploadfile/342bd59b-edf4-48cf-aa27-d13e5a0b70df.jpeg', 0, 0, 1499750534, 1, 1557909906);
INSERT INTO `sys_user` VALUES (8, '321', '321', '20180106', '40BD001563085FC35165329EA1FF5C5ECBDBBEEF', '/uploadfile/342bd59b-edf4-48cf-aa27-d13e5a0b70df.jpeg', 1, 0, 1499750544, NULL, NULL);
INSERT INTO `sys_user` VALUES (9, '1234', '1234', '20180107', '40BD001563085FC35165329EA1FF5C5ECBDBBEEF', '/uploadfile/342bd59b-edf4-48cf-aa27-d13e5a0b70df.jpeg', 1, 0, 1499750555, NULL, NULL);
INSERT INTO `sys_user` VALUES (10, '1234', '1234', '20180108', '40BD001563085FC35165329EA1FF5C5ECBDBBEEF', '/uploadfile/342bd59b-edf4-48cf-aa27-d13e5a0b70df.jpeg', 1, 0, 1499750555, NULL, NULL);
INSERT INTO `sys_user` VALUES (11, 'asd', 'asd', '20180109', '40BD001563085FC35165329EA1FF5C5ECBDBBEEF', '/uploadfile/342bd59b-edf4-48cf-aa27-d13e5a0b70df.jpeg', 1, 0, 1499750583, NULL, NULL);
INSERT INTO `sys_user` VALUES (12, 'asd', 'asd', '20180110', '40BD001563085FC35165329EA1FF5C5ECBDBBEEF', '/uploadfile/342bd59b-edf4-48cf-aa27-d13e5a0b70df.jpeg', 1, 0, 1499750584, NULL, NULL);
INSERT INTO `sys_user` VALUES (13, 'aaa', 'aaa', '20180111', '40BD001563085FC35165329EA1FF5C5ECBDBBEEF', '/uploadfile/342bd59b-edf4-48cf-aa27-d13e5a0b70df.jpeg', 1, 0, 1499750592, NULL, NULL);
INSERT INTO `sys_user` VALUES (14, 'aaa', 'aaa', '20180112', '40BD001563085FC35165329EA1FF5C5ECBDBBEEF', '/uploadfile/342bd59b-edf4-48cf-aa27-d13e5a0b70df.jpeg', 1, 0, 1499750592, NULL, NULL);
INSERT INTO `sys_user` VALUES (15, 'bbb', 'bbb', '20180113', '40BD001563085FC35165329EA1FF5C5ECBDBBEEF', '/uploadfile/342bd59b-edf4-48cf-aa27-d13e5a0b70df.jpeg', 1, 0, 1501310757, NULL, NULL);
INSERT INTO `sys_user` VALUES (16, 'ccc', 'ccc', '20180114', '40BD001563085FC35165329EA1FF5C5ECBDBBEEF', '/uploadfile/342bd59b-edf4-48cf-aa27-d13e5a0b70df.jpeg', 1, 0, 1501310765, NULL, NULL);
INSERT INTO `sys_user` VALUES (17, 'ddd', 'ddd', '20180115', '40BD001563085FC35165329EA1FF5C5ECBDBBEEF', '/uploadfile/342bd59b-edf4-48cf-aa27-d13e5a0b70df.jpeg', 1, 0, 1501310778, NULL, NULL);
INSERT INTO `sys_user` VALUES (18, 'eee', 'eee', '20180116', '40BD001563085FC35165329EA1FF5C5ECBDBBEEF', '/uploadfile/342bd59b-edf4-48cf-aa27-d13e5a0b70df.jpeg', 1, 0, 1501310789, NULL, NULL);
INSERT INTO `sys_user` VALUES (19, 'asd', 'asd', '20180117', '40BD001563085FC35165329EA1FF5C5ECBDBBEEF', '/uploadfile/342bd59b-edf4-48cf-aa27-d13e5a0b70df.jpeg', 1, 0, 1509869141, NULL, NULL);
INSERT INTO `sys_user` VALUES (20, '123', '123', '2018102098', 'A93C168323147D1135503939396CAC628DC194C5', '/uploadfile/342bd59b-edf4-48cf-aa27-d13e5a0b70df.jpeg', 1, 0, 1539993966, NULL, NULL);
INSERT INTO `sys_user` VALUES (21, 'cs', 'cs', '2019041302', '40BD001563085FC35165329EA1FF5C5ECBDBBEEF', '/uploadfile/342bd59b-edf4-48cf-aa27-d13e5a0b70df.jpeg', 1, 0, 1555123202, NULL, NULL);

-- ----------------------------
-- Table structure for sys_user_dept
-- ----------------------------
DROP TABLE IF EXISTS `sys_user_dept`;
CREATE TABLE `sys_user_dept`  (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `DeptId` bigint(20) NOT NULL COMMENT '部门ID',
  `UserId` bigint(20) NOT NULL COMMENT '用户ID',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间戳',
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `DeptId`(`DeptId`) USING BTREE,
  INDEX `UserId`(`UserId`) USING BTREE,
  CONSTRAINT `sys_user_dept_ibfk_1` FOREIGN KEY (`DeptId`) REFERENCES `sys_dept` (`DeptId`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `sys_user_dept_ibfk_2` FOREIGN KEY (`UserId`) REFERENCES `sys_user` (`UserId`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 7 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_user_dept
-- ----------------------------
INSERT INTO `sys_user_dept` VALUES (4, 4, 6, 1557303745);
INSERT INTO `sys_user_dept` VALUES (5, 3, 1, 1559469199);
INSERT INTO `sys_user_dept` VALUES (6, 3, 4, 1559792769);

-- ----------------------------
-- Table structure for sys_user_role
-- ----------------------------
DROP TABLE IF EXISTS `sys_user_role`;
CREATE TABLE `sys_user_role`  (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `UserId` bigint(20) NOT NULL COMMENT '用户ID',
  `RoleId` bigint(20) NOT NULL COMMENT '角色ID',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间戳',
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `UserId`(`UserId`) USING BTREE,
  INDEX `RoleId`(`RoleId`) USING BTREE,
  CONSTRAINT `sys_user_role_ibfk_1` FOREIGN KEY (`UserId`) REFERENCES `sys_user` (`UserId`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `sys_user_role_ibfk_2` FOREIGN KEY (`RoleId`) REFERENCES `sys_role` (`RoleId`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 65 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '用户角色关联表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_user_role
-- ----------------------------
INSERT INTO `sys_user_role` VALUES (47, 1, 1, 1547040313);
INSERT INTO `sys_user_role` VALUES (48, 1, 5, 1547040313);
INSERT INTO `sys_user_role` VALUES (49, 1, 7, 1547040313);
INSERT INTO `sys_user_role` VALUES (50, 1, 6, 1547040313);
INSERT INTO `sys_user_role` VALUES (51, 4, 1, 1557214422);
INSERT INTO `sys_user_role` VALUES (52, 4, 5, 1557214422);
INSERT INTO `sys_user_role` VALUES (56, 6, 6, 1557303730);
INSERT INTO `sys_user_role` VALUES (58, 6, 1, 1557304953);
INSERT INTO `sys_user_role` VALUES (59, 7, 5, 1557909952);
INSERT INTO `sys_user_role` VALUES (60, 7, 6, 1557909952);
INSERT INTO `sys_user_role` VALUES (61, 4, 6, 1559053056);
INSERT INTO `sys_user_role` VALUES (62, 5, 1, 1559702623);
INSERT INTO `sys_user_role` VALUES (63, 5, 5, 1559702623);
INSERT INTO `sys_user_role` VALUES (64, 5, 6, 1559702623);

-- ----------------------------
-- Table structure for sys_workflowsql
-- ----------------------------
DROP TABLE IF EXISTS `sys_workflowsql`;
CREATE TABLE `sys_workflowsql`  (
  `Name` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '流程sql名称,必须是以sys_为开头，用于判断属于哪个系统，方便调用接口',
  `FlowSQL` text CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '流程SQL，执行结果必须是一行一列',
  `Param` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '参数名。以英文 , 分割',
  `SQLType` tinyint(4) NOT NULL DEFAULT 0 COMMENT '类型，0：选人节点，必须返回的是用户ID，1：连线条件，条件通过返回的是一行一列的数据，不通过没有任何返回结果',
  `Status` int(11) NOT NULL DEFAULT 1 COMMENT '状态',
  `Remark` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  `CreateUserId` bigint(20) NOT NULL COMMENT '创建人ID',
  PRIMARY KEY (`Name`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '用于工作流获取权限系统数据' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sys_workflowsql
-- ----------------------------
INSERT INTO `sys_workflowsql` VALUES ('sys_getdeptleader', 'SELECT DISTINCT dl.`UserId` FROM sys_user u \r\nLEFT JOIN sys_user_dept ud ON ud.`UserId`=u.`UserId`\r\nLEFT JOIN sys_dept_leader dl ON dl.`DeptId`=ud.`DeptId`\r\nWHERE u.`UserId`=@userid', 'userid', 0, 1, '权限系统，获取部门负责人', 1, 1);
