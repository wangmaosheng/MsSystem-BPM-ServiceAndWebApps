CREATE DATABASE /*!32312 IF NOT EXISTS*/`mssystemwf` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `mssystemwf`;

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;



-- ----------------------------
-- Table structure for wf_workflow
-- ----------------------------
DROP TABLE IF EXISTS `wf_workflow`;
CREATE TABLE `wf_workflow`  (
  `FlowId` char(36) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '主键',
  `CategoryId` char(36) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '分类ID',
  `FormId` char(36) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '表单ID',
  `FlowCode` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '编码',
  `FlowName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '流程名称',
  `FlowContent` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL COMMENT '流程JSON',
  `FlowVersion` int(11) NOT NULL DEFAULT 0 COMMENT '流程版本',
  `Memo` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  `Enable` int(11) NOT NULL DEFAULT 1 COMMENT '是否启用',
  `CreateUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '创建人',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`FlowId`) USING BTREE,
  INDEX `CategoryId`(`CategoryId`) USING BTREE,
  INDEX `FormId`(`FormId`) USING BTREE,
  CONSTRAINT `wf_workflow_ibfk_1` FOREIGN KEY (`CategoryId`) REFERENCES `wf_workflow_category` (`Id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `wf_workflow_ibfk_2` FOREIGN KEY (`FormId`) REFERENCES `wf_workflow_form` (`FormId`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '工作流表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of wf_workflow
-- ----------------------------
INSERT INTO `wf_workflow` VALUES ('011980a7-0ba3-4752-964e-12d88ca5c54c', '9e9fc7e7-8792-40f2-97bc-8b42e583126d', '3b1ceb38-e1ee-4f15-a709-d6dd3a399c77', '15580575818487', '员工借款', '{\"title\":\"员工借款\",\"nodes\":[{\"name\":\"开始\",\"left\":238,\"top\":29,\"type\":\"start round mix\",\"id\":\"f9104625-252a-49c8-91d4-9401509fceb5\",\"width\":26,\"height\":26,\"alt\":true,\"setInfo\":{}},{\"name\":\"部门经理\",\"left\":390,\"top\":93,\"type\":\"task\",\"id\":\"f5cef31d-cb13-4195-86f3-7e2c96f345ee\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SQL\",\"Nodedesignatedata\":{\"users\":[],\"roles\":[],\"orgs\":[],\"sql\":\"sys_getdeptleader\"}}},{\"name\":\"财务总监\",\"left\":423,\"top\":213,\"type\":\"task\",\"id\":\"d8842622-f5e8-4336-b9cd-4383e5bcec3d\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SPECIAL_USER\",\"Nodedesignatedata\":{\"users\":[\"5\"],\"roles\":[],\"orgs\":[]}}},{\"name\":\"结束\",\"left\":694,\"top\":273,\"type\":\"end round\",\"id\":\"38ebf6f4-5a82-4fb6-9342-94c0f95f6820\",\"width\":26,\"height\":26,\"alt\":true,\"setInfo\":{}}],\"lines\":[{\"type\":\"tb\",\"M\":110,\"from\":\"f9104625-252a-49c8-91d4-9401509fceb5\",\"to\":\"f5cef31d-cb13-4195-86f3-7e2c96f345ee\",\"id\":\"b279111d-eb6a-4d8a-86b6-135de84a732a\",\"name\":\"\",\"dash\":false},{\"type\":\"tb\",\"M\":225.5,\"from\":\"f5cef31d-cb13-4195-86f3-7e2c96f345ee\",\"to\":\"d8842622-f5e8-4336-b9cd-4383e5bcec3d\",\"id\":\"596c6d67-715e-4332-809b-7a4b8ba7fa50\",\"name\":\"\",\"dash\":false},{\"type\":\"tb\",\"M\":256,\"from\":\"d8842622-f5e8-4336-b9cd-4383e5bcec3d\",\"to\":\"38ebf6f4-5a82-4fb6-9342-94c0f95f6820\",\"id\":\"f3ddca8b-135b-43f6-b0bc-e42060a233af\",\"name\":\"\",\"dash\":false,\"alt\":true}],\"areas\":[],\"initNum\":8}', 0, '测试流程', 1, '1', 1558057581);
INSERT INTO `wf_workflow` VALUES ('2b1b17c4-69ca-474b-977a-e8b1f1382e89', '9e9fc7e7-8792-40f2-97bc-8b42e583126d', 'fd4a4efc-7df2-49c4-9ffc-f84db346cac7', '15601364386520', '通知测试', '{\"title\":\"通知测试\",\"nodes\":[{\"name\":\"node_1\",\"left\":264,\"top\":83,\"type\":\"start round mix\",\"id\":\"1474e4c4-d512-49aa-8681-8720b4168554\",\"width\":26,\"height\":26,\"alt\":true},{\"name\":\"部门负责人\",\"left\":473,\"top\":84,\"type\":\"task\",\"id\":\"e0080e39-b227-45c1-81d1-ca18213d80d6\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SQL\",\"Nodedesignatedata\":{\"users\":[],\"roles\":[],\"orgs\":[],\"sql\":\"sys_getdeptleader\"}}},{\"name\":\"wms\",\"left\":480,\"top\":247,\"type\":\"view\",\"id\":\"1f702b3c-b514-47f3-a761-9190e4a8e965\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"CREATEUSER\",\"Nodedesignatedata\":{\"users\":[],\"roles\":[],\"orgs\":[],\"sql\":\"\"}}},{\"name\":\"node_4\",\"left\":837,\"top\":84,\"type\":\"end round\",\"id\":\"06941d43-5d7a-4a4f-a096-1235d493a24c\",\"width\":26,\"height\":26,\"alt\":true}],\"lines\":[{\"type\":\"sl\",\"from\":\"1474e4c4-d512-49aa-8681-8720b4168554\",\"to\":\"e0080e39-b227-45c1-81d1-ca18213d80d6\",\"id\":\"18365384-6f16-41dc-aeb6-ce384cc11d94\",\"name\":\"\",\"dash\":false},{\"type\":\"sl\",\"from\":\"e0080e39-b227-45c1-81d1-ca18213d80d6\",\"to\":\"1f702b3c-b514-47f3-a761-9190e4a8e965\",\"id\":\"a543a2e5-9045-4742-b974-477acdd37ffe\",\"name\":\"\",\"dash\":false},{\"type\":\"sl\",\"from\":\"e0080e39-b227-45c1-81d1-ca18213d80d6\",\"to\":\"06941d43-5d7a-4a4f-a096-1235d493a24c\",\"id\":\"148f4d46-2ee7-405d-a2e3-270a5b6d2539\",\"name\":\"\",\"dash\":false}],\"areas\":[],\"initNum\":8}', 0, '通知测试', 1, '1', 1560136438);
INSERT INTO `wf_workflow` VALUES ('477e4199-aaf0-4e21-9eed-088922a83d58', '9e9fc7e7-8792-40f2-97bc-8b42e583126d', '041f7de8-dd83-4aec-a253-e181b77cc40e', '15563796431067', '员工请假', '{\"title\":\"员工请假\",\"nodes\":[{\"name\":\"开始\",\"left\":67,\"top\":44,\"type\":\"start round mix\",\"id\":\"77825e68-4a61-43b8-9081-504088b332e6\",\"width\":26,\"height\":26,\"alt\":true,\"setInfo\":{}},{\"name\":\"王五\",\"left\":438,\"top\":49,\"type\":\"task\",\"id\":\"5fb04da8-7113-4f80-9c91-be19db2c1a9c\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SPECIAL_USER\",\"Nodedesignatedata\":{\"users\":[\"4\"],\"roles\":[],\"orgs\":[],\"sql\":\"\"}},\"areaId\":\"77046665-290a-4ae1-a5cf-a69e11d41f7f\"},{\"name\":\"结束\",\"left\":809,\"top\":238,\"type\":\"end round\",\"id\":\"6dae3d55-04fc-4112-824f-e87542a03779\",\"width\":26,\"height\":26,\"alt\":true,\"setInfo\":{},\"areaId\":\"77046665-290a-4ae1-a5cf-a69e11d41f7f\"},{\"name\":\"张三\",\"left\":778,\"top\":52,\"type\":\"task\",\"id\":\"634b9765-ac0e-4272-bfab-f5b260c7fde8\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SPECIAL_USER\",\"Nodedesignatedata\":{\"users\":[\"5\"],\"roles\":[],\"orgs\":[],\"sql\":\"\"}},\"areaId\":\"77046665-290a-4ae1-a5cf-a69e11d41f7f\"},{\"name\":\"部门负责人\",\"left\":178,\"top\":49,\"type\":\"task\",\"id\":\"33e53484-5b48-4210-a62c-949dd7d6dbaa\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SQL\",\"Nodedesignatedata\":{\"users\":[],\"roles\":[],\"orgs\":[],\"sql\":\"sys_getdeptleader\"}}}],\"lines\":[{\"type\":\"sl\",\"from\":\"634b9765-ac0e-4272-bfab-f5b260c7fde8\",\"to\":\"6dae3d55-04fc-4112-824f-e87542a03779\",\"id\":\"c764f55f-125b-48e6-8f37-8f281788d960\",\"name\":\"\",\"dash\":false},{\"type\":\"sl\",\"from\":\"77825e68-4a61-43b8-9081-504088b332e6\",\"to\":\"33e53484-5b48-4210-a62c-949dd7d6dbaa\",\"id\":\"c587ca2a-c95f-491a-b55a-a27c67df3037\",\"name\":\"\",\"dash\":false},{\"type\":\"sl\",\"from\":\"33e53484-5b48-4210-a62c-949dd7d6dbaa\",\"to\":\"5fb04da8-7113-4f80-9c91-be19db2c1a9c\",\"id\":\"5923dd84-6010-4003-bf4c-d4ee8605e945\",\"setInfo\":{\"CustomSQL\":\"oa_leaveMoreThenThreeDays\"},\"name\":\">3天\",\"dash\":false},{\"type\":\"sl\",\"from\":\"5fb04da8-7113-4f80-9c91-be19db2c1a9c\",\"to\":\"634b9765-ac0e-4272-bfab-f5b260c7fde8\",\"id\":\"0ebead88-6942-4563-aa5f-8dbd4c453fe5\",\"name\":\"\",\"dash\":false},{\"type\":\"tb\",\"M\":210.5,\"from\":\"33e53484-5b48-4210-a62c-949dd7d6dbaa\",\"to\":\"6dae3d55-04fc-4112-824f-e87542a03779\",\"id\":\"ba6765a0-f21e-436a-a094-73a2b03183b1\",\"setInfo\":{\"CustomSQL\":\"oa_leaveLessThenThreeDays\"},\"name\":\"<=3天\",\"dash\":false}],\"areas\":[],\"initNum\":22}', 0, '测试流程', 1, '1', 1556379643);
INSERT INTO `wf_workflow` VALUES ('a9dd987c-f25f-4297-94be-465c5044b076', '9e9fc7e7-8792-40f2-97bc-8b42e583126d', '4dfacbf1-40bd-4fe0-b4fa-249713f28659', '15594687698457', '新版测试', '{\"title\":\"新版测试\",\"nodes\":[{\"name\":\"开始\",\"left\":282,\"top\":116,\"type\":\"start round mix\",\"id\":\"5231704b-8c9f-4155-9e9d-7cdcdc9c57fe\",\"width\":26,\"height\":26,\"alt\":true,\"setInfo\":{}},{\"name\":\"结束\",\"left\":955,\"top\":245,\"type\":\"end round\",\"id\":\"75e7fb37-1d3d-4be6-9e65-aa6ffc78bccf\",\"width\":26,\"height\":26,\"alt\":true,\"setInfo\":{}},{\"name\":\"部门负责人\",\"left\":514,\"top\":169,\"type\":\"task\",\"id\":\"d96914cd-d85e-47b4-acdf-1b1fbecd78fc\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SQL\",\"Nodedesignatedata\":{\"users\":[],\"roles\":[],\"orgs\":[],\"sql\":\"sys_getdeptleader\"}}}],\"lines\":[{\"type\":\"sl\",\"from\":\"5231704b-8c9f-4155-9e9d-7cdcdc9c57fe\",\"to\":\"d96914cd-d85e-47b4-acdf-1b1fbecd78fc\",\"id\":\"95aa2ed5-aaf8-410d-a858-c864ca843dfa\",\"name\":\"\",\"dash\":false},{\"type\":\"sl\",\"from\":\"d96914cd-d85e-47b4-acdf-1b1fbecd78fc\",\"to\":\"75e7fb37-1d3d-4be6-9e65-aa6ffc78bccf\",\"id\":\"bb1567d9-0d70-44e8-a093-a0352fccc700\",\"name\":\"\",\"dash\":false}],\"areas\":[],\"initNum\":6}', 0, '新版测试', 1, '1', 1559468769);

-- ----------------------------
-- Table structure for wf_workflow_category
-- ----------------------------
DROP TABLE IF EXISTS `wf_workflow_category`;
CREATE TABLE `wf_workflow_category`  (
  `Id` char(36) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '主键',
  `Name` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '分类名称',
  `ParentId` char(36) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '父级ID',
  `Memo` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  `Status` int(11) NULL DEFAULT 1 COMMENT '状态',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  `CreateUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '流程分类' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of wf_workflow_category
-- ----------------------------
INSERT INTO `wf_workflow_category` VALUES ('0b286d49-162a-451b-a1d4-4ab5f2eeddb8', '测试分类', '00000000-0000-0000-0000-000000000000', '你好啊', 0, 1559180780, '1');
INSERT INTO `wf_workflow_category` VALUES ('0e9deb2e-941e-423a-85f0-4092b0c46204', '测试分类', '00000000-0000-0000-0000-000000000000', '你好啊你', 0, 1559180844, '1');
INSERT INTO `wf_workflow_category` VALUES ('9e9fc7e7-8792-40f2-97bc-8b42e583126d', '通用流程', '00000000-0000-0000-0000-000000000000', '通用流程', 1, 1556072828, '1');

-- ----------------------------
-- Table structure for wf_workflow_form
-- ----------------------------
DROP TABLE IF EXISTS `wf_workflow_form`;
CREATE TABLE `wf_workflow_form`  (
  `FormId` char(36) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '表单ID',
  `FormName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '表单名称',
  `FormType` int(11) NULL DEFAULT NULL COMMENT '表单类型',
  `Content` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL COMMENT '表单内容',
  `OriginalContent` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL COMMENT '原始表单内容',
  `FormUrl` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '表单地址',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  `CreateUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '创建人',
  PRIMARY KEY (`FormId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '流程表单' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of wf_workflow_form
-- ----------------------------
INSERT INTO `wf_workflow_form` VALUES ('041f7de8-dd83-4aec-a253-e181b77cc40e', '请假', 1, NULL, NULL, '/OA/Leave/Show', 1556075724, '1');
INSERT INTO `wf_workflow_form` VALUES ('3b1ceb38-e1ee-4f15-a709-d6dd3a399c77', '员工借款', 0, '\n                                    \n                                    \n                                    \n	<div class=\"form-group\">\n		<label class=\"col-sm-3 control-label\">标题：</label>\n		<div class=\"col-sm-9\">\n			<input name=\"FlowParam_1\" type=\"text\" class=\"form-control required\">\n		</div>\n	</div>\n	<div class=\"form-group\">\n		<label class=\"col-sm-3 control-label\">借款金额：</label>\n		<div class=\"col-sm-9\">\n			  <input name=\"FlowParam_2\" type=\"text\" class=\"form-control required\">\n		</div>\n	</div>\n	<div class=\"form-group\">\n		<label class=\"col-sm-3 control-label\">借款原因：</label>\n		<div class=\"col-sm-9\">\n			<textarea name=\"FlowParam_4\" class=\"form-control required\"></textarea>\n		</div>\n	</div>\n\n                                \n                                \n                                ', '\n                                    \n                                    \n                                    \n	<div class=\"form-group\">\n		<label class=\"col-sm-3 control-label\">标题：</label>\n		<div class=\"col-sm-9\">\n			<input type=\"text\" class=\"form-control required\">\n		</div>\n	</div>\n	<div class=\"form-group\">\n		<label class=\"col-sm-3 control-label\">借款金额：</label>\n		<div class=\"col-sm-9\">\n			  <input type=\"text\" class=\"form-control required\">\n		</div>\n	</div>\n	<div class=\"form-group\">\n		<label class=\"col-sm-3 control-label\">借款原因：</label>\n		<div class=\"col-sm-9\">\n			<textarea class=\"form-control required\"></textarea>\n		</div>\n	</div>\n\n                                \n                                \n                                ', NULL, 1558057039, '1');
INSERT INTO `wf_workflow_form` VALUES ('4dfacbf1-40bd-4fe0-b4fa-249713f28659', '新版测试', 0, '新版测试\n                                    \n                                ', '新版测试\n                                    \n                                ', NULL, 1559466362, '1');
INSERT INTO `wf_workflow_form` VALUES ('fd4a4efc-7df2-49c4-9ffc-f84db346cac7', '通知测试', 0, '通知测试\n                                    \n                                ', '通知测试\n                                    \n                                ', NULL, 1560136422, '1');

-- ----------------------------
-- Table structure for wf_workflow_instance
-- ----------------------------
DROP TABLE IF EXISTS `wf_workflow_instance`;
CREATE TABLE `wf_workflow_instance`  (
  `InstanceId` char(36) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '实例ID',
  `FlowId` char(36) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '流程ID',
  `Code` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '实例编号',
  `ActivityId` char(36) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '当前节点ID',
  `ActivityType` int(11) NULL DEFAULT NULL COMMENT '当前节点类型',
  `ActivityName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '当前节点名称',
  `PreviousId` char(36) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '上一个节点ID',
  `MakerList` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '执行人',
  `FlowContent` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL COMMENT '流程JSON内容',
  `IsFinish` int(11) NULL DEFAULT NULL COMMENT '流程是否结束',
  `Status` int(11) NOT NULL DEFAULT 0 COMMENT '用户操作状态',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  `CreateUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '创建人',
  `CreateUserName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人姓名',
  `UpdateTime` bigint(20) NOT NULL COMMENT '实例被修改时间',
  PRIMARY KEY (`InstanceId`) USING BTREE,
  INDEX `FlowId`(`FlowId`) USING BTREE,
  CONSTRAINT `wf_workflow_instance_ibfk_1` FOREIGN KEY (`FlowId`) REFERENCES `wf_workflow` (`FlowId`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '流程实例' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of wf_workflow_instance
-- ----------------------------
INSERT INTO `wf_workflow_instance` VALUES ('59e8c3f0-db02-4b11-978a-64bd54ddb6c8', '2b1b17c4-69ca-474b-977a-e8b1f1382e89', '15602457896721', '06941d43-5d7a-4a4f-a096-1235d493a24c', 4, 'node_4', 'e0080e39-b227-45c1-81d1-ca18213d80d6', '', '{\"title\":\"通知测试\",\"nodes\":[{\"name\":\"node_1\",\"left\":264,\"top\":83,\"type\":\"start round mix\",\"id\":\"1474e4c4-d512-49aa-8681-8720b4168554\",\"width\":26,\"height\":26,\"alt\":true},{\"name\":\"部门负责人\",\"left\":473,\"top\":84,\"type\":\"task\",\"id\":\"e0080e39-b227-45c1-81d1-ca18213d80d6\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SQL\",\"Nodedesignatedata\":{\"users\":[],\"roles\":[],\"orgs\":[],\"sql\":\"sys_getdeptleader\"}}},{\"name\":\"wms\",\"left\":480,\"top\":247,\"type\":\"view\",\"id\":\"1f702b3c-b514-47f3-a761-9190e4a8e965\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"CREATEUSER\",\"Nodedesignatedata\":{\"users\":[],\"roles\":[],\"orgs\":[],\"sql\":\"\"}}},{\"name\":\"node_4\",\"left\":837,\"top\":84,\"type\":\"end round\",\"id\":\"06941d43-5d7a-4a4f-a096-1235d493a24c\",\"width\":26,\"height\":26,\"alt\":true}],\"lines\":[{\"type\":\"sl\",\"from\":\"1474e4c4-d512-49aa-8681-8720b4168554\",\"to\":\"e0080e39-b227-45c1-81d1-ca18213d80d6\",\"id\":\"18365384-6f16-41dc-aeb6-ce384cc11d94\",\"name\":\"\",\"dash\":false},{\"type\":\"sl\",\"from\":\"e0080e39-b227-45c1-81d1-ca18213d80d6\",\"to\":\"1f702b3c-b514-47f3-a761-9190e4a8e965\",\"id\":\"a543a2e5-9045-4742-b974-477acdd37ffe\",\"name\":\"\",\"dash\":false},{\"type\":\"sl\",\"from\":\"e0080e39-b227-45c1-81d1-ca18213d80d6\",\"to\":\"06941d43-5d7a-4a4f-a096-1235d493a24c\",\"id\":\"148f4d46-2ee7-405d-a2e3-270a5b6d2539\",\"name\":\"\",\"dash\":false}],\"areas\":[],\"initNum\":8}', 1, 1, 1560245789, '1', 'wms', 1560246314);
INSERT INTO `wf_workflow_instance` VALUES ('9be2c4e3-9dc5-43c3-acc0-eb3e388373f5', '011980a7-0ba3-4752-964e-12d88ca5c54c', '15604151753683', '38ebf6f4-5a82-4fb6-9342-94c0f95f6820', 4, '结束', 'd8842622-f5e8-4336-b9cd-4383e5bcec3d', '', '{\"title\":\"员工借款\",\"nodes\":[{\"name\":\"开始\",\"left\":238,\"top\":29,\"type\":\"start round mix\",\"id\":\"f9104625-252a-49c8-91d4-9401509fceb5\",\"width\":26,\"height\":26,\"alt\":true,\"setInfo\":{}},{\"name\":\"部门经理\",\"left\":390,\"top\":93,\"type\":\"task\",\"id\":\"f5cef31d-cb13-4195-86f3-7e2c96f345ee\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SQL\",\"Nodedesignatedata\":{\"users\":[],\"roles\":[],\"orgs\":[],\"sql\":\"sys_getdeptleader\"}}},{\"name\":\"财务总监\",\"left\":423,\"top\":213,\"type\":\"task\",\"id\":\"d8842622-f5e8-4336-b9cd-4383e5bcec3d\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SPECIAL_USER\",\"Nodedesignatedata\":{\"users\":[\"5\"],\"roles\":[],\"orgs\":[]}}},{\"name\":\"结束\",\"left\":694,\"top\":273,\"type\":\"end round\",\"id\":\"38ebf6f4-5a82-4fb6-9342-94c0f95f6820\",\"width\":26,\"height\":26,\"alt\":true,\"setInfo\":{}}],\"lines\":[{\"type\":\"tb\",\"M\":110,\"from\":\"f9104625-252a-49c8-91d4-9401509fceb5\",\"to\":\"f5cef31d-cb13-4195-86f3-7e2c96f345ee\",\"id\":\"b279111d-eb6a-4d8a-86b6-135de84a732a\",\"name\":\"\",\"dash\":false},{\"type\":\"tb\",\"M\":225.5,\"from\":\"f5cef31d-cb13-4195-86f3-7e2c96f345ee\",\"to\":\"d8842622-f5e8-4336-b9cd-4383e5bcec3d\",\"id\":\"596c6d67-715e-4332-809b-7a4b8ba7fa50\",\"name\":\"\",\"dash\":false},{\"type\":\"tb\",\"M\":256,\"from\":\"d8842622-f5e8-4336-b9cd-4383e5bcec3d\",\"to\":\"38ebf6f4-5a82-4fb6-9342-94c0f95f6820\",\"id\":\"f3ddca8b-135b-43f6-b0bc-e42060a233af\",\"name\":\"\",\"dash\":false,\"alt\":true}],\"areas\":[],\"initNum\":8}', 1, 1, 1560415175, '1', 'wms', 1560415232);
INSERT INTO `wf_workflow_instance` VALUES ('a32a6150-e4ae-4264-aeb8-93a89638679c', '477e4199-aaf0-4e21-9eed-088922a83d58', '15604170867061', '6dae3d55-04fc-4112-824f-e87542a03779', 4, '结束', '634b9765-ac0e-4272-bfab-f5b260c7fde8', '', '{\"title\":\"员工请假\",\"nodes\":[{\"name\":\"开始\",\"left\":67,\"top\":44,\"type\":\"start round mix\",\"id\":\"77825e68-4a61-43b8-9081-504088b332e6\",\"width\":26,\"height\":26,\"alt\":true,\"setInfo\":{}},{\"name\":\"王五\",\"left\":438,\"top\":49,\"type\":\"task\",\"id\":\"5fb04da8-7113-4f80-9c91-be19db2c1a9c\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SPECIAL_USER\",\"Nodedesignatedata\":{\"users\":[\"4\"],\"roles\":[],\"orgs\":[],\"sql\":\"\"}},\"areaId\":\"77046665-290a-4ae1-a5cf-a69e11d41f7f\"},{\"name\":\"结束\",\"left\":809,\"top\":238,\"type\":\"end round\",\"id\":\"6dae3d55-04fc-4112-824f-e87542a03779\",\"width\":26,\"height\":26,\"alt\":true,\"setInfo\":{},\"areaId\":\"77046665-290a-4ae1-a5cf-a69e11d41f7f\"},{\"name\":\"张三\",\"left\":778,\"top\":52,\"type\":\"task\",\"id\":\"634b9765-ac0e-4272-bfab-f5b260c7fde8\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SPECIAL_USER\",\"Nodedesignatedata\":{\"users\":[\"5\"],\"roles\":[],\"orgs\":[],\"sql\":\"\"}},\"areaId\":\"77046665-290a-4ae1-a5cf-a69e11d41f7f\"},{\"name\":\"部门负责人\",\"left\":178,\"top\":49,\"type\":\"task\",\"id\":\"33e53484-5b48-4210-a62c-949dd7d6dbaa\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SQL\",\"Nodedesignatedata\":{\"users\":[],\"roles\":[],\"orgs\":[],\"sql\":\"sys_getdeptleader\"}}}],\"lines\":[{\"type\":\"sl\",\"from\":\"634b9765-ac0e-4272-bfab-f5b260c7fde8\",\"to\":\"6dae3d55-04fc-4112-824f-e87542a03779\",\"id\":\"c764f55f-125b-48e6-8f37-8f281788d960\",\"name\":\"\",\"dash\":false},{\"type\":\"sl\",\"from\":\"77825e68-4a61-43b8-9081-504088b332e6\",\"to\":\"33e53484-5b48-4210-a62c-949dd7d6dbaa\",\"id\":\"c587ca2a-c95f-491a-b55a-a27c67df3037\",\"name\":\"\",\"dash\":false},{\"type\":\"sl\",\"from\":\"33e53484-5b48-4210-a62c-949dd7d6dbaa\",\"to\":\"5fb04da8-7113-4f80-9c91-be19db2c1a9c\",\"id\":\"5923dd84-6010-4003-bf4c-d4ee8605e945\",\"setInfo\":{\"CustomSQL\":\"oa_leaveMoreThenThreeDays\"},\"name\":\">3天\",\"dash\":false},{\"type\":\"sl\",\"from\":\"5fb04da8-7113-4f80-9c91-be19db2c1a9c\",\"to\":\"634b9765-ac0e-4272-bfab-f5b260c7fde8\",\"id\":\"0ebead88-6942-4563-aa5f-8dbd4c453fe5\",\"name\":\"\",\"dash\":false},{\"type\":\"tb\",\"M\":210.5,\"from\":\"33e53484-5b48-4210-a62c-949dd7d6dbaa\",\"to\":\"6dae3d55-04fc-4112-824f-e87542a03779\",\"id\":\"ba6765a0-f21e-436a-a094-73a2b03183b1\",\"setInfo\":{\"CustomSQL\":\"oa_leaveLessThenThreeDays\"},\"name\":\"<=3天\",\"dash\":false}],\"areas\":[],\"initNum\":22}', 1, 1, 1560417086, '1', 'wms', 1560417476);
INSERT INTO `wf_workflow_instance` VALUES ('d8420a67-c3f4-4f93-a5da-a5598094f447', '477e4199-aaf0-4e21-9eed-088922a83d58', '15604180620754', '6dae3d55-04fc-4112-824f-e87542a03779', 4, '结束', '77825e68-4a61-43b8-9081-504088b332e6', '', '{\"title\":\"员工请假\",\"nodes\":[{\"name\":\"开始\",\"left\":67,\"top\":44,\"type\":\"start round mix\",\"id\":\"77825e68-4a61-43b8-9081-504088b332e6\",\"width\":26,\"height\":26,\"alt\":true,\"setInfo\":{}},{\"name\":\"王五\",\"left\":438,\"top\":49,\"type\":\"task\",\"id\":\"5fb04da8-7113-4f80-9c91-be19db2c1a9c\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SPECIAL_USER\",\"Nodedesignatedata\":{\"users\":[\"4\"],\"roles\":[],\"orgs\":[],\"sql\":\"\"}},\"areaId\":\"77046665-290a-4ae1-a5cf-a69e11d41f7f\"},{\"name\":\"结束\",\"left\":809,\"top\":238,\"type\":\"end round\",\"id\":\"6dae3d55-04fc-4112-824f-e87542a03779\",\"width\":26,\"height\":26,\"alt\":true,\"setInfo\":{},\"areaId\":\"77046665-290a-4ae1-a5cf-a69e11d41f7f\"},{\"name\":\"张三\",\"left\":778,\"top\":52,\"type\":\"task\",\"id\":\"634b9765-ac0e-4272-bfab-f5b260c7fde8\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SPECIAL_USER\",\"Nodedesignatedata\":{\"users\":[\"5\"],\"roles\":[],\"orgs\":[],\"sql\":\"\"}},\"areaId\":\"77046665-290a-4ae1-a5cf-a69e11d41f7f\"},{\"name\":\"部门负责人\",\"left\":178,\"top\":49,\"type\":\"task\",\"id\":\"33e53484-5b48-4210-a62c-949dd7d6dbaa\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SQL\",\"Nodedesignatedata\":{\"users\":[],\"roles\":[],\"orgs\":[],\"sql\":\"sys_getdeptleader\"}}}],\"lines\":[{\"type\":\"sl\",\"from\":\"634b9765-ac0e-4272-bfab-f5b260c7fde8\",\"to\":\"6dae3d55-04fc-4112-824f-e87542a03779\",\"id\":\"c764f55f-125b-48e6-8f37-8f281788d960\",\"name\":\"\",\"dash\":false},{\"type\":\"sl\",\"from\":\"77825e68-4a61-43b8-9081-504088b332e6\",\"to\":\"33e53484-5b48-4210-a62c-949dd7d6dbaa\",\"id\":\"c587ca2a-c95f-491a-b55a-a27c67df3037\",\"name\":\"\",\"dash\":false},{\"type\":\"sl\",\"from\":\"33e53484-5b48-4210-a62c-949dd7d6dbaa\",\"to\":\"5fb04da8-7113-4f80-9c91-be19db2c1a9c\",\"id\":\"5923dd84-6010-4003-bf4c-d4ee8605e945\",\"setInfo\":{\"CustomSQL\":\"oa_leaveMoreThenThreeDays\"},\"name\":\">3天\",\"dash\":false},{\"type\":\"sl\",\"from\":\"5fb04da8-7113-4f80-9c91-be19db2c1a9c\",\"to\":\"634b9765-ac0e-4272-bfab-f5b260c7fde8\",\"id\":\"0ebead88-6942-4563-aa5f-8dbd4c453fe5\",\"name\":\"\",\"dash\":false},{\"type\":\"tb\",\"M\":210.5,\"from\":\"33e53484-5b48-4210-a62c-949dd7d6dbaa\",\"to\":\"6dae3d55-04fc-4112-824f-e87542a03779\",\"id\":\"ba6765a0-f21e-436a-a094-73a2b03183b1\",\"setInfo\":{\"CustomSQL\":\"oa_leaveLessThenThreeDays\"},\"name\":\"<=3天\",\"dash\":false}],\"areas\":[],\"initNum\":22}', 1, 1, 1560418062, '1', 'wms', 1560419404);

-- ----------------------------
-- Table structure for wf_workflow_instance_form
-- ----------------------------
DROP TABLE IF EXISTS `wf_workflow_instance_form`;
CREATE TABLE `wf_workflow_instance_form`  (
  `Id` char(36) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '主键',
  `InstanceId` char(36) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '流程实例ID',
  `FormId` char(36) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '表单ID',
  `FormContent` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL COMMENT '表单内容/对于表单ID',
  `FormType` int(11) NULL DEFAULT NULL COMMENT '表单类型',
  `FormUrl` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '表单地址',
  `FormData` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL COMMENT '表单数据JSON',
  `CreateUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '创建人',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `InstanceId`(`InstanceId`) USING BTREE,
  INDEX `FormId`(`FormId`) USING BTREE,
  CONSTRAINT `wf_workflow_instance_form_ibfk_1` FOREIGN KEY (`InstanceId`) REFERENCES `wf_workflow_instance` (`InstanceId`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `wf_workflow_instance_form_ibfk_2` FOREIGN KEY (`FormId`) REFERENCES `wf_workflow_form` (`FormId`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '流程实例表单关联表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of wf_workflow_instance_form
-- ----------------------------
INSERT INTO `wf_workflow_instance_form` VALUES ('6432959b-620f-4fbe-9f69-9b851574810f', '9be2c4e3-9dc5-43c3-acc0-eb3e388373f5', '3b1ceb38-e1ee-4f15-a709-d6dd3a399c77', '\n                                    \n                                    \n                                    \n	<div class=\"form-group\">\n		<label class=\"col-sm-3 control-label\">标题：</label>\n		<div class=\"col-sm-9\">\n			<input name=\"FlowParam_1\" type=\"text\" class=\"form-control required\">\n		</div>\n	</div>\n	<div class=\"form-group\">\n		<label class=\"col-sm-3 control-label\">借款金额：</label>\n		<div class=\"col-sm-9\">\n			  <input name=\"FlowParam_2\" type=\"text\" class=\"form-control required\">\n		</div>\n	</div>\n	<div class=\"form-group\">\n		<label class=\"col-sm-3 control-label\">借款原因：</label>\n		<div class=\"col-sm-9\">\n			<textarea name=\"FlowParam_4\" class=\"form-control required\"></textarea>\n		</div>\n	</div>\n\n                                \n                                \n                                ', 0, NULL, '{\"FlowParam_1\":\"测试\",\"FlowParam_2\":\"100\",\"FlowParam_4\":\"测试\"}', '1', 1560415175);
INSERT INTO `wf_workflow_instance_form` VALUES ('69ad72e6-99cc-4a00-8bc4-88025e141cff', 'a32a6150-e4ae-4264-aeb8-93a89638679c', '041f7de8-dd83-4aec-a253-e181b77cc40e', '1', 1, '/OA/Leave/Show', '1', '1', 1560417087);
INSERT INTO `wf_workflow_instance_form` VALUES ('b744d9b4-70b3-4151-b67b-fd2efc952369', 'd8420a67-c3f4-4f93-a5da-a5598094f447', '041f7de8-dd83-4aec-a253-e181b77cc40e', '2', 1, '/OA/Leave/Show', '2', '1', 1560418062);
INSERT INTO `wf_workflow_instance_form` VALUES ('f938467a-b223-417a-b593-853d5b6ee0e4', '59e8c3f0-db02-4b11-978a-64bd54ddb6c8', 'fd4a4efc-7df2-49c4-9ffc-f84db346cac7', '通知测试\n                                    \n                                ', 0, NULL, '{}', '1', 1560245789);

-- ----------------------------
-- Table structure for wf_workflow_notice
-- ----------------------------
DROP TABLE IF EXISTS `wf_workflow_notice`;
CREATE TABLE `wf_workflow_notice`  (
  `Id` char(36) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '主键',
  `InstanceId` char(36) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '流程实例ID',
  `NodeId` char(36) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '通知节点ID',
  `NodeName` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '通知节点名称',
  `Maker` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '执行人',
  `IsTransition` tinyint(4) NOT NULL DEFAULT 0 COMMENT '是否已经流转过',
  `Status` tinyint(4) NOT NULL DEFAULT 1 COMMENT '状态，退回时候用',
  `IsRead` tinyint(4) NOT NULL DEFAULT 0 COMMENT '是否已阅',
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `InstanceId`(`InstanceId`) USING BTREE,
  CONSTRAINT `wf_workflow_notice_ibfk_1` FOREIGN KEY (`InstanceId`) REFERENCES `wf_workflow_instance` (`InstanceId`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '流程通知节点表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of wf_workflow_notice
-- ----------------------------
INSERT INTO `wf_workflow_notice` VALUES ('1d6af7db-3117-46b9-a5dd-8ba0cb9514b6', '59e8c3f0-db02-4b11-978a-64bd54ddb6c8', '1f702b3c-b514-47f3-a761-9190e4a8e965', 'wms', '1', 1, 1, 1);

-- ----------------------------
-- Table structure for wf_workflow_operation_history
-- ----------------------------
DROP TABLE IF EXISTS `wf_workflow_operation_history`;
CREATE TABLE `wf_workflow_operation_history`  (
  `OperationId` char(36) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '主键',
  `InstanceId` char(36) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '实例进程ID',
  `NodeId` char(36) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '节点ID',
  `NodeName` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '节点名称',
  `TransitionType` int(11) NULL DEFAULT NULL COMMENT '流转类型',
  `Content` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '操作内容',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  `CreateUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '创建人',
  `CreateUserName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人姓名',
  PRIMARY KEY (`OperationId`) USING BTREE,
  INDEX `InstanceId`(`InstanceId`) USING BTREE,
  CONSTRAINT `wf_workflow_operation_history_ibfk_1` FOREIGN KEY (`InstanceId`) REFERENCES `wf_workflow_instance` (`InstanceId`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '流程操作历史记录' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of wf_workflow_operation_history
-- ----------------------------
INSERT INTO `wf_workflow_operation_history` VALUES ('21d8c76f-d6bf-479c-9fa4-68e67a8bf57d', 'a32a6150-e4ae-4264-aeb8-93a89638679c', '33e53484-5b48-4210-a62c-949dd7d6dbaa', '部门负责人', 2, '同意！', 1560417353, '1', 'wms');
INSERT INTO `wf_workflow_operation_history` VALUES ('28e938aa-6d37-41a5-a282-5a8d44eac04b', 'a32a6150-e4ae-4264-aeb8-93a89638679c', '5fb04da8-7113-4f80-9c91-be19db2c1a9c', '王五', 2, '同意！', 1560417448, '4', '王五123');
INSERT INTO `wf_workflow_operation_history` VALUES ('3530b628-0493-4562-bdd8-105039cdbac8', '59e8c3f0-db02-4b11-978a-64bd54ddb6c8', '1474e4c4-d512-49aa-8681-8720b4168554', 'node_1', 1, '提交流程', 1560245806, '1', 'wms');
INSERT INTO `wf_workflow_operation_history` VALUES ('4606236e-e153-4132-85c7-cd0957fbac23', '59e8c3f0-db02-4b11-978a-64bd54ddb6c8', '1f702b3c-b514-47f3-a761-9190e4a8e965', 'wms', 9, '流程已阅', 1560301605, '1', 'wms');
INSERT INTO `wf_workflow_operation_history` VALUES ('672584ec-6f97-4613-b286-863f0fae05de', '59e8c3f0-db02-4b11-978a-64bd54ddb6c8', 'e0080e39-b227-45c1-81d1-ca18213d80d6', '部门负责人', 2, '同意！', 1560246314, '1', 'wms');
INSERT INTO `wf_workflow_operation_history` VALUES ('68ad122c-ccf6-4e66-aa0f-888db2f0e58d', '9be2c4e3-9dc5-43c3-acc0-eb3e388373f5', 'f9104625-252a-49c8-91d4-9401509fceb5', '开始', 1, '提交流程', 1560415177, '1', 'wms');
INSERT INTO `wf_workflow_operation_history` VALUES ('8e5b642c-780c-4c15-bfa8-1962446cbb6b', 'a32a6150-e4ae-4264-aeb8-93a89638679c', '77825e68-4a61-43b8-9081-504088b332e6', '开始', 1, '提交流程', 1560417087, '1', 'wms');
INSERT INTO `wf_workflow_operation_history` VALUES ('9ce5e7d3-4729-4906-a166-65e55a1372bd', 'a32a6150-e4ae-4264-aeb8-93a89638679c', '634b9765-ac0e-4272-bfab-f5b260c7fde8', '张三', 2, '同意！', 1560417476, '5', '张三');
INSERT INTO `wf_workflow_operation_history` VALUES ('a45896aa-daa0-4d79-8da8-e21205dbbc0e', 'd8420a67-c3f4-4f93-a5da-a5598094f447', '33e53484-5b48-4210-a62c-949dd7d6dbaa', '部门负责人', 2, '同意！', 1560419404, '1', 'wms');
INSERT INTO `wf_workflow_operation_history` VALUES ('cc601b1c-6ca4-48c8-970f-f0336a6f5152', '9be2c4e3-9dc5-43c3-acc0-eb3e388373f5', 'f5cef31d-cb13-4195-86f3-7e2c96f345ee', '部门经理', 2, '同意！', 1560415190, '1', 'wms');
INSERT INTO `wf_workflow_operation_history` VALUES ('fcf9bdd4-5f22-4ea3-afec-bb492e603961', 'd8420a67-c3f4-4f93-a5da-a5598094f447', '77825e68-4a61-43b8-9081-504088b332e6', '开始', 1, '提交流程', 1560418062, '1', 'wms');
INSERT INTO `wf_workflow_operation_history` VALUES ('fe1adecd-1985-48b0-b888-55e3cff873d7', '9be2c4e3-9dc5-43c3-acc0-eb3e388373f5', 'd8842622-f5e8-4336-b9cd-4383e5bcec3d', '财务总监', 2, '同意！', 1560415232, '5', '张三');

-- ----------------------------
-- Table structure for wf_workflow_transition_history
-- ----------------------------
DROP TABLE IF EXISTS `wf_workflow_transition_history`;
CREATE TABLE `wf_workflow_transition_history`  (
  `TransitionId` char(36) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '主键',
  `InstanceId` char(36) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '流程实例ID',
  `FromNodeId` char(36) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '开始节点Id',
  `FromNodeType` int(11) NULL DEFAULT NULL COMMENT '开始节点类型',
  `FromNodName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '开始节点名称',
  `ToNodeId` char(36) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '结束节点Id',
  `ToNodeType` int(11) NULL DEFAULT NULL COMMENT '结束节点类型',
  `ToNodeName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '结束节点名称',
  `TransitionState` int(11) NULL DEFAULT NULL COMMENT '转化状态',
  `IsFinish` int(11) NULL DEFAULT NULL COMMENT '是否结束',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  `CreateUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '创建人',
  `CreateUserName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '创建人姓名',
  PRIMARY KEY (`TransitionId`) USING BTREE,
  INDEX `InstanceId`(`InstanceId`) USING BTREE,
  CONSTRAINT `wf_workflow_transition_history_ibfk_1` FOREIGN KEY (`InstanceId`) REFERENCES `wf_workflow_instance` (`InstanceId`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '流程流转历史表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of wf_workflow_transition_history
-- ----------------------------
INSERT INTO `wf_workflow_transition_history` VALUES ('066307c9-640e-4bb0-911d-766a3cb024ab', 'd8420a67-c3f4-4f93-a5da-a5598094f447', '77825e68-4a61-43b8-9081-504088b332e6', 3, '开始', '33e53484-5b48-4210-a62c-949dd7d6dbaa', 2, '部门负责人', 0, 0, 1560418062, '1', 'wms');
INSERT INTO `wf_workflow_transition_history` VALUES ('10f1968e-1f80-4c3d-8e93-5f13835c2618', '9be2c4e3-9dc5-43c3-acc0-eb3e388373f5', 'f9104625-252a-49c8-91d4-9401509fceb5', 3, '开始', 'f5cef31d-cb13-4195-86f3-7e2c96f345ee', 2, '部门经理', 0, 0, 1560415177, '1', 'wms');
INSERT INTO `wf_workflow_transition_history` VALUES ('28480957-8b93-4d9f-948c-3862f42b18a8', 'a32a6150-e4ae-4264-aeb8-93a89638679c', '33e53484-5b48-4210-a62c-949dd7d6dbaa', 2, '部门负责人', '5fb04da8-7113-4f80-9c91-be19db2c1a9c', 2, '王五', 0, 0, 1560417353, '1', 'wms');
INSERT INTO `wf_workflow_transition_history` VALUES ('5c0c3c95-31fe-4a1c-99a4-a045999339a2', '9be2c4e3-9dc5-43c3-acc0-eb3e388373f5', 'f5cef31d-cb13-4195-86f3-7e2c96f345ee', 2, '部门经理', 'd8842622-f5e8-4336-b9cd-4383e5bcec3d', 2, '财务总监', 0, 0, 1560415190, '1', 'wms');
INSERT INTO `wf_workflow_transition_history` VALUES ('653192e9-e944-434f-9f40-c6693d5df821', 'a32a6150-e4ae-4264-aeb8-93a89638679c', '5fb04da8-7113-4f80-9c91-be19db2c1a9c', 2, '王五', '634b9765-ac0e-4272-bfab-f5b260c7fde8', 2, '张三', 0, 0, 1560417448, '4', '王五123');
INSERT INTO `wf_workflow_transition_history` VALUES ('65bf952f-9594-4392-882f-6f6580519f5b', '59e8c3f0-db02-4b11-978a-64bd54ddb6c8', 'e0080e39-b227-45c1-81d1-ca18213d80d6', 2, '部门负责人', '06941d43-5d7a-4a4f-a096-1235d493a24c', 4, 'node_4', 0, 1, 1560246314, '1', 'wms');
INSERT INTO `wf_workflow_transition_history` VALUES ('8da53ca8-a71f-4ee3-a7db-e7d673860e6c', 'a32a6150-e4ae-4264-aeb8-93a89638679c', '634b9765-ac0e-4272-bfab-f5b260c7fde8', 2, '张三', '6dae3d55-04fc-4112-824f-e87542a03779', 4, '结束', 0, 1, 1560417476, '5', '张三');
INSERT INTO `wf_workflow_transition_history` VALUES ('b5125db1-a882-464e-9d5a-a4df3319d976', 'd8420a67-c3f4-4f93-a5da-a5598094f447', '33e53484-5b48-4210-a62c-949dd7d6dbaa', 2, '部门负责人', '6dae3d55-04fc-4112-824f-e87542a03779', 4, '结束', 0, 1, 1560419404, '1', 'wms');
INSERT INTO `wf_workflow_transition_history` VALUES ('c03193be-6a26-4ef3-ae3c-0f840f03152e', '9be2c4e3-9dc5-43c3-acc0-eb3e388373f5', 'd8842622-f5e8-4336-b9cd-4383e5bcec3d', 2, '财务总监', '38ebf6f4-5a82-4fb6-9342-94c0f95f6820', 4, '结束', 0, 1, 1560415232, '5', '张三');
INSERT INTO `wf_workflow_transition_history` VALUES ('c07e31a5-4f8d-465f-8586-4d87adf12be4', 'a32a6150-e4ae-4264-aeb8-93a89638679c', '77825e68-4a61-43b8-9081-504088b332e6', 3, '开始', '33e53484-5b48-4210-a62c-949dd7d6dbaa', 2, '部门负责人', 0, 0, 1560417087, '1', 'wms');
INSERT INTO `wf_workflow_transition_history` VALUES ('e2daf996-cf1a-4ed0-8b40-22f6095499b5', '59e8c3f0-db02-4b11-978a-64bd54ddb6c8', '1474e4c4-d512-49aa-8681-8720b4168554', 3, 'node_1', 'e0080e39-b227-45c1-81d1-ca18213d80d6', 2, '部门负责人', 0, 0, 1560245806, '1', 'wms');

-- ----------------------------
-- Table structure for wf_workflowsql
-- ----------------------------
DROP TABLE IF EXISTS `wf_workflowsql`;
CREATE TABLE `wf_workflowsql`  (
  `Name` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '流程sql名称,必须是以wf_为开头，用于判断属于哪个系统，方便调用接口',
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
-- Records of wf_workflowsql
-- ----------------------------
INSERT INTO `wf_workflowsql` VALUES ('wf_agree', 'SELECT 1 ', NULL, 0, 1, '同意、通过', 1, 1);
INSERT INTO `wf_workflowsql` VALUES ('wf_deprecate', 'SELECT 0 ', NULL, 0, 1, '不同意、不通过', 1, 1);
