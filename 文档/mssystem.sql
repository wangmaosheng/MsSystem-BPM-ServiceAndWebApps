/*
SQLyog Professional v12.08 (64 bit)
MySQL - 5.7.19-log : Database - mssystem
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`mssystem` /*!40100 DEFAULT CHARACTER SET utf8 */;

USE `mssystem`;

/*Table structure for table `cap.published` */

DROP TABLE IF EXISTS `cap.published`;

CREATE TABLE `cap.published` (
  `Id` bigint(20) NOT NULL,
  `Version` varchar(20) DEFAULT NULL,
  `Name` varchar(200) NOT NULL,
  `Content` longtext,
  `Retries` int(11) DEFAULT NULL,
  `Added` datetime NOT NULL,
  `ExpiresAt` datetime DEFAULT NULL,
  `StatusName` varchar(40) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

/*Table structure for table `cap.received` */

DROP TABLE IF EXISTS `cap.received`;

CREATE TABLE `cap.received` (
  `Id` bigint(20) NOT NULL,
  `Version` varchar(20) DEFAULT NULL,
  `Name` varchar(400) NOT NULL,
  `Group` varchar(200) DEFAULT NULL,
  `Content` longtext,
  `Retries` int(11) DEFAULT NULL,
  `Added` datetime NOT NULL,
  `ExpiresAt` datetime DEFAULT NULL,
  `StatusName` varchar(50) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

/*Table structure for table `oa_leave` */

DROP TABLE IF EXISTS `oa_leave`;

CREATE TABLE `oa_leave` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `LeaveCode` varchar(30) NOT NULL COMMENT '请假编号',
  `Title` varchar(500) DEFAULT NULL COMMENT '标题',
  `UserId` int(11) NOT NULL COMMENT '请假人',
  `AgentId` int(11) NOT NULL COMMENT '工作代理人',
  `LeaveType` tinyint(4) NOT NULL COMMENT '请假类型',
  `Reason` varchar(1000) DEFAULT NULL COMMENT '请假原因',
  `Days` int(11) NOT NULL COMMENT '请假天数',
  `StartTime` bigint(20) NOT NULL COMMENT '开始时间',
  `EndTime` bigint(20) NOT NULL COMMENT '结束时间',
  `CreateUserId` int(11) NOT NULL COMMENT '创建人',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  `FlowStatus` int(11) DEFAULT '-1' COMMENT '流程状态',
  `FlowTime` bigint(20) DEFAULT NULL COMMENT '流程操作时间戳',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COMMENT='员工请假';

/*Table structure for table `oa_message` */

DROP TABLE IF EXISTS `oa_message`;

CREATE TABLE `oa_message` (
  `Id` bigint(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `MsgType` int(11) NOT NULL COMMENT '消息类型',
  `FaceUserType` tinyint(4) NOT NULL COMMENT '面向人员类型',
  `Title` varchar(200) DEFAULT NULL COMMENT '标题',
  `IsLocal` tinyint(4) NOT NULL COMMENT '是否是本地消息',
  `TargetType` varchar(50) NOT NULL COMMENT '跳转方式',
  `Link` text COMMENT '链接地址',
  `Content` text COMMENT '消息内容',
  `IsEnable` tinyint(4) NOT NULL COMMENT '是否立即生效',
  `StartTime` bigint(20) DEFAULT NULL COMMENT '开始时间',
  `EndTime` bigint(20) DEFAULT NULL COMMENT '结束时间',
  `IsExecuted` tinyint(4) NOT NULL COMMENT '是否执行过',
  `IsDel` tinyint(4) NOT NULL COMMENT '是否删除',
  `MakerUserId` bigint(20) DEFAULT NULL COMMENT '编制人ID',
  `CreateUserId` bigint(20) NOT NULL COMMENT '创建人Id',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COMMENT='系统消息表';

/*Table structure for table `oa_message_user` */

DROP TABLE IF EXISTS `oa_message_user`;

CREATE TABLE `oa_message_user` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `MessageId` bigint(20) NOT NULL COMMENT '消息ID',
  `UserId` bigint(20) NOT NULL COMMENT '用户ID',
  PRIMARY KEY (`Id`),
  KEY `MessageId` (`MessageId`),
  CONSTRAINT `oa_message_user_ibfk_1` FOREIGN KEY (`MessageId`) REFERENCES `oa_message` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='消息用户关系表';

/*Table structure for table `oa_message_user_read` */

DROP TABLE IF EXISTS `oa_message_user_read`;

CREATE TABLE `oa_message_user_read` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `MessageId` bigint(20) NOT NULL COMMENT '消息ID',
  `UserId` bigint(20) NOT NULL COMMENT '用户ID',
  PRIMARY KEY (`Id`),
  KEY `MessageId` (`MessageId`),
  CONSTRAINT `oa_message_user_read_ibfk_1` FOREIGN KEY (`MessageId`) REFERENCES `oa_message` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COMMENT='用户消息已读表';

/*Table structure for table `oa_work` */

DROP TABLE IF EXISTS `oa_work`;

CREATE TABLE `oa_work` (
  `WorkId` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `WorkType` tinyint(4) NOT NULL COMMENT '类型：1日报，2周报，3月报',
  `Content` varchar(1000) DEFAULT NULL COMMENT '已完成工作',
  `PlanContent` varchar(1000) DEFAULT NULL COMMENT '计划完成工作',
  `NeedHelpContent` varchar(1000) DEFAULT NULL COMMENT '需要协助的工作',
  `Memo` varchar(1000) DEFAULT NULL COMMENT '备注',
  `IsDel` tinyint(4) NOT NULL DEFAULT '0' COMMENT '是否删除',
  `ReportDate` bigint(20) NOT NULL COMMENT '汇报日期',
  `CreateUserId` int(11) NOT NULL COMMENT '创建人ID',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`WorkId`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COMMENT='我的工作';

/*Table structure for table `oa_work_reporter` */

DROP TABLE IF EXISTS `oa_work_reporter`;

CREATE TABLE `oa_work_reporter` (
  `ReportId` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `WorkId` int(11) NOT NULL COMMENT '工作id',
  `Reporter` int(11) NOT NULL COMMENT '汇报人',
  `ReadDate` bigint(20) DEFAULT NULL COMMENT '阅读时间',
  `Memo` varchar(500) DEFAULT NULL COMMENT '备注',
  PRIMARY KEY (`ReportId`),
  KEY `WorkId` (`WorkId`),
  CONSTRAINT `oa_work_reporter_ibfk_1` FOREIGN KEY (`WorkId`) REFERENCES `oa_work` (`WorkId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='汇总人列表';

/*Table structure for table `schedule` */

DROP TABLE IF EXISTS `schedule`;

CREATE TABLE `schedule` (
  `JobId` bigint(20) NOT NULL AUTO_INCREMENT,
  `JobName` varchar(100) NOT NULL,
  `JobGroup` varchar(100) NOT NULL,
  `JobStatus` tinyint(4) NOT NULL,
  `TriggerType` tinyint(4) NOT NULL,
  `Cron` varchar(50) DEFAULT NULL,
  `AssemblyName` varchar(100) DEFAULT NULL,
  `ClassName` varchar(50) DEFAULT NULL,
  `Remark` varchar(500) DEFAULT NULL,
  `CreateTime` datetime NOT NULL,
  `UpdateTime` datetime DEFAULT NULL,
  `RunTimes` int(11) NOT NULL DEFAULT '0',
  `BeginTime` datetime NOT NULL,
  `EndTime` datetime DEFAULT NULL,
  `IntervalSecond` int(11) DEFAULT NULL,
  `Url` varchar(1000) DEFAULT NULL,
  `Status` tinyint(4) NOT NULL DEFAULT '1',
  PRIMARY KEY (`JobId`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4;

/*Table structure for table `sys_button` */

DROP TABLE IF EXISTS `sys_button`;

CREATE TABLE `sys_button` (
  `ButtonId` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `ButtonName` varchar(50) NOT NULL COMMENT '菜单名称',
  `Memo` varchar(200) DEFAULT NULL COMMENT '备注',
  PRIMARY KEY (`ButtonId`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;

/*Table structure for table `sys_data_privileges` */

DROP TABLE IF EXISTS `sys_data_privileges`;

CREATE TABLE `sys_data_privileges` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `UserId` bigint(20) NOT NULL COMMENT '用户ID',
  `DeptId` bigint(20) NOT NULL COMMENT '部门ID',
  `SystemId` bigint(20) NOT NULL COMMENT '系统ID',
  PRIMARY KEY (`Id`),
  KEY `UserId` (`UserId`),
  KEY `DeptId` (`DeptId`),
  KEY `SystemId` (`SystemId`),
  CONSTRAINT `sys_data_privileges_ibfk_1` FOREIGN KEY (`UserId`) REFERENCES `sys_user` (`UserId`),
  CONSTRAINT `sys_data_privileges_ibfk_2` FOREIGN KEY (`DeptId`) REFERENCES `sys_dept` (`DeptId`),
  CONSTRAINT `sys_data_privileges_ibfk_3` FOREIGN KEY (`SystemId`) REFERENCES `sys_system` (`SystemId`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8 COMMENT='数据权限';

/*Table structure for table `sys_dept` */

DROP TABLE IF EXISTS `sys_dept`;

CREATE TABLE `sys_dept` (
  `DeptId` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '部门ID',
  `DeptName` varchar(50) NOT NULL COMMENT '部门名称',
  `DeptCode` varchar(50) DEFAULT NULL COMMENT '部门编号',
  `ParentId` bigint(20) NOT NULL COMMENT '父级ID',
  `Path` varchar(200) DEFAULT NULL COMMENT '路径',
  `IsDel` tinyint(4) NOT NULL DEFAULT '0' COMMENT '是否删除',
  `Memo` varchar(50) DEFAULT NULL COMMENT '备注',
  `CreateUserId` bigint(20) NOT NULL COMMENT '创建人id',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间戳',
  PRIMARY KEY (`DeptId`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8 COMMENT='部门表（作用于全部系统）';

/*Table structure for table `sys_release_log` */

DROP TABLE IF EXISTS `sys_release_log`;

CREATE TABLE `sys_release_log` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `VersionNumber` varchar(50) NOT NULL COMMENT '版本号',
  `Memo` varchar(500) DEFAULT NULL COMMENT '描述',
  `CreateTime` bigint(20) NOT NULL COMMENT '发布时间',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=27 DEFAULT CHARSET=utf8;

/*Table structure for table `sys_resource` */

DROP TABLE IF EXISTS `sys_resource`;

CREATE TABLE `sys_resource` (
  `ResourceId` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '资源ID',
  `SystemId` bigint(20) NOT NULL COMMENT '所属系统',
  `ResourceName` varchar(50) NOT NULL COMMENT '资源名称',
  `ParentId` bigint(11) NOT NULL DEFAULT '0' COMMENT '父级ID',
  `ResourceUrl` varchar(200) DEFAULT NULL COMMENT '资源地址',
  `Sort` int(11) NOT NULL COMMENT '同级排序',
  `ButtonClass` varchar(50) DEFAULT NULL COMMENT '按钮样式',
  `Icon` varchar(50) DEFAULT NULL COMMENT '样式图标ICON',
  `IsShow` tinyint(4) NOT NULL COMMENT '是否显示到菜单',
  `CreateUserId` bigint(11) NOT NULL COMMENT '创建人',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间戳',
  `Memo` varchar(200) DEFAULT NULL COMMENT '备注',
  `IsDel` tinyint(4) NOT NULL DEFAULT '0' COMMENT '是否删除',
  `IsButton` tinyint(4) NOT NULL DEFAULT '0' COMMENT '是否是按钮',
  `ButtonType` tinyint(4) DEFAULT NULL COMMENT '按钮类型',
  `Path` varchar(50) DEFAULT NULL COMMENT '资源路径',
  PRIMARY KEY (`ResourceId`),
  KEY `ParentId` (`ParentId`),
  KEY `SystemId` (`SystemId`),
  CONSTRAINT `sys_resource_ibfk_1` FOREIGN KEY (`SystemId`) REFERENCES `sys_system` (`SystemId`)
) ENGINE=InnoDB AUTO_INCREMENT=268 DEFAULT CHARSET=utf8 COMMENT='资源表';

/*Table structure for table `sys_role` */

DROP TABLE IF EXISTS `sys_role`;

CREATE TABLE `sys_role` (
  `RoleId` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '角色ID',
  `SystemId` bigint(20) NOT NULL COMMENT '所属系统',
  `RoleName` varchar(50) NOT NULL COMMENT '角色名称',
  `Memo` varchar(200) DEFAULT NULL COMMENT '备注',
  `IsDel` tinyint(4) NOT NULL DEFAULT '0' COMMENT '是否删除',
  `CreateUserId` bigint(20) NOT NULL COMMENT '创建人ID',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  `UpdateUserId` bigint(20) DEFAULT NULL COMMENT '修改人',
  `UpdateTime` bigint(20) DEFAULT NULL COMMENT '修改时间',
  PRIMARY KEY (`RoleId`),
  KEY `SystemId` (`SystemId`),
  CONSTRAINT `sys_role_ibfk_1` FOREIGN KEY (`SystemId`) REFERENCES `sys_system` (`SystemId`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8 COMMENT='角色表';

/*Table structure for table `sys_role_resource` */

DROP TABLE IF EXISTS `sys_role_resource`;

CREATE TABLE `sys_role_resource` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `RoleId` bigint(20) NOT NULL COMMENT '角色ID',
  `ResourceId` bigint(20) NOT NULL COMMENT '资源ID',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间戳',
  PRIMARY KEY (`Id`),
  KEY `RoleId` (`RoleId`),
  KEY `ResourceId` (`ResourceId`),
  CONSTRAINT `sys_role_resource_ibfk_1` FOREIGN KEY (`RoleId`) REFERENCES `sys_role` (`RoleId`),
  CONSTRAINT `sys_role_resource_ibfk_2` FOREIGN KEY (`ResourceId`) REFERENCES `sys_resource` (`ResourceId`)
) ENGINE=InnoDB AUTO_INCREMENT=2210 DEFAULT CHARSET=utf8 COMMENT='角色资源关联表';

/*Table structure for table `sys_system` */

DROP TABLE IF EXISTS `sys_system`;

CREATE TABLE `sys_system` (
  `SystemId` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '系统ID',
  `SystemName` varchar(50) NOT NULL COMMENT '系统名称',
  `SystemCode` varchar(50) NOT NULL COMMENT '系统编码',
  `Memo` varchar(100) DEFAULT NULL COMMENT '备注',
  `IsDel` tinyint(4) NOT NULL DEFAULT '0' COMMENT '是否删除',
  `Sort` int(11) NOT NULL DEFAULT '0' COMMENT '排序',
  `CreateUserId` bigint(20) NOT NULL COMMENT '创建人ID',
  `CreateTime` bigint(20) DEFAULT NULL COMMENT '创建时间',
  `UpdateUserId` bigint(20) DEFAULT NULL COMMENT '更新人',
  `UpdateTime` bigint(20) DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`SystemId`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8 COMMENT='系统表';

/*Table structure for table `sys_user` */

DROP TABLE IF EXISTS `sys_user`;

CREATE TABLE `sys_user` (
  `UserId` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `Account` varchar(50) DEFAULT NULL COMMENT '登录账号',
  `UserName` varchar(50) NOT NULL COMMENT '用户名',
  `JobNumber` varchar(50) DEFAULT NULL COMMENT '工号',
  `Password` varchar(50) NOT NULL COMMENT '密码',
  `HeadImg` varchar(200) DEFAULT NULL COMMENT '头像地址',
  `IsDel` tinyint(4) NOT NULL DEFAULT '0' COMMENT '是否删除 1:是，0：否',
  `CreateUserId` bigint(20) NOT NULL COMMENT '创建人ID',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  `UpdateUserId` bigint(20) DEFAULT NULL COMMENT '更新人',
  `UpdateTime` bigint(20) DEFAULT NULL COMMENT '更新时间',
  PRIMARY KEY (`UserId`),
  KEY `Account` (`Account`),
  KEY `JobNumber` (`JobNumber`)
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=utf8 COMMENT='用户表（作用于全部系统）';

/*Table structure for table `sys_user_dept` */

DROP TABLE IF EXISTS `sys_user_dept`;

CREATE TABLE `sys_user_dept` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `DeptId` bigint(20) NOT NULL COMMENT '部门ID',
  `UserId` bigint(20) NOT NULL COMMENT '用户ID',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间戳',
  PRIMARY KEY (`Id`),
  KEY `DeptId` (`DeptId`),
  KEY `UserId` (`UserId`),
  CONSTRAINT `sys_user_dept_ibfk_1` FOREIGN KEY (`DeptId`) REFERENCES `sys_dept` (`DeptId`),
  CONSTRAINT `sys_user_dept_ibfk_2` FOREIGN KEY (`UserId`) REFERENCES `sys_user` (`UserId`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

/*Table structure for table `sys_user_role` */

DROP TABLE IF EXISTS `sys_user_role`;

CREATE TABLE `sys_user_role` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `UserId` bigint(20) NOT NULL COMMENT '用户ID',
  `RoleId` bigint(20) NOT NULL COMMENT '角色ID',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间戳',
  PRIMARY KEY (`Id`),
  KEY `UserId` (`UserId`),
  KEY `RoleId` (`RoleId`),
  CONSTRAINT `sys_user_role_ibfk_1` FOREIGN KEY (`UserId`) REFERENCES `sys_user` (`UserId`),
  CONSTRAINT `sys_user_role_ibfk_2` FOREIGN KEY (`RoleId`) REFERENCES `sys_role` (`RoleId`)
) ENGINE=InnoDB AUTO_INCREMENT=61 DEFAULT CHARSET=utf8 COMMENT='用户角色关联表';

/*Table structure for table `wf_workflow` */

DROP TABLE IF EXISTS `wf_workflow`;

CREATE TABLE `wf_workflow` (
  `FlowId` char(36) NOT NULL COMMENT '主键',
  `CategoryId` char(36) NOT NULL COMMENT '分类ID',
  `FormId` char(36) NOT NULL COMMENT '表单ID',
  `FlowCode` varchar(50) NOT NULL COMMENT '编码',
  `FlowName` varchar(50) NOT NULL COMMENT '流程名称',
  `FlowContent` text COMMENT '流程JSON',
  `FlowVersion` int(11) NOT NULL DEFAULT '0' COMMENT '流程版本',
  `Memo` varchar(50) DEFAULT NULL COMMENT '备注',
  `Enable` int(11) NOT NULL DEFAULT '1' COMMENT '是否启用',
  `CreateUserId` varchar(50) NOT NULL COMMENT '创建人',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`FlowId`),
  KEY `CategoryId` (`CategoryId`),
  KEY `FormId` (`FormId`),
  CONSTRAINT `wf_workflow_ibfk_1` FOREIGN KEY (`CategoryId`) REFERENCES `wf_workflow_category` (`Id`),
  CONSTRAINT `wf_workflow_ibfk_2` FOREIGN KEY (`FormId`) REFERENCES `wf_workflow_form` (`FormId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='工作流表';

/*Table structure for table `wf_workflow_category` */

DROP TABLE IF EXISTS `wf_workflow_category`;

CREATE TABLE `wf_workflow_category` (
  `Id` char(36) NOT NULL COMMENT '主键',
  `Name` varchar(50) NOT NULL COMMENT '分类名称',
  `ParentId` char(36) DEFAULT NULL COMMENT '父级ID',
  `Memo` varchar(100) DEFAULT NULL COMMENT '备注',
  `Status` int(11) DEFAULT '1' COMMENT '状态',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  `CreateUserId` varchar(50) DEFAULT NULL COMMENT '创建人',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='流程分类';

/*Table structure for table `wf_workflow_form` */

DROP TABLE IF EXISTS `wf_workflow_form`;

CREATE TABLE `wf_workflow_form` (
  `FormId` char(36) NOT NULL COMMENT '表单ID',
  `FormName` varchar(50) NOT NULL COMMENT '表单名称',
  `FormType` int(11) DEFAULT NULL COMMENT '表单类型',
  `Content` text COMMENT '表单内容',
  `OriginalContent` text COMMENT '原始表单内容',
  `FormUrl` varchar(200) DEFAULT NULL COMMENT '表单地址',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  `CreateUserId` varchar(50) NOT NULL COMMENT '创建人',
  PRIMARY KEY (`FormId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='流程表单';

/*Table structure for table `wf_workflow_instance` */

DROP TABLE IF EXISTS `wf_workflow_instance`;

CREATE TABLE `wf_workflow_instance` (
  `InstanceId` char(36) NOT NULL COMMENT '实例ID',
  `FlowId` char(36) DEFAULT NULL COMMENT '流程ID',
  `Code` varchar(50) DEFAULT NULL COMMENT '实例编号',
  `ActivityId` char(36) DEFAULT NULL COMMENT '当前节点ID',
  `ActivityType` int(11) DEFAULT NULL COMMENT '当前节点类型',
  `ActivityName` varchar(50) DEFAULT NULL COMMENT '当前节点名称',
  `PreviousId` char(36) DEFAULT NULL COMMENT '上一个节点ID',
  `MakerList` varchar(200) DEFAULT NULL COMMENT '执行人',
  `FlowContent` text COMMENT '流程JSON内容',
  `IsFinish` int(11) DEFAULT NULL COMMENT '流程是否结束',
  `Status` int(11) NOT NULL DEFAULT '0' COMMENT '用户操作状态',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  `CreateUserId` varchar(50) NOT NULL COMMENT '创建人',
  `CreateUserName` varchar(50) DEFAULT NULL COMMENT '创建人姓名',
  PRIMARY KEY (`InstanceId`),
  KEY `FlowId` (`FlowId`),
  CONSTRAINT `wf_workflow_instance_ibfk_1` FOREIGN KEY (`FlowId`) REFERENCES `wf_workflow` (`FlowId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='流程实例';

/*Table structure for table `wf_workflow_instance_form` */

DROP TABLE IF EXISTS `wf_workflow_instance_form`;

CREATE TABLE `wf_workflow_instance_form` (
  `Id` char(36) NOT NULL COMMENT '主键',
  `InstanceId` char(36) DEFAULT NULL COMMENT '流程实例ID',
  `FormId` char(36) DEFAULT NULL COMMENT '表单ID',
  `FormContent` text COMMENT '表单内容/对于表单ID',
  `FormType` int(11) DEFAULT NULL COMMENT '表单类型',
  `FormUrl` varchar(200) DEFAULT NULL COMMENT '表单地址',
  `FormData` text COMMENT '表单数据JSON',
  `CreateUserId` varchar(50) NOT NULL COMMENT '创建人',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`Id`),
  KEY `InstanceId` (`InstanceId`),
  KEY `FormId` (`FormId`),
  CONSTRAINT `wf_workflow_instance_form_ibfk_1` FOREIGN KEY (`InstanceId`) REFERENCES `wf_workflow_instance` (`InstanceId`),
  CONSTRAINT `wf_workflow_instance_form_ibfk_2` FOREIGN KEY (`FormId`) REFERENCES `wf_workflow_form` (`FormId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='流程实例表单关联表';

/*Table structure for table `wf_workflow_line` */

DROP TABLE IF EXISTS `wf_workflow_line`;

CREATE TABLE `wf_workflow_line` (
  `Id` char(36) NOT NULL COMMENT '主键',
  `GroupId` char(36) NOT NULL COMMENT '分组Id',
  `Name` varchar(50) NOT NULL COMMENT '名称',
  `LineType` tinyint(4) DEFAULT NULL COMMENT '类型',
  `ExecuteSQL` text COMMENT '执行SQL',
  `Memo` varchar(200) DEFAULT NULL COMMENT '描述',
  `IsDel` tinyint(4) NOT NULL COMMENT '是否删除',
  `CreateTime` bigint(20) DEFAULT NULL COMMENT '创建时间',
  `CreateUserId` varchar(50) DEFAULT NULL COMMENT '创建人',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='工作流line通用条件值';

/*Table structure for table `wf_workflow_operation_history` */

DROP TABLE IF EXISTS `wf_workflow_operation_history`;

CREATE TABLE `wf_workflow_operation_history` (
  `OperationId` char(36) NOT NULL COMMENT '主键',
  `InstanceId` char(36) DEFAULT NULL COMMENT '实例进程ID',
  `NodeId` char(36) DEFAULT NULL COMMENT '节点ID',
  `NodeName` varchar(200) DEFAULT NULL COMMENT '节点名称',
  `TransitionType` int(11) DEFAULT NULL COMMENT '流转类型',
  `Content` varchar(500) DEFAULT NULL COMMENT '操作内容',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  `CreateUserId` varchar(50) NOT NULL COMMENT '创建人',
  `CreateUserName` varchar(50) DEFAULT NULL COMMENT '创建人姓名',
  PRIMARY KEY (`OperationId`),
  KEY `InstanceId` (`InstanceId`),
  CONSTRAINT `wf_workflow_operation_history_ibfk_1` FOREIGN KEY (`InstanceId`) REFERENCES `wf_workflow_instance` (`InstanceId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='流程操作历史记录';

/*Table structure for table `wf_workflow_transition_history` */

DROP TABLE IF EXISTS `wf_workflow_transition_history`;

CREATE TABLE `wf_workflow_transition_history` (
  `TransitionId` char(36) NOT NULL COMMENT '主键',
  `InstanceId` char(36) DEFAULT NULL COMMENT '流程实例ID',
  `FromNodeId` char(36) DEFAULT NULL COMMENT '开始节点Id',
  `FromNodeType` int(11) DEFAULT NULL COMMENT '开始节点类型',
  `FromNodName` varchar(50) DEFAULT NULL COMMENT '开始节点名称',
  `ToNodeId` char(36) DEFAULT NULL COMMENT '结束节点Id',
  `ToNodeType` int(11) DEFAULT NULL COMMENT '结束节点类型',
  `ToNodeName` varchar(50) DEFAULT NULL COMMENT '结束节点名称',
  `TransitionState` int(11) DEFAULT NULL COMMENT '转化状态',
  `IsFinish` int(11) DEFAULT NULL COMMENT '是否结束',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  `CreateUserId` varchar(50) NOT NULL COMMENT '创建人',
  `CreateUserName` varchar(50) DEFAULT NULL COMMENT '创建人姓名',
  PRIMARY KEY (`TransitionId`),
  KEY `InstanceId` (`InstanceId`),
  CONSTRAINT `wf_workflow_transition_history_ibfk_1` FOREIGN KEY (`InstanceId`) REFERENCES `wf_workflow_instance` (`InstanceId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='流程流转历史表';

/*Table structure for table `wx_account` */

DROP TABLE IF EXISTS `wx_account`;

CREATE TABLE `wx_account` (
  `WeixinId` varchar(50) NOT NULL COMMENT '微信原始Id',
  `AppId` varchar(50) NOT NULL COMMENT '开发者ID',
  `AppSecret` varchar(200) NOT NULL COMMENT '开发者密码',
  `WeixinName` varchar(50) NOT NULL COMMENT '公众号名称',
  `WeixinQRCode` varchar(500) DEFAULT NULL COMMENT '公众号二维码地址',
  `AccessToken` varchar(1000) DEFAULT NULL COMMENT 'AccessToken',
  `AccessTokenCreateTime` datetime DEFAULT NULL COMMENT 'AccessToken创建时间',
  `JsApiTicket` varchar(100) DEFAULT NULL COMMENT 'JS API临时票据',
  `JsApiTicketCreateTime` datetime DEFAULT NULL COMMENT 'JS API临时票据创建时间',
  `SubscribePageUrl` varchar(500) DEFAULT NULL COMMENT '微信号关注引导页地址URL',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  `Token` varchar(50) NOT NULL COMMENT '令牌',
  `EncodingAESKey` varchar(200) DEFAULT NULL COMMENT '消息加解密密钥',
  PRIMARY KEY (`WeixinId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='公众号表';

/*Table structure for table `wx_account_miniprogram` */

DROP TABLE IF EXISTS `wx_account_miniprogram`;

CREATE TABLE `wx_account_miniprogram` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `WeixinId` varchar(50) NOT NULL COMMENT '微信原始Id',
  `AppId` varchar(50) NOT NULL COMMENT 'AppID(小程序ID)',
  `AppSecret` varchar(200) NOT NULL COMMENT 'AppSecret(小程序密钥)',
  PRIMARY KEY (`Id`),
  KEY `WeixinId` (`WeixinId`),
  CONSTRAINT `wx_account_miniprogram_ibfk_1` FOREIGN KEY (`WeixinId`) REFERENCES `wx_account` (`WeixinId`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

/*Table structure for table `wx_keyword` */

DROP TABLE IF EXISTS `wx_keyword`;

CREATE TABLE `wx_keyword` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `RuleId` int(11) NOT NULL COMMENT '规则表ID',
  `Keyword` varchar(50) NOT NULL COMMENT '关键字',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`Id`),
  KEY `RuleId` (`RuleId`),
  CONSTRAINT `wx_keyword_ibfk_1` FOREIGN KEY (`RuleId`) REFERENCES `wx_rule` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='关键字表';

/*Table structure for table `wx_menu` */

DROP TABLE IF EXISTS `wx_menu`;

CREATE TABLE `wx_menu` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `ParentId` int(11) NOT NULL DEFAULT '0' COMMENT '父级ID',
  `Name` varchar(60) NOT NULL COMMENT '菜单标题，不超过16个字节，子菜单不超过60个字节',
  `Type` varchar(50) DEFAULT NULL COMMENT '菜单的响应动作类型，view表示网页类型，click表示点击类型，miniprogram表示小程序类型',
  `Key` varchar(128) DEFAULT NULL COMMENT '菜单KEY值，用于消息接口推送，不超过128字节',
  `Url` varchar(1000) DEFAULT NULL COMMENT '网页 链接，用户点击菜单可打开链接，不超过1024字节。 type为miniprogram时，不支持小程序的老版本客户端将打开本url。',
  `AppId` varchar(50) DEFAULT NULL COMMENT '小程序的appid（仅认证公众号可配置）',
  `PagePath` varchar(200) DEFAULT NULL COMMENT '小程序的页面路径',
  `Sort` int(11) NOT NULL DEFAULT '0',
  `IsDel` int(11) NOT NULL DEFAULT '0',
  `CreateTime` bigint(20) DEFAULT NULL COMMENT '创建时间',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='微信菜单';

/*Table structure for table `wx_miniprogram_user` */

DROP TABLE IF EXISTS `wx_miniprogram_user`;

CREATE TABLE `wx_miniprogram_user` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '自增主键',
  `OpenId` varchar(50) NOT NULL COMMENT '小程序对应该用户的OpenId',
  `UnionId` varchar(200) DEFAULT NULL COMMENT '微信开放平台的唯一标识符',
  `NickName` varchar(50) DEFAULT NULL COMMENT '昵称',
  `City` varchar(50) DEFAULT NULL COMMENT '用户所在城市',
  `Country` varchar(50) DEFAULT NULL COMMENT '用户所在国家',
  `Province` varchar(50) DEFAULT NULL COMMENT '用户所在的省份',
  `Language` varchar(20) DEFAULT NULL COMMENT '语言',
  `Gender` tinyint(4) DEFAULT NULL COMMENT '性别',
  `AvatarUrl` varchar(1000) DEFAULT NULL COMMENT '头像',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`OpenId`,`Id`),
  KEY `Id` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COMMENT='小程序用户表';

/*Table structure for table `wx_news_response` */

DROP TABLE IF EXISTS `wx_news_response`;

CREATE TABLE `wx_news_response` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `RuleId` int(11) NOT NULL COMMENT '规则ID',
  `Title` varchar(50) DEFAULT NULL COMMENT '图文消息标题',
  `Description` varchar(200) DEFAULT NULL COMMENT '图文消息描述',
  `PicUrl` varchar(500) DEFAULT NULL COMMENT '图片链接，支持JPG、PNG格式，较好的效果为大图360*200，小图200*200',
  `Url` varchar(500) DEFAULT NULL COMMENT '点击图文消息跳转链接',
  `Sort` int(11) NOT NULL DEFAULT '0' COMMENT '排序',
  `IsDel` tinyint(1) NOT NULL DEFAULT '0' COMMENT '是否已删除0否，1是',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`Id`),
  KEY `RuleId` (`RuleId`),
  CONSTRAINT `wx_news_response_ibfk_1` FOREIGN KEY (`RuleId`) REFERENCES `wx_rule` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='回复图文消息';

/*Table structure for table `wx_rule` */

DROP TABLE IF EXISTS `wx_rule`;

CREATE TABLE `wx_rule` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `RuleName` varchar(50) NOT NULL COMMENT '规则名称',
  `RuleType` tinyint(4) NOT NULL COMMENT '规则类型0:普通，1:未匹配到回复规则',
  `RequestMsgType` int(11) NOT NULL COMMENT '规则类型',
  `ResponseMsgType` int(11) NOT NULL COMMENT '响应消息类型',
  `CreateTime` bigint(20) NOT NULL COMMENT '规则创建时间',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COMMENT='响应微信消息规则';

/*Table structure for table `wx_text_response` */

DROP TABLE IF EXISTS `wx_text_response`;

CREATE TABLE `wx_text_response` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `RuleId` int(11) NOT NULL COMMENT '规则ID',
  `Content` varchar(1000) DEFAULT NULL COMMENT '消息内容',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  PRIMARY KEY (`Id`),
  KEY `RuleId` (`RuleId`),
  CONSTRAINT `wx_text_response_ibfk_1` FOREIGN KEY (`RuleId`) REFERENCES `wx_rule` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COMMENT='文本回复';

/*Table structure for table `wx_user` */

DROP TABLE IF EXISTS `wx_user`;

CREATE TABLE `wx_user` (
  `OpenId` varchar(50) NOT NULL COMMENT '用户OpenId主键',
  `Subscribe` int(11) DEFAULT NULL COMMENT '用户是否关注该公众号1：关注了，0：没关注',
  `UserName` varchar(100) DEFAULT NULL COMMENT '用户名',
  `NickName` varchar(100) DEFAULT NULL COMMENT '昵称',
  `Mobile` varchar(50) DEFAULT NULL COMMENT '手机号码',
  `Sex` int(11) DEFAULT NULL COMMENT '用户的性别，值为1时是男性，值为2时是女性，值为0时是未知',
  `City` varchar(50) DEFAULT NULL COMMENT '用户所在城市',
  `Country` varchar(50) DEFAULT NULL COMMENT '用户所在国家',
  `Province` varchar(50) DEFAULT NULL COMMENT '用户所在的省份',
  `Headimgurl` varchar(500) DEFAULT NULL COMMENT '用户头像',
  `Birthday` varchar(50) DEFAULT NULL COMMENT '生日',
  `CreateTime` bigint(20) NOT NULL COMMENT '创建时间',
  `IsSync` int(11) NOT NULL DEFAULT '0' COMMENT '用户基本信息是否同步过',
  PRIMARY KEY (`OpenId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='微信用户表';

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
