/*
 Navicat Premium Data Transfer

 Source Server         : docker mysql
 Source Server Type    : MySQL
 Source Server Version : 50727
 Source Host           : localhost:3306
 Source Schema         : mssystemoa

 Target Server Type    : MySQL
 Target Server Version : 50727
 File Encoding         : 65001

 Date: 13/11/2019 10:58:06
*/
CREATE DATABASE /*!32312 IF NOT EXISTS*/`mssystemoa` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `mssystemoa`;
SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for oa_chat
-- ----------------------------
DROP TABLE IF EXISTS `oa_chat`;
CREATE TABLE `oa_chat`  (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `Sender` bigint(20) NOT NULL COMMENT '发送方',
  `Message` text CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '消息',
  `Receiver` bigint(20) NOT NULL COMMENT '接收方',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of oa_chat
-- ----------------------------
INSERT INTO `oa_chat` VALUES (1, 5, '你好', 1, 1560898931);
INSERT INTO `oa_chat` VALUES (2, 1, '你好', 5, 1560898950);

-- ----------------------------
-- Table structure for oa_leave
-- ----------------------------
DROP TABLE IF EXISTS `oa_leave`;
CREATE TABLE `oa_leave`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `LeaveCode` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '请假编号',
  `Title` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '标题',
  `UserId` int(11) NOT NULL COMMENT '请假人',
  `AgentId` int(11) NOT NULL COMMENT '工作代理人',
  `LeaveType` tinyint(4) NOT NULL COMMENT '请假类型',
  `Reason` varchar(1000) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '请假原因',
  `Days` int(11) NOT NULL COMMENT '请假天数',
  `StartTime` bigint(20) NOT NULL COMMENT '开始时间',
  `EndTime` bigint(20) NOT NULL COMMENT '结束时间',
  `CreateUserId` int(11) NOT NULL COMMENT '创建人',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  `FlowStatus` int(11) NULL DEFAULT -1 COMMENT '流程状态',
  `FlowTime` bigint(20) NULL DEFAULT NULL COMMENT '流程操作时间戳',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '员工请假' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of oa_leave
-- ----------------------------
INSERT INTO `oa_leave` VALUES (1, '15604170854388', '想看看外面的世界', 0, 0, 0, '想看看外面的世界', 7, 1560441600, 1560960000, 1, 1560417085, 1, 1560417476);
INSERT INTO `oa_leave` VALUES (2, '15604180604164', '测试2', 0, 0, 0, '测试', 2, 1560441600, 1560528000, 1, 1560418060, 1, 1560419404);

-- ----------------------------
-- Table structure for oa_mail
-- ----------------------------
DROP TABLE IF EXISTS `oa_mail`;
CREATE TABLE `oa_mail`  (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `Title` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `Content` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `SendStatus` tinyint(4) NOT NULL COMMENT '发送状态',
  `Sender` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '发送人',
  `SendMail` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '发送地址',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for oa_mail_config
-- ----------------------------
DROP TABLE IF EXISTS `oa_mail_config`;
CREATE TABLE `oa_mail_config`  (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `Host` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '邮件服务地址',
  `Port` int(11) NOT NULL COMMENT '端口',
  `SecureSocketOptions` tinyint(4) NOT NULL DEFAULT 1 COMMENT 'Secure socket options.',
  `UserName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '账号',
  `Password` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '密码',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 2 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '邮件服务配置' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of oa_mail_config
-- ----------------------------
INSERT INTO `oa_mail_config` VALUES (1, '	smtp.qq.com', 587, 1, '2636256005@qq.com', 'snewsyiqgyagecdd');

-- ----------------------------
-- Table structure for oa_message
-- ----------------------------
DROP TABLE IF EXISTS `oa_message`;
CREATE TABLE `oa_message`  (
  `Id` bigint(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `MsgType` int(11) NOT NULL COMMENT '消息类型',
  `FaceUserType` tinyint(4) NOT NULL COMMENT '面向人员类型',
  `Title` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '标题',
  `IsLocal` tinyint(4) NOT NULL COMMENT '是否是本地消息',
  `TargetType` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '跳转方式',
  `Link` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL COMMENT '链接地址',
  `Content` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL COMMENT '消息内容',
  `IsEnable` tinyint(4) NOT NULL COMMENT '是否立即生效',
  `StartTime` bigint(20) NULL DEFAULT NULL COMMENT '开始时间',
  `EndTime` bigint(20) NULL DEFAULT NULL COMMENT '结束时间',
  `IsExecuted` tinyint(4) NOT NULL COMMENT '是否执行过',
  `IsDel` tinyint(4) NOT NULL COMMENT '是否删除',
  `MakerUserId` bigint(20) NULL DEFAULT NULL COMMENT '编制人ID',
  `CreateUserId` bigint(20) NOT NULL COMMENT '创建人Id',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '系统消息表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of oa_message
-- ----------------------------
INSERT INTO `oa_message` VALUES (1, 1, 0, '测试', 1, 'tab', NULL, '测试', 1, 0, 0, 1, 0, 0, 1, 1555213291);
INSERT INTO `oa_message` VALUES (2, 0, 0, '测试2', 1, 'blank', NULL, NULL, 1, 0, 0, 1, 0, 0, 1, 1555315893);

-- ----------------------------
-- Table structure for oa_message_user
-- ----------------------------
DROP TABLE IF EXISTS `oa_message_user`;
CREATE TABLE `oa_message_user`  (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `MessageId` bigint(20) NOT NULL COMMENT '消息ID',
  `UserId` bigint(20) NOT NULL COMMENT '用户ID',
  `IsRead` tinyint(4) UNSIGNED NOT NULL DEFAULT 0 COMMENT '是否已读',
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `MessageId`(`MessageId`) USING BTREE,
  CONSTRAINT `oa_message_user_ibfk_1` FOREIGN KEY (`MessageId`) REFERENCES `oa_message` (`Id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '消息用户关系表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for oa_work
-- ----------------------------
DROP TABLE IF EXISTS `oa_work`;
CREATE TABLE `oa_work`  (
  `WorkId` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `WorkType` tinyint(4) NOT NULL COMMENT '类型：1日报，2周报，3月报',
  `Content` varchar(1000) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '已完成工作',
  `PlanContent` varchar(1000) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '计划完成工作',
  `NeedHelpContent` varchar(1000) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '需要协助的工作',
  `Memo` varchar(1000) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  `IsDel` tinyint(4) NOT NULL DEFAULT 0 COMMENT '是否删除',
  `ReportDate` bigint(20) NOT NULL COMMENT '汇报日期',
  `CreateUserId` int(11) NOT NULL COMMENT '创建人ID',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`WorkId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 3 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '我的工作' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of oa_work
-- ----------------------------
INSERT INTO `oa_work` VALUES (1, 0, '123', '123', '123', NULL, 0, 0, 1, 0);
INSERT INTO `oa_work` VALUES (2, 0, NULL, NULL, NULL, NULL, 0, 0, 1, 0);

-- ----------------------------
-- Table structure for oa_work_reporter
-- ----------------------------
DROP TABLE IF EXISTS `oa_work_reporter`;
CREATE TABLE `oa_work_reporter`  (
  `ReportId` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `WorkId` int(11) NOT NULL COMMENT '工作id',
  `Reporter` int(11) NOT NULL COMMENT '汇报人',
  `ReadDate` bigint(20) NULL DEFAULT NULL COMMENT '阅读时间',
  `Memo` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  PRIMARY KEY (`ReportId`) USING BTREE,
  INDEX `WorkId`(`WorkId`) USING BTREE,
  CONSTRAINT `oa_work_reporter_ibfk_1` FOREIGN KEY (`WorkId`) REFERENCES `oa_work` (`WorkId`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '汇总人列表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for oa_workflowsql
-- ----------------------------
DROP TABLE IF EXISTS `oa_workflowsql`;
CREATE TABLE `oa_workflowsql`  (
  `Name` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '流程sql名称,必须是以oa_为开头，用于判断属于哪个系统，方便调用接口',
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
-- Records of oa_workflowsql
-- ----------------------------
INSERT INTO `oa_workflowsql` VALUES ('oa_leaveLessThenThreeDays', 'SELECT ol.`Id` FROM `oa_leave` ol WHERE ol.`Days`<=3 AND ol.`CreateUserId`=@userid AND ol.`Id`=@formid', 'userid,formid', 1, 1, '请假时间小于等于三天判断', 1, 1);
INSERT INTO `oa_workflowsql` VALUES ('oa_leaveMoreThenThreeDays', 'SELECT ol.`Id` FROM `oa_leave` ol WHERE ol.`Days` > 3 AND ol.`CreateUserId`=@userid AND ol.`Id`=@formid', 'userid,formid', 1, 1, '请假时间大于三天判断', 1, 1);

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

SET FOREIGN_KEY_CHECKS = 1;
