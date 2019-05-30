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

/*Data for the table `cap.published` */

insert  into `cap.published`(`Id`,`Version`,`Name`,`Content`,`Retries`,`Added`,`ExpiresAt`,`StatusName`) values (1133661410732945408,'v1','WorkFlowStatusChanged','{\"Id\":\"5cee4c1577800f315003a423\",\"Timestamp\":\"2019-05-29T17:08:37.398+08:00\",\"Content\":\"2019/5/29 17:08:37\",\"CallbackName\":null}',0,'2019-05-29 17:08:37','2019-05-30 17:08:38','Succeeded'),(1133662384088297472,'v1','WorkFlowStatusChanged','{\"Id\":\"5cee4cfd77800f315003a424\",\"Timestamp\":\"2019-05-29T17:12:29.452+08:00\",\"Content\":\"2019/5/29 17:12:29\",\"CallbackName\":null}',0,'2019-05-29 17:12:29','2019-05-30 17:12:29','Succeeded'),(1133662845918916608,'v1','WorkFlowStatusChanged','{\"Id\":\"5cee4d6b77800f315003a425\",\"Timestamp\":\"2019-05-29T17:14:19.561+08:00\",\"Content\":\"2019/5/29 17:14:19\",\"CallbackName\":null}',0,'2019-05-29 17:14:20','2019-05-30 17:14:20','Succeeded'),(1133662917373079552,'v1','WorkFlowStatusChanged','{\"Id\":\"5cee4d7c77800f315003a426\",\"Timestamp\":\"2019-05-29T17:14:36.597+08:00\",\"Content\":\"2019/5/29 17:14:36\",\"CallbackName\":null}',0,'2019-05-29 17:14:37','2019-05-30 17:14:37','Succeeded'),(1133662967939608576,'v1','WorkFlowStatusChanged','{\"Id\":\"5cee4d8877800f315003a427\",\"Timestamp\":\"2019-05-29T17:14:48.653+08:00\",\"Content\":\"2019/5/29 17:14:48\",\"CallbackName\":null}',0,'2019-05-29 17:14:49','2019-05-30 17:14:49','Succeeded'),(1133663552789164032,'v1','WorkFlowStatusChanged','{\"Id\":\"5cee4e1477800f315003a428\",\"Timestamp\":\"2019-05-29T17:17:08.092+08:00\",\"Content\":\"2019/5/29 17:17:08\",\"CallbackName\":null}',0,'2019-05-29 17:17:08','2019-05-30 17:17:08','Succeeded'),(1133664513905537024,'v1','WorkFlowStatusChanged','{\"Id\":\"5cee4ef977800f315003a429\",\"Timestamp\":\"2019-05-29T17:20:57.24+08:00\",\"Content\":\"2019/5/29 17:20:57\",\"CallbackName\":null}',0,'2019-05-29 17:20:57','2019-05-30 17:20:57','Succeeded'),(1133664921315061760,'v1','WorkFlowStatusChanged','{\"Id\":\"5cee4f5a77800f315003a42a\",\"Timestamp\":\"2019-05-29T17:22:34.374+08:00\",\"Content\":\"2019/5/29 17:22:34\",\"CallbackName\":null}',0,'2019-05-29 17:22:34','2019-05-30 17:22:34','Succeeded'),(1133665680786075648,'v1','WorkFlowStatusChanged','{\"Id\":\"5cee500f77800f315003a42b\",\"Timestamp\":\"2019-05-29T17:25:35.446+08:00\",\"Content\":\"2019/5/29 17:25:35\",\"CallbackName\":null}',0,'2019-05-29 17:25:35','2019-05-30 17:25:35','Succeeded'),(1133665779318665216,'v1','WorkFlowStatusChanged','{\"Id\":\"5cee502677800f315003a42c\",\"Timestamp\":\"2019-05-29T17:25:58.938+08:00\",\"Content\":\"2019/5/29 17:25:58\",\"CallbackName\":null}',0,'2019-05-29 17:25:59','2019-05-30 17:25:59','Succeeded'),(1133727355597987840,'v1','WorkFlowStatusChanged','{\"Id\":\"5cee897f77800f230c741e36\",\"Timestamp\":\"2019-05-29T21:30:39.874+08:00\",\"Content\":\"2019/5/29 21:30:39\",\"CallbackName\":null}',0,'2019-05-29 21:30:40','2019-05-30 21:30:40','Succeeded'),(1133733659729846272,'v1','WorkFlowStatusChanged','{\"Id\":\"5cee8f5e77800f14b4e3db2e\",\"Timestamp\":\"2019-05-29T21:55:42.896+08:00\",\"Content\":\"2019/5/29 21:55:42\",\"CallbackName\":null}',0,'2019-05-29 21:55:43','2019-05-30 21:55:43','Succeeded'),(1133733659729846273,'v1','WorkFlowStatusChanged','{\"Id\":\"5cee8f5e77800f14b4e3db2f\",\"Timestamp\":\"2019-05-29T21:55:42.896+08:00\",\"Content\":\"2019/5/29 21:55:42\",\"CallbackName\":null}',0,'2019-05-29 21:55:43','2019-05-30 21:55:43','Succeeded'),(1133738447838633984,'v1','WorkFlowStatusChanged','{\"Id\":\"5cee93d477800f22a4e2cbd6\",\"Timestamp\":\"2019-05-29T22:14:44.471+08:00\",\"Content\":\"2019/5/29 22:14:44\",\"CallbackName\":null}',0,'2019-05-29 22:14:44','2019-05-30 22:14:45','Succeeded'),(1133909460377141248,'v1','WorkFlowStatusChanged','{\"Id\":\"5cef331977800f29c0d8b13f\",\"Timestamp\":\"2019-05-30T09:34:17.0390006+08:00\",\"Content\":\"2019/5/30 9:34:17\",\"CallbackName\":null}',0,'2019-05-30 09:34:17','2019-05-31 09:34:17','Succeeded'),(1133911452031094784,'v1','WorkFlowStatusChanged','{\"Id\":\"5cef34f377800f0fec3050aa\",\"Timestamp\":\"2019-05-30T09:42:11.8880006+08:00\",\"Content\":\"2019/5/30 9:42:11\",\"CallbackName\":null}',0,'2019-05-30 09:42:12','2019-05-31 09:42:12','Succeeded'),(1133912509889089536,'v1','WorkFlowStatusChanged','{\"Id\":\"5cef35f077800f0fec3050ab\",\"Timestamp\":\"2019-05-30T09:46:24.0900006+08:00\",\"Content\":\"2019/5/30 9:46:24\",\"CallbackName\":null}',0,'2019-05-30 09:46:24','2019-05-31 09:46:24','Succeeded'),(1133912894326411264,'v1','WorkFlowStatusChanged','{\"Id\":\"5cef364b77800f0fec3050ac\",\"Timestamp\":\"2019-05-30T09:47:55.7470006+08:00\",\"Content\":\"2019/5/30 9:47:55\",\"CallbackName\":null}',0,'2019-05-30 09:47:56','2019-05-31 09:47:56','Succeeded'),(1133915073348435968,'v1','WorkFlowStatusChanged','{\"Id\":\"5cef385377800f2958910712\",\"Timestamp\":\"2019-05-30T09:56:35.2730006+08:00\",\"Content\":\"2019/5/30 9:56:35\",\"CallbackName\":null}',0,'2019-05-30 09:56:35','2019-05-31 09:56:35','Succeeded'),(1133915142189547520,'v1','WorkFlowStatusChanged','{\"Id\":\"5cef386377800f2958910713\",\"Timestamp\":\"2019-05-30T09:56:51.6790006+08:00\",\"Content\":\"2019/5/30 9:56:51\",\"CallbackName\":null}',0,'2019-05-30 09:56:52','2019-05-31 09:56:52','Succeeded'),(1133915258740867072,'v1','WorkFlowStatusChanged','{\"Id\":\"5cef387f77800f2958910714\",\"Timestamp\":\"2019-05-30T09:57:19.4670006+08:00\",\"Content\":\"2019/5/30 9:57:19\",\"CallbackName\":null}',0,'2019-05-30 09:57:19','2019-05-31 09:57:19','Succeeded'),(1133915985559224320,'v1','WorkFlowStatusChanged','{\"Id\":\"5cef392c77800f2958910715\",\"Timestamp\":\"2019-05-30T10:00:12.7540006+08:00\",\"Content\":\"2019/5/30 10:00:12\",\"CallbackName\":null}',0,'2019-05-30 10:00:13','2019-05-31 10:00:13','Succeeded'),(1133917950690701312,'v1','WorkFlowStatusChanged','{\"Id\":\"5cef3b0177800f2964cdc9e9\",\"Timestamp\":\"2019-05-30T10:08:01.2840006+08:00\",\"Content\":\"2019/5/30 10:08:01\",\"CallbackName\":null}',0,'2019-05-30 10:08:01','2019-05-31 10:08:01','Succeeded'),(1133918158417801216,'v1','WorkFlowStatusChanged','{\"Id\":\"5cef3b3277800f2964cdc9ea\",\"Timestamp\":\"2019-05-30T10:08:50.8040006+08:00\",\"Content\":\"2019/5/30 10:08:50\",\"CallbackName\":null}',0,'2019-05-30 10:08:51','2019-05-31 10:08:51','Succeeded'),(1133918518767235072,'v1','WorkFlowStatusChanged','{\"Id\":\"5cef3b8877800f2964cdc9eb\",\"Timestamp\":\"2019-05-30T10:10:16.7180006+08:00\",\"Content\":\"2019/5/30 10:10:16\",\"CallbackName\":null}',0,'2019-05-30 10:10:17','2019-05-31 10:10:17','Succeeded'),(1133918664783540224,'v1','WorkFlowStatusChanged','{\"Id\":\"5cef3bab77800f2964cdc9ec\",\"Timestamp\":\"2019-05-30T10:10:51.5310006+08:00\",\"Content\":\"2019/5/30 10:10:51\",\"CallbackName\":null}',0,'2019-05-30 10:10:52','2019-05-31 10:10:52','Succeeded'),(1133918773231464448,'v1','WorkFlowStatusChanged','{\"Id\":\"5cef3bc577800f2964cdc9ed\",\"Timestamp\":\"2019-05-30T10:11:17.3870006+08:00\",\"Content\":\"2019/5/30 10:11:17\",\"CallbackName\":null}',0,'2019-05-30 10:11:17','2019-05-31 10:11:17','Succeeded'),(1133918810481078272,'v1','WorkFlowStatusChanged','{\"Id\":\"5cef3bce77800f2964cdc9ee\",\"Timestamp\":\"2019-05-30T10:11:26.2680006+08:00\",\"Content\":\"2019/5/30 10:11:26\",\"CallbackName\":null}',0,'2019-05-30 10:11:26','2019-05-31 10:11:26','Succeeded'),(1133918887865987072,'v1','WorkFlowStatusChanged','{\"Id\":\"5cef3be077800f2964cdc9ef\",\"Timestamp\":\"2019-05-30T10:11:44.7180006+08:00\",\"Content\":\"2019/5/30 10:11:44\",\"CallbackName\":null}',0,'2019-05-30 10:11:45','2019-05-31 10:11:45','Succeeded'),(1133918951300640768,'v1','WorkFlowStatusChanged','{\"Id\":\"5cef3bef77800f2964cdc9f0\",\"Timestamp\":\"2019-05-30T10:11:59.8420006+08:00\",\"Content\":\"2019/5/30 10:11:59\",\"CallbackName\":null}',0,'2019-05-30 10:12:00','2019-05-31 10:12:00','Succeeded'),(1133919117449605120,'v1','WorkFlowStatusChanged','{\"Id\":\"5cef3c1777800f2964cdc9f1\",\"Timestamp\":\"2019-05-30T10:12:39.4550006+08:00\",\"Content\":\"2019/5/30 10:12:39\",\"CallbackName\":null}',0,'2019-05-30 10:12:39','2019-05-31 10:17:07','Succeeded'),(1133920273200537600,'v1','WorkFlowStatusChanged','{\"Id\":\"5cef3d2b77800f1cf81d4ab2\",\"Timestamp\":\"2019-05-30T10:17:15.0190006+08:00\",\"Content\":\"2019/5/30 10:17:15\",\"CallbackName\":null}',0,'2019-05-30 10:17:15','2019-05-31 10:17:15','Succeeded'),(1133920358890168320,'v1','WorkFlowStatusChanged','{\"Id\":\"5cef3d3f77800f1cf81d4ab3\",\"Timestamp\":\"2019-05-30T10:17:35.4370006+08:00\",\"Content\":\"2019/5/30 10:17:35\",\"CallbackName\":null}',0,'2019-05-30 10:17:35','2019-05-31 10:17:35','Succeeded'),(1133920378272047104,'v1','WorkFlowStatusChanged','{\"Id\":\"5cef3d4477800f1cf81d4ab4\",\"Timestamp\":\"2019-05-30T10:17:40.0580006+08:00\",\"Content\":\"2019/5/30 10:17:40\",\"CallbackName\":null}',0,'2019-05-30 10:17:40','2019-05-31 10:17:40','Succeeded'),(1133920452599308288,'v1','WorkFlowStatusChanged','{\"Id\":\"5cef3d5577800f1cf81d4ab5\",\"Timestamp\":\"2019-05-30T10:17:57.7790006+08:00\",\"Content\":\"2019/5/30 10:17:57\",\"CallbackName\":null}',0,'2019-05-30 10:17:58','2019-05-31 10:17:58','Succeeded'),(1133920516851851264,'v1','WorkFlowStatusChanged','{\"Id\":\"5cef3d6577800f1cf81d4ab6\",\"Timestamp\":\"2019-05-30T10:18:13.0980006+08:00\",\"Content\":\"2019/5/30 10:18:13\",\"CallbackName\":null}',0,'2019-05-30 10:18:13','2019-05-31 10:18:13','Succeeded'),(1133920547503824896,'v1','WorkFlowStatusChanged','{\"Id\":\"5cef3d6c77800f1cf81d4ab7\",\"Timestamp\":\"2019-05-30T10:18:20.4060006+08:00\",\"Content\":\"2019/5/30 10:18:20\",\"CallbackName\":null}',0,'2019-05-30 10:18:20','2019-05-31 10:18:20','Succeeded'),(1133920763896356864,'v1','WorkFlowStatusChanged','{\"Id\":\"5cef3d9f77800f1cf81d4ab8\",\"Timestamp\":\"2019-05-30T10:19:11.9980006+08:00\",\"Content\":\"2019/5/30 10:19:11\",\"CallbackName\":null}',0,'2019-05-30 10:19:12','2019-05-31 10:19:12','Succeeded');

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

/*Data for the table `cap.received` */

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
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COMMENT='员工请假';

/*Data for the table `oa_leave` */

insert  into `oa_leave`(`Id`,`LeaveCode`,`Title`,`UserId`,`AgentId`,`LeaveType`,`Reason`,`Days`,`StartTime`,`EndTime`,`CreateUserId`,`CreateTime`,`FlowStatus`,`FlowTime`) values (1,'15581490705436','测试',0,0,0,'表单保存',0,1558149070,1558149070,1,1558149070,2,1559099810),(2,'15590981348486','测试201905',0,0,0,'测试201905',0,1559098134,1559098134,1,1559098134,2,1559098469);

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

/*Data for the table `oa_message` */

insert  into `oa_message`(`Id`,`MsgType`,`FaceUserType`,`Title`,`IsLocal`,`TargetType`,`Link`,`Content`,`IsEnable`,`StartTime`,`EndTime`,`IsExecuted`,`IsDel`,`MakerUserId`,`CreateUserId`,`CreateTime`) values (1,1,0,'测试',1,'tab',NULL,'测试',1,1558945580,1558945580,1,0,0,1,1555213291),(2,0,0,'测试2',1,'blank',NULL,NULL,0,0,0,0,0,0,1,1555315893);

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

/*Data for the table `oa_message_user` */

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

/*Data for the table `oa_message_user_read` */

insert  into `oa_message_user_read`(`Id`,`MessageId`,`UserId`) values (1,1,1);

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

/*Data for the table `oa_work` */

insert  into `oa_work`(`WorkId`,`WorkType`,`Content`,`PlanContent`,`NeedHelpContent`,`Memo`,`IsDel`,`ReportDate`,`CreateUserId`,`CreateTime`) values (1,0,'123','123','123',NULL,0,0,1,0),(2,0,NULL,NULL,NULL,NULL,0,0,1,0);

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

/*Data for the table `oa_work_reporter` */

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

/*Data for the table `schedule` */

insert  into `schedule`(`JobId`,`JobName`,`JobGroup`,`JobStatus`,`TriggerType`,`Cron`,`AssemblyName`,`ClassName`,`Remark`,`CreateTime`,`UpdateTime`,`RunTimes`,`BeginTime`,`EndTime`,`IntervalSecond`,`Url`,`Status`) values (1,'微信AccessToken自动同步任务','DefaultJob',0,0,'3 * * * * ? ','MsSystem.Schedule.Job','WxAccessTokenJob',NULL,'2019-04-17 11:05:13',NULL,0,'2019-04-17 11:05:08',NULL,0,NULL,1),(2,'微信用户信息同步任务','DefaultJob',0,0,'0 0 0/1 * * ? ','MsSystem.Schedule.Job','WxUserInfoJob',NULL,'2019-04-17 11:05:13',NULL,0,'2019-04-17 11:05:08',NULL,0,NULL,1);

/*Table structure for table `sys_button` */

DROP TABLE IF EXISTS `sys_button`;

CREATE TABLE `sys_button` (
  `ButtonId` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `ButtonName` varchar(50) NOT NULL COMMENT '菜单名称',
  `Memo` varchar(200) DEFAULT NULL COMMENT '备注',
  PRIMARY KEY (`ButtonId`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;

/*Data for the table `sys_button` */

insert  into `sys_button`(`ButtonId`,`ButtonName`,`Memo`) values (1,'查看','查看'),(2,'新增','新增'),(3,'编辑','编辑'),(4,'删除','删除'),(5,'打印','打印'),(6,'审核','审核'),(7,'作废','作废'),(8,'结束','结束'),(9,'扩展','扩展');

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

/*Data for the table `sys_data_privileges` */

insert  into `sys_data_privileges`(`Id`,`UserId`,`DeptId`,`SystemId`) values (8,1,1,1),(9,1,2,1),(10,1,3,1),(11,1,4,1);

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

/*Data for the table `sys_dept` */

insert  into `sys_dept`(`DeptId`,`DeptName`,`DeptCode`,`ParentId`,`Path`,`IsDel`,`Memo`,`CreateUserId`,`CreateTime`) values (1,'MS软件','001',0,'1',0,'MS软件',1,1517812123),(2,'总经室','002',1,'1:2',0,'总经室',1,1517812220),(3,'研发部','003',1,'1:3',0,'研发部',1,1517814189),(4,'销售部','004',1,'1:4',0,'销售部',1,1517814213);

/*Table structure for table `sys_release_log` */

DROP TABLE IF EXISTS `sys_release_log`;

CREATE TABLE `sys_release_log` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '主键',
  `VersionNumber` varchar(50) NOT NULL COMMENT '版本号',
  `Memo` varchar(500) DEFAULT NULL COMMENT '描述',
  `CreateTime` bigint(20) NOT NULL COMMENT '发布时间',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=27 DEFAULT CHARSET=utf8;

/*Data for the table `sys_release_log` */

insert  into `sys_release_log`(`Id`,`VersionNumber`,`Memo`,`CreateTime`) values (1,'1','123',1498481230),(2,'2','123',1498481230),(3,'3','123',1498481230),(4,'4','123',1498481230),(5,'5','123',1498481230),(6,'6','123',1498481230),(7,'7','123',1498481230),(8,'8','123',1498481230),(9,'9','123',1498481230),(10,'10','123',1498481230),(11,'11','123',1498481230),(12,'12','123',1498481230),(13,'13','123',1498481230),(14,'14','123',1498481230),(15,'15','123',1498481230),(16,'16','123',1498481230),(17,'17','123',1498481230),(18,'18','123',1498481230),(19,'19','123',1498481230),(20,'20','123',1498481230),(21,'21','123',1498481230),(22,'22','123',1498481230),(23,'23','123',1498481230),(24,'24','123',1498481230),(25,'25','123',1498481230),(26,'26','123',1498481230);

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

/*Data for the table `sys_resource` */

insert  into `sys_resource`(`ResourceId`,`SystemId`,`ResourceName`,`ParentId`,`ResourceUrl`,`Sort`,`ButtonClass`,`Icon`,`IsShow`,`CreateUserId`,`CreateTime`,`Memo`,`IsDel`,`IsButton`,`ButtonType`,`Path`) values (1,1,'系统管理',0,'/',1,NULL,'fa fa-cogs',1,1,1,'123',0,0,NULL,'1'),(2,1,'菜单管理',1,'/Sys/Resource/Index',5,NULL,'fa fa-bars',1,1,1,NULL,0,0,NULL,'1:2'),(4,1,'菜单管理编辑页',2,'/Sys/Resource/Show',0,NULL,'fa fa-balance-scale',0,1,1,NULL,0,0,NULL,'2:4'),(48,1,'查看',4,NULL,4,NULL,'fa fa-search',0,0,1503957966,NULL,0,1,1,NULL),(49,1,'新增',4,NULL,1,NULL,'fa fa-plus',0,0,1503957966,NULL,0,1,2,NULL),(50,1,'编辑',4,NULL,2,NULL,'fa fa-edit',0,0,1503957966,NULL,0,1,3,NULL),(51,1,'删除',4,NULL,3,NULL,'fa fa-trash',0,0,1503957966,NULL,0,1,4,NULL),(53,1,'角色管理',1,'/Sys/Role/Index',2,NULL,'fa fa-group',1,1,1503958727,'123',0,0,NULL,'1:53'),(54,1,'查看',53,NULL,4,NULL,'fa fa-search',0,0,1503958727,NULL,0,1,1,NULL),(55,1,'新增',53,NULL,1,NULL,'fa fa-plus',0,0,1503958727,NULL,0,1,2,NULL),(56,1,'编辑',53,NULL,2,NULL,'fa fa-edit',0,0,1503958727,NULL,0,1,3,NULL),(57,1,'删除',53,NULL,3,NULL,'fa fa-trash',0,0,1503958727,NULL,0,1,4,NULL),(58,1,'子系统管理',1,'/Sys/System/Index',4,NULL,'fa fa-cog',1,1,1503958798,'123',0,0,NULL,'1:58'),(59,1,'查看',58,NULL,4,NULL,'fa fa-search',0,0,1503958825,NULL,0,1,1,NULL),(60,1,'新增',58,NULL,1,NULL,'fa fa-plus',0,0,1503958825,NULL,0,1,2,NULL),(61,1,'编辑',58,NULL,2,NULL,'fa fa-edit',0,0,1503958825,NULL,0,1,3,NULL),(62,1,'删除',58,NULL,3,NULL,'fa fa-trash',0,0,1503958825,NULL,0,1,4,NULL),(64,1,'编辑',2,NULL,2,NULL,'fa fa-edit',0,0,1503958921,NULL,0,1,3,NULL),(65,1,'删除',2,NULL,3,NULL,'fa fa-trash',0,0,1503958921,NULL,0,1,4,NULL),(66,1,'角色编辑页',53,'/Sys/Role/Show',0,NULL,NULL,0,1,1503959673,NULL,0,0,NULL,'53:66'),(67,1,'查看',66,NULL,4,NULL,'fa fa-search',0,0,1503959674,NULL,0,1,1,NULL),(68,1,'新增',66,NULL,1,NULL,'fa fa-plus',0,0,1503959674,NULL,0,1,2,NULL),(69,1,'编辑',66,NULL,2,NULL,'fa fa-edit',0,0,1503959674,NULL,0,1,3,NULL),(70,1,'删除',66,NULL,3,NULL,'fa fa-trash',0,0,1503959674,NULL,0,1,4,NULL),(71,1,'系统编辑页',58,'/Sys/System/Show',0,NULL,NULL,0,1,1503959721,NULL,0,0,NULL,'58:71'),(72,1,'查看',71,NULL,4,NULL,'fa fa-search',0,0,1503959721,NULL,0,1,1,NULL),(73,1,'新增',71,NULL,1,NULL,'fa fa-plus',0,0,1503959721,NULL,0,1,2,NULL),(74,1,'编辑',71,NULL,2,NULL,'fa fa-edit',0,0,1503959721,NULL,0,1,3,NULL),(75,1,'删除',71,NULL,3,NULL,'fa fa-trash',0,0,1503959721,NULL,0,1,4,NULL),(76,1,'用户管理',1,'/Sys/User/Index',1,NULL,'fa fa-user',1,1,1503959782,NULL,0,0,NULL,'1:76'),(77,1,'查看',76,NULL,4,NULL,'fa fa-search',0,0,1503959782,NULL,0,1,1,NULL),(78,1,'新增',76,NULL,1,NULL,'fa fa-plus',0,0,1503959782,NULL,0,1,2,NULL),(79,1,'编辑',76,NULL,2,NULL,'fa fa-edit',0,0,1503959782,NULL,0,1,3,NULL),(80,1,'删除',76,NULL,3,NULL,'fa fa-trash',0,0,1503959782,NULL,0,1,4,NULL),(81,1,'用户编辑页',76,'/Sys/User/Show',0,NULL,NULL,0,1,1503959818,NULL,0,0,NULL,'76:81'),(82,1,'查看',81,NULL,4,NULL,'fa fa-search',0,0,1503959818,NULL,0,1,1,NULL),(83,1,'新增',81,NULL,1,NULL,'fa fa-plus',0,0,1503959818,NULL,0,1,2,NULL),(84,1,'编辑',81,NULL,2,NULL,'fa fa-edit',0,0,1503959818,NULL,0,1,3,NULL),(85,1,'删除',81,NULL,3,NULL,'fa fa-trash',0,0,1503959818,NULL,0,1,4,NULL),(86,1,'查看',2,NULL,4,NULL,'fa fa-search',0,0,1503990906,NULL,0,1,1,NULL),(87,1,'新增',2,NULL,1,NULL,'fa fa-plus',0,0,1503990906,NULL,0,1,2,NULL),(88,2,'工作流',0,'/',3,NULL,'fa fa-sitemap',1,1,1504013557,NULL,0,0,NULL,'88'),(91,2,'流程设计',88,'/WF/WorkFlow/Index',3,NULL,'fa fa-legal',1,1,1504439709,'流程设计列表',0,0,NULL,'88:91'),(92,2,'查看',91,NULL,4,NULL,'fa fa-search',0,0,1504439709,NULL,0,1,1,NULL),(93,2,'我的待办',88,'/WF/WorkFlowInstance/TodoList',1,NULL,'fa fa-user',1,1,1504439745,'我的待办',0,0,NULL,'88:93'),(94,2,'审批历史',88,'/WF/WorkFlowInstance/MyApprovalHistory',0,NULL,'fa fa-history',1,1,1504575850,'审批历史记录',0,0,NULL,'88:94'),(95,2,'查看',93,NULL,4,NULL,'fa fa-search',0,0,1504575862,NULL,0,1,1,NULL),(96,2,'查看',94,NULL,4,NULL,'fa fa-search',0,0,1504575866,NULL,0,1,1,NULL),(97,3,'微信管理',0,'/',2,NULL,'fa fa-weixin',1,1,1504576048,'微信管理',0,0,NULL,'97'),(98,2,'流程分类',88,'/WF/Category/Index',4,NULL,'fa fa-building-o',1,1,1508764750,'流程分类',0,0,NULL,'88:98'),(99,2,'查看',98,NULL,4,NULL,'fa fa-search',0,0,1508764750,NULL,0,1,1,NULL),(100,5,'行政办公',0,'/',2,NULL,'fa fa-book',1,1,1509884326,NULL,0,0,NULL,'100'),(142,1,'部门管理',1,'/Sys/Dept/Index',3,NULL,'fa fa-sitemap',1,1,1514899755,NULL,0,0,NULL,'1:142'),(143,1,'查看',142,NULL,4,NULL,'fa fa-search',0,0,1514899755,NULL,0,1,1,NULL),(144,1,'新增',142,NULL,1,NULL,'fa fa-plus',0,0,1514899755,NULL,0,1,2,NULL),(145,1,'编辑',142,NULL,2,NULL,'fa fa-edit',0,0,1514899755,NULL,0,1,3,NULL),(146,1,'删除',142,NULL,3,NULL,'fa fa-trash',0,0,1514899755,NULL,0,1,4,NULL),(152,1,'部门编辑页',142,'/Sys/Dept/Show',1,NULL,'',0,1,1514899820,NULL,0,0,NULL,'1:142:152'),(153,1,'查看',152,NULL,4,NULL,'fa fa-search',0,0,1514899820,NULL,0,1,1,NULL),(154,1,'新增',152,NULL,1,NULL,'fa fa-plus',0,0,1514899820,NULL,0,1,2,NULL),(155,1,'编辑',152,NULL,2,NULL,'fa fa-edit',0,0,1514899820,NULL,0,1,3,NULL),(156,1,'删除',152,NULL,3,NULL,'fa fa-trash',0,0,1514899820,NULL,0,1,4,NULL),(157,1,'数据权限',76,'/Sys/User/DataPrivileges',2,NULL,NULL,0,1,1515548644,NULL,0,0,NULL,'1:76:157'),(158,1,'查看',157,NULL,4,NULL,'fa fa-search',0,0,1515548644,NULL,0,1,1,NULL),(159,1,'新增',157,NULL,1,NULL,'fa fa-plus',0,0,1515548644,NULL,0,1,2,NULL),(160,1,'编辑',157,NULL,2,NULL,'fa fa-edit',0,0,1515548644,NULL,0,1,3,NULL),(161,1,'删除',157,NULL,3,NULL,'fa fa-trash',0,0,1515548644,NULL,0,1,4,NULL),(162,5,'员工请假',100,'/OA/Leave/Index',1,NULL,'fa fa-hand-paper-o',1,1,1519985181,NULL,0,0,NULL,'100:162'),(163,5,'查看',162,NULL,0,NULL,'fa fa-search',0,0,1519985181,NULL,0,1,1,NULL),(164,5,'新增',162,NULL,0,NULL,'fa fa-plus',0,0,1519985181,NULL,0,1,2,NULL),(165,5,'编辑',162,NULL,0,NULL,'fa fa-edit',0,0,1519985181,NULL,0,1,3,NULL),(166,5,'删除',162,NULL,0,NULL,'fa fa-trash',0,0,1519985181,NULL,0,1,4,NULL),(167,5,'员工请假编辑页',162,'/OA/Leave/Show',1,NULL,'fa fa-cc-diners-club',0,1,1520040301,NULL,0,0,NULL,'162:167'),(168,5,'查看',167,NULL,0,NULL,'fa fa-search',0,0,1520040301,NULL,0,1,1,NULL),(169,5,'新增',167,NULL,0,NULL,'fa fa-plus',0,0,1520040301,NULL,0,1,2,NULL),(170,5,'编辑',167,NULL,0,NULL,'fa fa-edit',0,0,1520040301,NULL,0,1,3,NULL),(171,5,'删除',167,NULL,0,NULL,'fa fa-trash',0,0,1520040301,NULL,0,1,4,NULL),(172,2,'新增',91,NULL,0,NULL,'fa fa-plus',0,0,1544438530,NULL,0,1,2,NULL),(173,2,'编辑',91,NULL,0,NULL,'fa fa-edit',0,0,1544438530,NULL,0,1,3,NULL),(174,2,'删除',91,NULL,0,NULL,'fa fa-trash',0,0,1544438530,NULL,0,1,4,NULL),(175,1,'调度中心',1,'/Schedule/Home/Index',6,NULL,'fa fa-joomla',1,1,1545365625,NULL,0,0,NULL,'1:175'),(176,1,'查看',175,NULL,0,NULL,'fa fa-search',0,0,1545365625,NULL,0,1,1,NULL),(177,1,'新增',175,NULL,0,NULL,'fa fa-plus',0,0,1545365625,NULL,0,1,2,NULL),(178,1,'编辑',175,NULL,0,NULL,'fa fa-edit',0,0,1545365625,NULL,0,1,3,NULL),(179,1,'删除',175,NULL,0,NULL,'fa fa-trash',0,0,1545365625,NULL,0,1,4,NULL),(180,1,'调度编辑页',175,'/Schedule/Home/Show',1,NULL,NULL,0,1,1545365682,NULL,0,0,NULL,'1:175:180'),(181,1,'查看',180,NULL,0,NULL,'fa fa-search',0,0,1545365682,NULL,0,1,1,NULL),(182,1,'新增',180,NULL,0,NULL,'fa fa-plus',0,0,1545365754,NULL,0,1,2,NULL),(183,1,'编辑',180,NULL,0,NULL,'fa fa-edit',0,0,1545365754,NULL,0,1,3,NULL),(184,1,'删除',180,NULL,0,NULL,'fa fa-trash',0,0,1545365754,NULL,0,1,4,NULL),(185,1,'日志列表',1,'/Sys/Log/Index',7,NULL,'fa fa-list-alt',1,1,1545902555,NULL,0,0,NULL,'1:185'),(186,1,'查看',185,NULL,0,NULL,'fa fa-search',0,0,1545902555,NULL,0,1,1,NULL),(187,3,'规则管理',97,'/Weixin/Rule/Index',1,NULL,'fa fa-hand-lizard-o',1,1,1547040097,NULL,0,0,NULL,'97:187'),(188,3,'查看',187,NULL,0,NULL,'fa fa-search',0,0,1547040097,NULL,0,1,1,NULL),(189,3,'新增',187,NULL,0,NULL,'fa fa-plus',0,0,1547040097,NULL,0,1,2,NULL),(190,3,'编辑',187,NULL,0,NULL,'fa fa-edit',0,0,1547040097,NULL,0,1,3,NULL),(191,3,'删除',187,NULL,0,NULL,'fa fa-trash',0,0,1547040097,NULL,0,1,4,NULL),(192,3,'规则编辑页',187,'/Weixin/Rule/Show',1,NULL,NULL,0,1,1547040143,NULL,0,0,NULL,'97:187:192'),(193,3,'查看',192,NULL,0,NULL,'fa fa-search',0,0,1547040143,NULL,0,1,1,NULL),(194,3,'新增',192,NULL,0,NULL,'fa fa-plus',0,0,1547040143,NULL,0,1,2,NULL),(195,3,'编辑',192,NULL,0,NULL,'fa fa-edit',0,0,1547040143,NULL,0,1,3,NULL),(196,3,'删除',192,NULL,0,NULL,'fa fa-trash',0,0,1547040143,NULL,0,1,4,NULL),(197,3,'自定义菜单',97,'/Weixin/Menu/Index',2,NULL,'fa fa-bars',1,1,1547040193,NULL,0,0,NULL,'97:197'),(198,3,'查看',197,NULL,0,NULL,'fa fa-search',0,0,1547040193,NULL,0,1,1,NULL),(199,3,'新增',197,NULL,0,NULL,'fa fa-plus',0,0,1547040193,NULL,0,1,2,NULL),(200,3,'编辑',197,NULL,0,NULL,'fa fa-edit',0,0,1547040193,NULL,0,1,3,NULL),(201,3,'删除',197,NULL,0,NULL,'fa fa-trash',0,0,1547040193,NULL,0,1,4,NULL),(202,3,'自定义菜单编辑页',197,'/Weixin/Menu/Show',1,NULL,NULL,0,1,1547040223,NULL,0,0,NULL,'97:197:202'),(203,3,'查看',202,NULL,0,NULL,'fa fa-search',0,0,1547040223,NULL,0,1,1,NULL),(204,3,'新增',202,NULL,0,NULL,'fa fa-plus',0,0,1547040223,NULL,0,1,2,NULL),(205,3,'编辑',202,NULL,0,NULL,'fa fa-edit',0,0,1547040223,NULL,0,1,3,NULL),(206,3,'删除',202,NULL,0,NULL,'fa fa-trash',0,0,1547040223,NULL,0,1,4,NULL),(223,2,'新增',98,NULL,0,'fa fa-plus',NULL,0,0,1556070241,NULL,0,1,2,NULL),(224,2,'编辑',98,NULL,0,'fa fa-edit',NULL,0,0,1556070241,NULL,0,1,3,NULL),(225,2,'删除',98,NULL,0,'fa fa-trash',NULL,0,0,1556070241,NULL,0,1,4,NULL),(226,2,'我的流程',88,'/WF/WorkFlowInstance/MyFlow',2,NULL,'fa fa-user-plus',1,1,1556096263,NULL,0,0,NULL,'88:226'),(227,2,'查看',226,NULL,0,'fa fa-search',NULL,0,0,1556096263,NULL,0,1,1,NULL),(228,2,'新增',226,NULL,0,'fa fa-plus',NULL,0,0,1556096263,NULL,0,1,2,NULL),(229,2,'编辑',226,NULL,0,'fa fa-edit',NULL,0,0,1556096263,NULL,0,1,3,NULL),(230,2,'删除',226,NULL,0,'fa fa-trash',NULL,0,0,1556096263,NULL,0,1,4,NULL),(231,2,'流程发起',88,'/WF/WorkFlowInstance/Start',5,NULL,'fa fa-location-arrow',1,1,1556096629,NULL,0,0,NULL,'88:231'),(232,2,'查看',231,NULL,0,'fa fa-search',NULL,0,0,1556096629,NULL,0,1,1,NULL),(236,2,'表单设计',88,'/WF/Form/Index',6,NULL,'fa fa-contao',1,1,1556097850,'表单设计',0,0,NULL,'88:236'),(237,2,'查看',236,NULL,0,'fa fa-search',NULL,0,0,1556097850,NULL,0,1,1,NULL),(238,2,'新增',236,NULL,0,'fa fa-plus',NULL,0,0,1556097850,NULL,0,1,2,NULL),(239,2,'编辑',236,NULL,0,'fa fa-edit',NULL,0,0,1556097850,NULL,0,1,3,NULL),(240,2,'删除',236,NULL,0,'fa fa-trash',NULL,0,0,1556097850,NULL,0,1,4,NULL),(241,2,'表单设计编辑页',236,'/WF/Form/Show',1,NULL,NULL,0,1,1556098005,NULL,0,0,NULL,'88:236:241'),(242,2,'查看',241,NULL,0,'fa fa-search',NULL,0,0,1556098005,NULL,0,1,1,NULL),(243,2,'新增',241,NULL,0,'fa fa-plus',NULL,0,0,1556098005,NULL,0,1,2,NULL),(244,2,'编辑',241,NULL,0,'fa fa-edit',NULL,0,0,1556098005,NULL,0,1,3,NULL),(245,2,'删除',241,NULL,0,'fa fa-trash',NULL,0,0,1556098005,NULL,0,1,4,NULL),(246,2,'流程设计编辑页',91,'/WF/WorkFlow/Show',1,NULL,NULL,0,1,1557981722,'流程设计编辑页',0,0,NULL,'88:91:246'),(247,2,'查看',246,NULL,0,'fa fa-search',NULL,0,0,1557981722,NULL,0,1,1,NULL),(248,2,'新增',246,NULL,0,'fa fa-plus',NULL,0,0,1557981722,NULL,0,1,2,NULL),(249,2,'编辑',246,NULL,0,'fa fa-edit',NULL,0,0,1557981722,NULL,0,1,3,NULL),(250,2,'删除',246,NULL,0,'fa fa-trash',NULL,0,0,1557981722,NULL,0,1,4,NULL),(253,5,'消息管理',100,'/OA/Message/Index',2,NULL,'fa fa-envelope-o',1,1,1558927920,NULL,0,0,NULL,'100:253'),(254,5,'查看',253,NULL,0,'fa fa-search',NULL,0,0,1558927920,NULL,0,1,1,NULL),(255,5,'新增',253,NULL,0,'fa fa-plus',NULL,0,0,1558927920,NULL,0,1,2,NULL),(256,5,'编辑',253,NULL,0,'fa fa-edit',NULL,0,0,1558927920,NULL,0,1,3,NULL),(257,5,'删除',253,NULL,0,'fa fa-trash',NULL,0,0,1558927920,NULL,0,1,4,NULL),(258,5,'立即发送',253,NULL,0,'fa fa-location-arrow',NULL,0,0,1558927931,NULL,0,1,9,NULL),(259,5,'消息编辑页',253,'/OA/Message/Show',1,NULL,NULL,0,1,1558927982,NULL,0,0,NULL,'100:253:259'),(260,5,'查看',259,NULL,0,'fa fa-search',NULL,0,0,1558927982,NULL,0,1,1,NULL),(261,5,'新增',259,NULL,0,'fa fa-plus',NULL,0,0,1558927982,NULL,0,1,2,NULL),(262,5,'编辑',259,NULL,0,'fa fa-edit',NULL,0,0,1558927982,NULL,0,1,3,NULL),(263,5,'删除',259,NULL,0,'fa fa-trash',NULL,0,0,1558927982,NULL,0,1,4,NULL),(264,5,'我的消息',100,'/OA/Message/MyList',3,NULL,'fa fa-commenting-o',1,1,1558928061,NULL,0,0,NULL,'100:264'),(265,5,'查看',264,NULL,0,'fa fa-search',NULL,0,0,1558928061,NULL,0,1,1,NULL),(266,5,'消息明细',264,'/OA/Message/Detail',1,NULL,NULL,0,1,1558946268,NULL,0,0,NULL,'100:264:266'),(267,5,'查看',266,NULL,0,'fa fa-search',NULL,0,0,1558946268,NULL,0,1,1,NULL);

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

/*Data for the table `sys_role` */

insert  into `sys_role`(`RoleId`,`SystemId`,`RoleName`,`Memo`,`IsDel`,`CreateUserId`,`CreateTime`,`UpdateUserId`,`UpdateTime`) values (1,1,'系统管理员','系统管理员',0,0,1497889200,NULL,NULL),(2,1,'微信管理员','微信管理员',1,0,1497889200,0,0),(4,1,'测试角色','测试角色',1,1,1499488686,NULL,NULL),(5,2,'流程管理员','流程管理员',0,1,1499493280,NULL,NULL),(6,5,'普通用户','普通用户123',0,1,1500956464,NULL,NULL),(7,3,'微信管理员','微信管理员',0,1,1547040281,0,0);

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
) ENGINE=InnoDB AUTO_INCREMENT=2247 DEFAULT CHARSET=utf8 COMMENT='角色资源关联表';

/*Data for the table `sys_role_resource` */

insert  into `sys_role_resource`(`Id`,`RoleId`,`ResourceId`,`CreateTime`) values (988,7,97,1547040290),(989,7,187,1547040290),(990,7,188,1547040290),(991,7,189,1547040290),(992,7,190,1547040290),(993,7,191,1547040290),(994,7,192,1547040290),(995,7,193,1547040290),(996,7,194,1547040290),(997,7,195,1547040290),(998,7,196,1547040290),(999,7,197,1547040290),(1000,7,198,1547040290),(1001,7,199,1547040290),(1002,7,200,1547040290),(1003,7,201,1547040290),(1004,7,202,1547040290),(1005,7,203,1547040290),(1006,7,204,1547040290),(1007,7,205,1547040290),(1008,7,206,1547040290),(2102,1,1,1558946119),(2103,1,76,1558946119),(2104,1,81,1558946119),(2105,1,83,1558946119),(2106,1,84,1558946119),(2107,1,85,1558946119),(2108,1,82,1558946119),(2109,1,78,1558946119),(2110,1,79,1558946119),(2111,1,157,1558946119),(2112,1,159,1558946119),(2113,1,160,1558946119),(2114,1,161,1558946119),(2115,1,158,1558946119),(2116,1,80,1558946119),(2117,1,77,1558946119),(2118,1,53,1558946119),(2119,1,66,1558946119),(2120,1,68,1558946119),(2121,1,69,1558946119),(2122,1,70,1558946119),(2123,1,67,1558946119),(2124,1,55,1558946119),(2125,1,56,1558946119),(2126,1,57,1558946119),(2127,1,54,1558946119),(2128,1,142,1558946119),(2129,1,144,1558946119),(2130,1,152,1558946119),(2131,1,154,1558946119),(2132,1,155,1558946119),(2133,1,156,1558946119),(2134,1,153,1558946119),(2135,1,145,1558946119),(2136,1,146,1558946119),(2137,1,143,1558946119),(2138,1,58,1558946119),(2139,1,71,1558946119),(2140,1,73,1558946119),(2141,1,74,1558946119),(2142,1,75,1558946119),(2143,1,72,1558946119),(2144,1,60,1558946119),(2145,1,61,1558946119),(2146,1,62,1558946119),(2147,1,59,1558946119),(2148,1,2,1558946119),(2149,1,4,1558946119),(2150,1,49,1558946119),(2151,1,50,1558946119),(2152,1,51,1558946119),(2153,1,48,1558946119),(2154,1,87,1558946119),(2155,1,64,1558946119),(2156,1,65,1558946119),(2157,1,86,1558946119),(2158,1,185,1558946119),(2159,1,186,1558946119),(2184,6,100,1558946383),(2185,6,162,1558946383),(2186,6,163,1558946383),(2187,6,164,1558946383),(2188,6,165,1558946383),(2189,6,166,1558946383),(2190,6,167,1558946383),(2191,6,168,1558946383),(2192,6,169,1558946383),(2193,6,170,1558946383),(2194,6,171,1558946383),(2195,6,253,1558946383),(2196,6,254,1558946383),(2197,6,255,1558946383),(2198,6,256,1558946383),(2199,6,257,1558946383),(2200,6,258,1558946383),(2201,6,259,1558946383),(2202,6,260,1558946383),(2203,6,261,1558946383),(2204,6,262,1558946383),(2205,6,263,1558946383),(2206,6,264,1558946383),(2207,6,265,1558946383),(2208,6,266,1558946383),(2209,6,267,1558946383),(2210,5,88,1559053008),(2211,5,94,1559053008),(2212,5,96,1559053008),(2213,5,93,1559053008),(2214,5,95,1559053008),(2215,5,226,1559053008),(2216,5,227,1559053008),(2217,5,228,1559053008),(2218,5,229,1559053008),(2219,5,230,1559053008),(2220,5,91,1559053008),(2221,5,172,1559053008),(2222,5,173,1559053008),(2223,5,174,1559053008),(2224,5,246,1559053008),(2225,5,247,1559053008),(2226,5,248,1559053008),(2227,5,249,1559053008),(2228,5,250,1559053008),(2229,5,92,1559053008),(2230,5,98,1559053008),(2231,5,223,1559053008),(2232,5,224,1559053008),(2233,5,225,1559053008),(2234,5,99,1559053008),(2235,5,231,1559053008),(2236,5,232,1559053008),(2237,5,236,1559053008),(2238,5,237,1559053008),(2239,5,238,1559053008),(2240,5,239,1559053008),(2241,5,240,1559053008),(2242,5,241,1559053008),(2243,5,242,1559053008),(2244,5,243,1559053008),(2245,5,244,1559053008),(2246,5,245,1559053008);

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

/*Data for the table `sys_system` */

insert  into `sys_system`(`SystemId`,`SystemName`,`SystemCode`,`Memo`,`IsDel`,`Sort`,`CreateUserId`,`CreateTime`,`UpdateUserId`,`UpdateTime`) values (1,'系统管理','be1c63a0-63aa-11e7-a221-f97d872f551b','权限系统',0,1,1,1498481230,0,1537448282),(2,'工作流','c3cd9538-63aa-11e7-a221-f97d872f551b','工作流',0,4,1,1498481230,0,1537447644),(3,'微信系统','c7526ad9-63aa-11e7-a221-f97d872f551b','微信系统',0,3,1,1498481230,0,1537447638),(4,'系统测试','252110d2-cb56-46c8-9dc2-64c3d7e23b21','系统测试0000',1,0,1,1499494026,0,0),(5,'行政办公系统','d65fb3df-d342-41c9-ad9a-3faedbb5b0dc','行政办公系统',0,2,1,1500955747,0,1537448139),(6,'行政办公','8130e64e-912b-4ce5-aae5-89ba3b5d97a9','行政办公系统',1,0,1,1509884116,NULL,NULL);

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

/*Data for the table `sys_user` */

insert  into `sys_user`(`UserId`,`Account`,`UserName`,`JobNumber`,`Password`,`HeadImg`,`IsDel`,`CreateUserId`,`CreateTime`,`UpdateUserId`,`UpdateTime`) values (1,'wms','wms','20180101','40BD001563085FC35165329EA1FF5C5ECBDBBEEF','/uploadfile/342bd59b-edf4-48cf-aa27-d13e5a0b70df.jpeg',0,1,12,1,1542809506),(4,'wangwu','王五123','20180102','40BD001563085FC35165329EA1FF5C5ECBDBBEEF',NULL,0,0,1498571322,1,1542874254),(5,'zhangsan','张三','20180103','40BD001563085FC35165329EA1FF5C5ECBDBBEEF',NULL,0,0,1499750510,1,1538660578),(6,'lisi','李四aa','20180104','40BD001563085FC35165329EA1FF5C5ECBDBBEEF',NULL,0,0,1499750523,NULL,NULL),(7,'123','123','20180105','40BD001563085FC35165329EA1FF5C5ECBDBBEEF',NULL,0,0,1499750534,1,1557909906),(8,'321','321','20180106','40BD001563085FC35165329EA1FF5C5ECBDBBEEF',NULL,1,0,1499750544,NULL,NULL),(9,'1234','1234','20180107','40BD001563085FC35165329EA1FF5C5ECBDBBEEF',NULL,1,0,1499750555,NULL,NULL),(10,'1234','1234','20180108','40BD001563085FC35165329EA1FF5C5ECBDBBEEF',NULL,1,0,1499750555,NULL,NULL),(11,'asd','asd','20180109','40BD001563085FC35165329EA1FF5C5ECBDBBEEF',NULL,1,0,1499750583,NULL,NULL),(12,'asd','asd','20180110','40BD001563085FC35165329EA1FF5C5ECBDBBEEF',NULL,1,0,1499750584,NULL,NULL),(13,'aaa','aaa','20180111','40BD001563085FC35165329EA1FF5C5ECBDBBEEF',NULL,1,0,1499750592,NULL,NULL),(14,'aaa','aaa','20180112','40BD001563085FC35165329EA1FF5C5ECBDBBEEF',NULL,1,0,1499750592,NULL,NULL),(15,'bbb','bbb','20180113','40BD001563085FC35165329EA1FF5C5ECBDBBEEF',NULL,1,0,1501310757,NULL,NULL),(16,'ccc','ccc','20180114','40BD001563085FC35165329EA1FF5C5ECBDBBEEF',NULL,1,0,1501310765,NULL,NULL),(17,'ddd','ddd','20180115','40BD001563085FC35165329EA1FF5C5ECBDBBEEF',NULL,1,0,1501310778,NULL,NULL),(18,'eee','eee','20180116','40BD001563085FC35165329EA1FF5C5ECBDBBEEF',NULL,1,0,1501310789,NULL,NULL),(19,'asd','asd','20180117','40BD001563085FC35165329EA1FF5C5ECBDBBEEF',NULL,1,0,1509869141,NULL,NULL),(20,'123','123','2018102098','A93C168323147D1135503939396CAC628DC194C5',NULL,1,0,1539993966,NULL,NULL),(21,'cs','cs','2019041302','40BD001563085FC35165329EA1FF5C5ECBDBBEEF',NULL,1,0,1555123202,NULL,NULL);

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

/*Data for the table `sys_user_dept` */

insert  into `sys_user_dept`(`Id`,`DeptId`,`UserId`,`CreateTime`) values (2,4,4,1517825512),(3,1,1,1517875868),(4,4,6,1557303745);

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
) ENGINE=InnoDB AUTO_INCREMENT=62 DEFAULT CHARSET=utf8 COMMENT='用户角色关联表';

/*Data for the table `sys_user_role` */

insert  into `sys_user_role`(`Id`,`UserId`,`RoleId`,`CreateTime`) values (47,1,1,1547040313),(48,1,5,1547040313),(49,1,7,1547040313),(50,1,6,1547040313),(51,4,1,1557214422),(52,4,5,1557214422),(53,5,1,1557292551),(54,5,5,1557292551),(56,6,6,1557303730),(58,6,1,1557304953),(59,7,5,1557909952),(60,7,6,1557909952),(61,4,6,1559053056);

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

/*Data for the table `wf_workflow` */

insert  into `wf_workflow`(`FlowId`,`CategoryId`,`FormId`,`FlowCode`,`FlowName`,`FlowContent`,`FlowVersion`,`Memo`,`Enable`,`CreateUserId`,`CreateTime`) values ('011980a7-0ba3-4752-964e-12d88ca5c54c','9e9fc7e7-8792-40f2-97bc-8b42e583126d','3b1ceb38-e1ee-4f15-a709-d6dd3a399c77','15580575818487','员工借款','{\"title\":\"员工借款\",\"nodes\":[{\"name\":\"开始\",\"left\":107,\"top\":48,\"type\":\"start round mix\",\"id\":\"f9104625-252a-49c8-91d4-9401509fceb5\",\"width\":26,\"height\":26,\"alt\":true,\"setInfo\":{}},{\"name\":\"部门经理\",\"left\":163,\"top\":156,\"type\":\"task\",\"id\":\"f5cef31d-cb13-4195-86f3-7e2c96f345ee\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SPECIAL_USER\",\"Nodedesignatedata\":{\"users\":[\"4\"],\"roles\":[],\"orgs\":[]}}},{\"name\":\"财务总监\",\"left\":497,\"top\":269,\"type\":\"task\",\"id\":\"d8842622-f5e8-4336-b9cd-4383e5bcec3d\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SPECIAL_USER\",\"Nodedesignatedata\":{\"users\":[\"5\"],\"roles\":[],\"orgs\":[]}}},{\"name\":\"结束\",\"left\":919,\"top\":329,\"type\":\"end round\",\"id\":\"38ebf6f4-5a82-4fb6-9342-94c0f95f6820\",\"width\":26,\"height\":26,\"alt\":true,\"setInfo\":{}}],\"lines\":[{\"type\":\"tb\",\"from\":\"f9104625-252a-49c8-91d4-9401509fceb5\",\"to\":\"f5cef31d-cb13-4195-86f3-7e2c96f345ee\",\"id\":\"b279111d-eb6a-4d8a-86b6-135de84a732a\",\"name\":\"\",\"dash\":false,\"alt\":true,\"M\":110},{\"type\":\"tb\",\"from\":\"f5cef31d-cb13-4195-86f3-7e2c96f345ee\",\"to\":\"d8842622-f5e8-4336-b9cd-4383e5bcec3d\",\"id\":\"596c6d67-715e-4332-809b-7a4b8ba7fa50\",\"name\":\"\",\"dash\":false,\"alt\":true,\"M\":225.5},{\"type\":\"tb\",\"from\":\"d8842622-f5e8-4336-b9cd-4383e5bcec3d\",\"to\":\"38ebf6f4-5a82-4fb6-9342-94c0f95f6820\",\"id\":\"f3ddca8b-135b-43f6-b0bc-e42060a233af\",\"name\":\"\",\"dash\":false,\"alt\":true,\"M\":312}],\"areas\":[],\"initNum\":8}',0,'测试流程',1,'1',1558057581),('477e4199-aaf0-4e21-9eed-088922a83d58','9e9fc7e7-8792-40f2-97bc-8b42e583126d','041f7de8-dd83-4aec-a253-e181b77cc40e','15563796431067','员工请假','{\"title\":\"员工请假\",\"nodes\":[{\"name\":\"开始\",\"left\":67,\"top\":44,\"type\":\"start round mix\",\"id\":\"77825e68-4a61-43b8-9081-504088b332e6\",\"width\":26,\"height\":26,\"alt\":true,\"setInfo\":{}},{\"name\":\"节点2\",\"left\":438,\"top\":49,\"type\":\"task\",\"id\":\"5fb04da8-7113-4f80-9c91-be19db2c1a9c\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SPECIAL_USER\",\"Nodedesignatedata\":{\"users\":[\"1\"],\"roles\":[],\"orgs\":[]}},\"areaId\":\"77046665-290a-4ae1-a5cf-a69e11d41f7f\"},{\"name\":\"结束\",\"left\":809,\"top\":238,\"type\":\"end round\",\"id\":\"6dae3d55-04fc-4112-824f-e87542a03779\",\"width\":26,\"height\":26,\"alt\":true,\"setInfo\":{},\"areaId\":\"77046665-290a-4ae1-a5cf-a69e11d41f7f\"},{\"name\":\"节点4\",\"left\":427,\"top\":236,\"type\":\"task\",\"id\":\"6859955f-e3a8-4df3-a6f6-522de7a8ba42\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SPECIAL_USER\",\"Nodedesignatedata\":{\"users\":[\"6\"],\"roles\":[],\"orgs\":[]}},\"areaId\":\"77046665-290a-4ae1-a5cf-a69e11d41f7f\"},{\"name\":\"节点3\",\"left\":778,\"top\":52,\"type\":\"task\",\"id\":\"634b9765-ac0e-4272-bfab-f5b260c7fde8\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SPECIAL_USER\",\"Nodedesignatedata\":{\"users\":[\"7\"],\"roles\":[],\"orgs\":[]}},\"areaId\":\"77046665-290a-4ae1-a5cf-a69e11d41f7f\"},{\"name\":\"节点1\",\"left\":177,\"top\":41,\"type\":\"task\",\"id\":\"33e53484-5b48-4210-a62c-949dd7d6dbaa\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SPECIAL_USER\",\"Nodedesignatedata\":{\"users\":[\"4\"],\"roles\":[],\"orgs\":[]}}}],\"lines\":[{\"type\":\"sl\",\"from\":\"5fb04da8-7113-4f80-9c91-be19db2c1a9c\",\"to\":\"6859955f-e3a8-4df3-a6f6-522de7a8ba42\",\"id\":\"1bf5e7b1-35fe-4a8c-802a-8bd287c12159\",\"setInfo\":{\"lineId\":\"8228534b-7884-4888-949d-07866c557d8e\",\"lineType\":\"System\"},\"name\":\"不通过\",\"dash\":false},{\"type\":\"sl\",\"from\":\"5fb04da8-7113-4f80-9c91-be19db2c1a9c\",\"to\":\"634b9765-ac0e-4272-bfab-f5b260c7fde8\",\"id\":\"0d7696d2-c077-4464-8a14-9518fc2c536a\",\"setInfo\":{\"lineId\":\"bd8d4dc7-6fbc-4d55-af2e-34661d9037a5\",\"lineType\":\"System\"},\"name\":\"通过\",\"dash\":false},{\"type\":\"sl\",\"from\":\"634b9765-ac0e-4272-bfab-f5b260c7fde8\",\"to\":\"6dae3d55-04fc-4112-824f-e87542a03779\",\"id\":\"c764f55f-125b-48e6-8f37-8f281788d960\",\"name\":\"\",\"dash\":false},{\"type\":\"sl\",\"from\":\"77825e68-4a61-43b8-9081-504088b332e6\",\"to\":\"33e53484-5b48-4210-a62c-949dd7d6dbaa\",\"id\":\"c587ca2a-c95f-491a-b55a-a27c67df3037\",\"name\":\"\",\"dash\":false},{\"type\":\"sl\",\"from\":\"33e53484-5b48-4210-a62c-949dd7d6dbaa\",\"to\":\"5fb04da8-7113-4f80-9c91-be19db2c1a9c\",\"id\":\"5923dd84-6010-4003-bf4c-d4ee8605e945\",\"setInfo\":{\"lineId\":\"\",\"lineType\":\"System\"},\"name\":\"\",\"dash\":false},{\"type\":\"sl\",\"from\":\"6859955f-e3a8-4df3-a6f6-522de7a8ba42\",\"to\":\"6dae3d55-04fc-4112-824f-e87542a03779\",\"id\":\"a5e331b7-e66a-4461-a004-5f38040f0b38\",\"name\":\"\",\"dash\":false}],\"areas\":[],\"initNum\":18}',0,'测试流程',1,'1',1556379643);

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

/*Data for the table `wf_workflow_category` */

insert  into `wf_workflow_category`(`Id`,`Name`,`ParentId`,`Memo`,`Status`,`CreateTime`,`CreateUserId`) values ('0b286d49-162a-451b-a1d4-4ab5f2eeddb8','测试分类','00000000-0000-0000-0000-000000000000','你好啊',0,1559180780,'1'),('0e9deb2e-941e-423a-85f0-4092b0c46204','测试分类','00000000-0000-0000-0000-000000000000','你好啊你',0,1559180844,'1'),('9e9fc7e7-8792-40f2-97bc-8b42e583126d','通用流程','00000000-0000-0000-0000-000000000000','通用流程',1,1556072828,'1');

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

/*Data for the table `wf_workflow_form` */

insert  into `wf_workflow_form`(`FormId`,`FormName`,`FormType`,`Content`,`OriginalContent`,`FormUrl`,`CreateTime`,`CreateUserId`) values ('041f7de8-dd83-4aec-a253-e181b77cc40e','请假',1,NULL,NULL,'/OA/Leave/Show',1556075724,'1'),('3b1ceb38-e1ee-4f15-a709-d6dd3a399c77','员工借款',0,'\n                                    \n                                    \n                                    \n	<div class=\"form-group\">\n		<label class=\"col-sm-3 control-label\">标题：</label>\n		<div class=\"col-sm-9\">\n			<input name=\"FlowParam_1\" type=\"text\" class=\"form-control required\">\n		</div>\n	</div>\n	<div class=\"form-group\">\n		<label class=\"col-sm-3 control-label\">借款金额：</label>\n		<div class=\"col-sm-9\">\n			  <input name=\"FlowParam_2\" type=\"text\" class=\"form-control required\">\n		</div>\n	</div>\n	<div class=\"form-group\">\n		<label class=\"col-sm-3 control-label\">借款原因：</label>\n		<div class=\"col-sm-9\">\n			<textarea name=\"FlowParam_4\" class=\"form-control required\"></textarea>\n		</div>\n	</div>\n\n                                \n                                \n                                ','\n                                    \n                                    \n                                    \n	<div class=\"form-group\">\n		<label class=\"col-sm-3 control-label\">标题：</label>\n		<div class=\"col-sm-9\">\n			<input type=\"text\" class=\"form-control required\">\n		</div>\n	</div>\n	<div class=\"form-group\">\n		<label class=\"col-sm-3 control-label\">借款金额：</label>\n		<div class=\"col-sm-9\">\n			  <input type=\"text\" class=\"form-control required\">\n		</div>\n	</div>\n	<div class=\"form-group\">\n		<label class=\"col-sm-3 control-label\">借款原因：</label>\n		<div class=\"col-sm-9\">\n			<textarea class=\"form-control required\"></textarea>\n		</div>\n	</div>\n\n                                \n                                \n                                ',NULL,1558057039,'1');

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

/*Data for the table `wf_workflow_instance` */

insert  into `wf_workflow_instance`(`InstanceId`,`FlowId`,`Code`,`ActivityId`,`ActivityType`,`ActivityName`,`PreviousId`,`MakerList`,`FlowContent`,`IsFinish`,`Status`,`CreateTime`,`CreateUserId`,`CreateUserName`) values ('2484c3b5-96d1-4b03-be08-6fd035770026','011980a7-0ba3-4752-964e-12d88ca5c54c','15581505292878','d8842622-f5e8-4336-b9cd-4383e5bcec3d',2,'财务总监','f5cef31d-cb13-4195-86f3-7e2c96f345ee','5,','{\"title\":\"员工借款\",\"nodes\":[{\"name\":\"开始\",\"left\":107,\"top\":48,\"type\":\"start round mix\",\"id\":\"f9104625-252a-49c8-91d4-9401509fceb5\",\"width\":26,\"height\":26,\"alt\":true,\"setInfo\":{}},{\"name\":\"部门经理\",\"left\":163,\"top\":156,\"type\":\"task\",\"id\":\"f5cef31d-cb13-4195-86f3-7e2c96f345ee\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SPECIAL_USER\",\"Nodedesignatedata\":{\"users\":[\"4\"],\"roles\":[],\"orgs\":[]}}},{\"name\":\"财务总监\",\"left\":497,\"top\":269,\"type\":\"task\",\"id\":\"d8842622-f5e8-4336-b9cd-4383e5bcec3d\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SPECIAL_USER\",\"Nodedesignatedata\":{\"users\":[\"5\"],\"roles\":[],\"orgs\":[]}}},{\"name\":\"结束\",\"left\":919,\"top\":329,\"type\":\"end round\",\"id\":\"38ebf6f4-5a82-4fb6-9342-94c0f95f6820\",\"width\":26,\"height\":26,\"alt\":true,\"setInfo\":{}}],\"lines\":[{\"type\":\"tb\",\"from\":\"f9104625-252a-49c8-91d4-9401509fceb5\",\"to\":\"f5cef31d-cb13-4195-86f3-7e2c96f345ee\",\"id\":\"b279111d-eb6a-4d8a-86b6-135de84a732a\",\"name\":\"\",\"dash\":false,\"alt\":true,\"M\":110},{\"type\":\"tb\",\"from\":\"f5cef31d-cb13-4195-86f3-7e2c96f345ee\",\"to\":\"d8842622-f5e8-4336-b9cd-4383e5bcec3d\",\"id\":\"596c6d67-715e-4332-809b-7a4b8ba7fa50\",\"name\":\"\",\"dash\":false,\"alt\":true,\"M\":225.5},{\"type\":\"tb\",\"from\":\"d8842622-f5e8-4336-b9cd-4383e5bcec3d\",\"to\":\"38ebf6f4-5a82-4fb6-9342-94c0f95f6820\",\"id\":\"f3ddca8b-135b-43f6-b0bc-e42060a233af\",\"name\":\"\",\"dash\":false,\"alt\":true,\"M\":312}],\"areas\":[],\"initNum\":8}',0,0,1558150529,'1','wms'),('899b7f1f-0844-4358-b605-b69fedff8ff6','477e4199-aaf0-4e21-9eed-088922a83d58','15581490721567','5fb04da8-7113-4f80-9c91-be19db2c1a9c',2,'节点1','33e53484-5b48-4210-a62c-949dd7d6dbaa','','{\"title\":\"员工请假\",\"nodes\":[{\"name\":\"开始\",\"left\":67,\"top\":44,\"type\":\"start round mix\",\"id\":\"77825e68-4a61-43b8-9081-504088b332e6\",\"width\":26,\"height\":26,\"alt\":true,\"setInfo\":{}},{\"name\":\"节点2\",\"left\":438,\"top\":49,\"type\":\"task\",\"id\":\"5fb04da8-7113-4f80-9c91-be19db2c1a9c\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SPECIAL_USER\",\"Nodedesignatedata\":{\"users\":[\"1\"],\"roles\":[],\"orgs\":[]}},\"areaId\":\"77046665-290a-4ae1-a5cf-a69e11d41f7f\"},{\"name\":\"结束\",\"left\":809,\"top\":238,\"type\":\"end round\",\"id\":\"6dae3d55-04fc-4112-824f-e87542a03779\",\"width\":26,\"height\":26,\"alt\":true,\"setInfo\":{},\"areaId\":\"77046665-290a-4ae1-a5cf-a69e11d41f7f\"},{\"name\":\"节点4\",\"left\":427,\"top\":236,\"type\":\"task\",\"id\":\"6859955f-e3a8-4df3-a6f6-522de7a8ba42\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SPECIAL_USER\",\"Nodedesignatedata\":{\"users\":[\"6\"],\"roles\":[],\"orgs\":[]}},\"areaId\":\"77046665-290a-4ae1-a5cf-a69e11d41f7f\"},{\"name\":\"节点3\",\"left\":778,\"top\":52,\"type\":\"task\",\"id\":\"634b9765-ac0e-4272-bfab-f5b260c7fde8\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SPECIAL_USER\",\"Nodedesignatedata\":{\"users\":[\"7\"],\"roles\":[],\"orgs\":[]}},\"areaId\":\"77046665-290a-4ae1-a5cf-a69e11d41f7f\"},{\"name\":\"节点1\",\"left\":177,\"top\":41,\"type\":\"task\",\"id\":\"33e53484-5b48-4210-a62c-949dd7d6dbaa\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SPECIAL_USER\",\"Nodedesignatedata\":{\"users\":[\"4\"],\"roles\":[],\"orgs\":[]}}}],\"lines\":[{\"type\":\"sl\",\"from\":\"5fb04da8-7113-4f80-9c91-be19db2c1a9c\",\"to\":\"6859955f-e3a8-4df3-a6f6-522de7a8ba42\",\"id\":\"1bf5e7b1-35fe-4a8c-802a-8bd287c12159\",\"setInfo\":{\"lineId\":\"8228534b-7884-4888-949d-07866c557d8e\",\"lineType\":\"System\"},\"name\":\"不通过\",\"dash\":false},{\"type\":\"sl\",\"from\":\"5fb04da8-7113-4f80-9c91-be19db2c1a9c\",\"to\":\"634b9765-ac0e-4272-bfab-f5b260c7fde8\",\"id\":\"0d7696d2-c077-4464-8a14-9518fc2c536a\",\"setInfo\":{\"lineId\":\"bd8d4dc7-6fbc-4d55-af2e-34661d9037a5\",\"lineType\":\"System\"},\"name\":\"通过\",\"dash\":false},{\"type\":\"sl\",\"from\":\"634b9765-ac0e-4272-bfab-f5b260c7fde8\",\"to\":\"6dae3d55-04fc-4112-824f-e87542a03779\",\"id\":\"c764f55f-125b-48e6-8f37-8f281788d960\",\"name\":\"\",\"dash\":false},{\"type\":\"sl\",\"from\":\"77825e68-4a61-43b8-9081-504088b332e6\",\"to\":\"33e53484-5b48-4210-a62c-949dd7d6dbaa\",\"id\":\"c587ca2a-c95f-491a-b55a-a27c67df3037\",\"name\":\"\",\"dash\":false},{\"type\":\"sl\",\"from\":\"33e53484-5b48-4210-a62c-949dd7d6dbaa\",\"to\":\"5fb04da8-7113-4f80-9c91-be19db2c1a9c\",\"id\":\"5923dd84-6010-4003-bf4c-d4ee8605e945\",\"setInfo\":{\"lineId\":\"\",\"lineType\":\"System\"},\"name\":\"\",\"dash\":false},{\"type\":\"sl\",\"from\":\"6859955f-e3a8-4df3-a6f6-522de7a8ba42\",\"to\":\"6dae3d55-04fc-4112-824f-e87542a03779\",\"id\":\"a5e331b7-e66a-4461-a004-5f38040f0b38\",\"name\":\"\",\"dash\":false}],\"areas\":[],\"initNum\":18}',0,2,1558149072,'1','wms'),('d5a6a625-34b7-490a-894e-c6f563e1708c','477e4199-aaf0-4e21-9eed-088922a83d58','15590981447875','5fb04da8-7113-4f80-9c91-be19db2c1a9c',2,'节点1','33e53484-5b48-4210-a62c-949dd7d6dbaa','','{\"title\":\"员工请假\",\"nodes\":[{\"name\":\"开始\",\"left\":67,\"top\":44,\"type\":\"start round mix\",\"id\":\"77825e68-4a61-43b8-9081-504088b332e6\",\"width\":26,\"height\":26,\"alt\":true,\"setInfo\":{}},{\"name\":\"节点2\",\"left\":438,\"top\":49,\"type\":\"task\",\"id\":\"5fb04da8-7113-4f80-9c91-be19db2c1a9c\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SPECIAL_USER\",\"Nodedesignatedata\":{\"users\":[\"1\"],\"roles\":[],\"orgs\":[]}},\"areaId\":\"77046665-290a-4ae1-a5cf-a69e11d41f7f\"},{\"name\":\"结束\",\"left\":809,\"top\":238,\"type\":\"end round\",\"id\":\"6dae3d55-04fc-4112-824f-e87542a03779\",\"width\":26,\"height\":26,\"alt\":true,\"setInfo\":{},\"areaId\":\"77046665-290a-4ae1-a5cf-a69e11d41f7f\"},{\"name\":\"节点4\",\"left\":427,\"top\":236,\"type\":\"task\",\"id\":\"6859955f-e3a8-4df3-a6f6-522de7a8ba42\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SPECIAL_USER\",\"Nodedesignatedata\":{\"users\":[\"6\"],\"roles\":[],\"orgs\":[]}},\"areaId\":\"77046665-290a-4ae1-a5cf-a69e11d41f7f\"},{\"name\":\"节点3\",\"left\":778,\"top\":52,\"type\":\"task\",\"id\":\"634b9765-ac0e-4272-bfab-f5b260c7fde8\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SPECIAL_USER\",\"Nodedesignatedata\":{\"users\":[\"7\"],\"roles\":[],\"orgs\":[]}},\"areaId\":\"77046665-290a-4ae1-a5cf-a69e11d41f7f\"},{\"name\":\"节点1\",\"left\":177,\"top\":41,\"type\":\"task\",\"id\":\"33e53484-5b48-4210-a62c-949dd7d6dbaa\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SPECIAL_USER\",\"Nodedesignatedata\":{\"users\":[\"4\"],\"roles\":[],\"orgs\":[]}}}],\"lines\":[{\"type\":\"sl\",\"from\":\"5fb04da8-7113-4f80-9c91-be19db2c1a9c\",\"to\":\"6859955f-e3a8-4df3-a6f6-522de7a8ba42\",\"id\":\"1bf5e7b1-35fe-4a8c-802a-8bd287c12159\",\"setInfo\":{\"lineId\":\"8228534b-7884-4888-949d-07866c557d8e\",\"lineType\":\"System\"},\"name\":\"不通过\",\"dash\":false},{\"type\":\"sl\",\"from\":\"5fb04da8-7113-4f80-9c91-be19db2c1a9c\",\"to\":\"634b9765-ac0e-4272-bfab-f5b260c7fde8\",\"id\":\"0d7696d2-c077-4464-8a14-9518fc2c536a\",\"setInfo\":{\"lineId\":\"bd8d4dc7-6fbc-4d55-af2e-34661d9037a5\",\"lineType\":\"System\"},\"name\":\"通过\",\"dash\":false},{\"type\":\"sl\",\"from\":\"634b9765-ac0e-4272-bfab-f5b260c7fde8\",\"to\":\"6dae3d55-04fc-4112-824f-e87542a03779\",\"id\":\"c764f55f-125b-48e6-8f37-8f281788d960\",\"name\":\"\",\"dash\":false},{\"type\":\"sl\",\"from\":\"77825e68-4a61-43b8-9081-504088b332e6\",\"to\":\"33e53484-5b48-4210-a62c-949dd7d6dbaa\",\"id\":\"c587ca2a-c95f-491a-b55a-a27c67df3037\",\"name\":\"\",\"dash\":false},{\"type\":\"sl\",\"from\":\"33e53484-5b48-4210-a62c-949dd7d6dbaa\",\"to\":\"5fb04da8-7113-4f80-9c91-be19db2c1a9c\",\"id\":\"5923dd84-6010-4003-bf4c-d4ee8605e945\",\"setInfo\":{\"lineId\":\"\",\"lineType\":\"System\"},\"name\":\"\",\"dash\":false},{\"type\":\"sl\",\"from\":\"6859955f-e3a8-4df3-a6f6-522de7a8ba42\",\"to\":\"6dae3d55-04fc-4112-824f-e87542a03779\",\"id\":\"a5e331b7-e66a-4461-a004-5f38040f0b38\",\"name\":\"\",\"dash\":false}],\"areas\":[],\"initNum\":18}',0,2,1559098144,'1','wms'),('eb8e78f7-1a18-4ba0-bcf8-2bc42a24c85b','011980a7-0ba3-4752-964e-12d88ca5c54c','15581484864158','38ebf6f4-5a82-4fb6-9342-94c0f95f6820',4,'结束','d8842622-f5e8-4336-b9cd-4383e5bcec3d','','{\"title\":\"员工借款\",\"nodes\":[{\"name\":\"开始\",\"left\":107,\"top\":48,\"type\":\"start round mix\",\"id\":\"f9104625-252a-49c8-91d4-9401509fceb5\",\"width\":26,\"height\":26,\"alt\":true,\"setInfo\":{}},{\"name\":\"部门经理\",\"left\":163,\"top\":156,\"type\":\"task\",\"id\":\"f5cef31d-cb13-4195-86f3-7e2c96f345ee\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SPECIAL_USER\",\"Nodedesignatedata\":{\"users\":[\"4\"],\"roles\":[],\"orgs\":[]}}},{\"name\":\"财务总监\",\"left\":497,\"top\":269,\"type\":\"task\",\"id\":\"d8842622-f5e8-4336-b9cd-4383e5bcec3d\",\"width\":104,\"height\":26,\"alt\":true,\"setInfo\":{\"NodeDesignate\":\"SPECIAL_USER\",\"Nodedesignatedata\":{\"users\":[\"5\"],\"roles\":[],\"orgs\":[]}}},{\"name\":\"结束\",\"left\":919,\"top\":329,\"type\":\"end round\",\"id\":\"38ebf6f4-5a82-4fb6-9342-94c0f95f6820\",\"width\":26,\"height\":26,\"alt\":true,\"setInfo\":{}}],\"lines\":[{\"type\":\"tb\",\"from\":\"f9104625-252a-49c8-91d4-9401509fceb5\",\"to\":\"f5cef31d-cb13-4195-86f3-7e2c96f345ee\",\"id\":\"b279111d-eb6a-4d8a-86b6-135de84a732a\",\"name\":\"\",\"dash\":false,\"alt\":true,\"M\":110},{\"type\":\"tb\",\"from\":\"f5cef31d-cb13-4195-86f3-7e2c96f345ee\",\"to\":\"d8842622-f5e8-4336-b9cd-4383e5bcec3d\",\"id\":\"596c6d67-715e-4332-809b-7a4b8ba7fa50\",\"name\":\"\",\"dash\":false,\"alt\":true,\"M\":225.5},{\"type\":\"tb\",\"from\":\"d8842622-f5e8-4336-b9cd-4383e5bcec3d\",\"to\":\"38ebf6f4-5a82-4fb6-9342-94c0f95f6820\",\"id\":\"f3ddca8b-135b-43f6-b0bc-e42060a233af\",\"name\":\"\",\"dash\":false,\"alt\":true,\"M\":312}],\"areas\":[],\"initNum\":8}',1,0,1558148486,'1',NULL);

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

/*Data for the table `wf_workflow_instance_form` */

insert  into `wf_workflow_instance_form`(`Id`,`InstanceId`,`FormId`,`FormContent`,`FormType`,`FormUrl`,`FormData`,`CreateUserId`,`CreateTime`) values ('28fb6e20-dcb3-4c4f-a01c-bb317e040a72','899b7f1f-0844-4358-b605-b69fedff8ff6','041f7de8-dd83-4aec-a253-e181b77cc40e','1',1,'/OA/Leave/Show','1','1',1558149072),('648f8e72-8d56-4d2e-8f91-f33f77b1a8f7','d5a6a625-34b7-490a-894e-c6f563e1708c','041f7de8-dd83-4aec-a253-e181b77cc40e','2',1,'/OA/Leave/Show','2','1',1559098144),('75784525-377c-4fb5-ba8a-cafa8504135a','eb8e78f7-1a18-4ba0-bcf8-2bc42a24c85b','3b1ceb38-e1ee-4f15-a709-d6dd3a399c77','\n                                    \n                                    \n                                    \n	<div class=\"form-group\">\n		<label class=\"col-sm-3 control-label\">标题：</label>\n		<div class=\"col-sm-9\">\n			<input name=\"FlowParam_1\" type=\"text\" class=\"form-control required\">\n		</div>\n	</div>\n	<div class=\"form-group\">\n		<label class=\"col-sm-3 control-label\">借款金额：</label>\n		<div class=\"col-sm-9\">\n			  <input name=\"FlowParam_2\" type=\"text\" class=\"form-control required\">\n		</div>\n	</div>\n	<div class=\"form-group\">\n		<label class=\"col-sm-3 control-label\">借款原因：</label>\n		<div class=\"col-sm-9\">\n			<textarea name=\"FlowParam_4\" class=\"form-control required\"></textarea>\n		</div>\n	</div>\n\n                                \n                                \n                                ',0,NULL,'{\"FlowParam_1\":\"测试\",\"FlowParam_2\":\"123\",\"FlowParam_4\":\"123\"}','1',1558148486),('bfb541b8-af21-4285-9034-16ed4e986272','2484c3b5-96d1-4b03-be08-6fd035770026','3b1ceb38-e1ee-4f15-a709-d6dd3a399c77','\n                                    \n                                    \n                                    \n	<div class=\"form-group\">\n		<label class=\"col-sm-3 control-label\">标题：</label>\n		<div class=\"col-sm-9\">\n			<input name=\"FlowParam_1\" type=\"text\" class=\"form-control required\">\n		</div>\n	</div>\n	<div class=\"form-group\">\n		<label class=\"col-sm-3 control-label\">借款金额：</label>\n		<div class=\"col-sm-9\">\n			  <input name=\"FlowParam_2\" type=\"text\" class=\"form-control required\">\n		</div>\n	</div>\n	<div class=\"form-group\">\n		<label class=\"col-sm-3 control-label\">借款原因：</label>\n		<div class=\"col-sm-9\">\n			<textarea name=\"FlowParam_4\" class=\"form-control required\"></textarea>\n		</div>\n	</div>\n\n                                \n                                \n                                ',0,NULL,'{\"FlowParam_1\":\"121\",\"FlowParam_2\":\"212\",\"FlowParam_4\":\"122\"}','1',1558150529);

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

/*Data for the table `wf_workflow_line` */

insert  into `wf_workflow_line`(`Id`,`GroupId`,`Name`,`LineType`,`ExecuteSQL`,`Memo`,`IsDel`,`CreateTime`,`CreateUserId`) values ('8228534b-7884-4888-949d-07866c557d8e','051c6aea-ffa5-41b7-8f6f-dc8d25dfd801','不通过',0,'0',NULL,0,1556348848,'1'),('bd8d4dc7-6fbc-4d55-af2e-34661d9037a5','051c6aea-ffa5-41b7-8f6f-dc8d25dfd801','通过',0,'1',NULL,0,1556348848,'1');

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

/*Data for the table `wf_workflow_operation_history` */

insert  into `wf_workflow_operation_history`(`OperationId`,`InstanceId`,`NodeId`,`NodeName`,`TransitionType`,`Content`,`CreateTime`,`CreateUserId`,`CreateUserName`) values ('4cc4acaf-8309-4fc2-8042-d8230708b93f','899b7f1f-0844-4358-b605-b69fedff8ff6','77825e68-4a61-43b8-9081-504088b332e6','开始',1,'提交流程',1558149072,'1','wms'),('7db810e4-83e9-48a4-93f2-324f29fbbb60','eb8e78f7-1a18-4ba0-bcf8-2bc42a24c85b','f5cef31d-cb13-4195-86f3-7e2c96f345ee','部门经理',2,'同意！',1558148564,'4','王五123'),('93c2fdf3-8227-423b-a3ae-967e7b169579','eb8e78f7-1a18-4ba0-bcf8-2bc42a24c85b','d8842622-f5e8-4336-b9cd-4383e5bcec3d','财务总监',2,'同意！',1558148898,'5','张三'),('afcec041-8f1b-497d-91a7-926206fcc45b','2484c3b5-96d1-4b03-be08-6fd035770026','f5cef31d-cb13-4195-86f3-7e2c96f345ee','部门经理',2,'同意！',1559026034,'4','王五123'),('c3db7db3-a7a0-4a84-8fac-fc12c19df7f2','d5a6a625-34b7-490a-894e-c6f563e1708c','33e53484-5b48-4210-a62c-949dd7d6dbaa','节点1',3,'任性',1559098469,'4','王五123'),('cc702069-4db8-4aa3-8d6b-d52ab66a1b81','899b7f1f-0844-4358-b605-b69fedff8ff6','33e53484-5b48-4210-a62c-949dd7d6dbaa','节点1',3,'任性',1559099810,'4','王五123'),('d8ce190d-d3fa-4555-821e-e6c717c2cce3','d5a6a625-34b7-490a-894e-c6f563e1708c','77825e68-4a61-43b8-9081-504088b332e6','开始',1,'提交流程',1559098144,'1','wms'),('de116d39-979d-46ea-9687-7a1f96341170','2484c3b5-96d1-4b03-be08-6fd035770026','f9104625-252a-49c8-91d4-9401509fceb5','开始',1,'提交流程',1559025956,'1','wms'),('fad0350f-b529-41db-bc90-659390d7e82e','eb8e78f7-1a18-4ba0-bcf8-2bc42a24c85b','f9104625-252a-49c8-91d4-9401509fceb5','开始',1,'提交流程',1558148487,'1','wms');

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

/*Data for the table `wf_workflow_transition_history` */

insert  into `wf_workflow_transition_history`(`TransitionId`,`InstanceId`,`FromNodeId`,`FromNodeType`,`FromNodName`,`ToNodeId`,`ToNodeType`,`ToNodeName`,`TransitionState`,`IsFinish`,`CreateTime`,`CreateUserId`,`CreateUserName`) values ('33cffdae-5757-4a60-8b8e-5897b8278b6f','eb8e78f7-1a18-4ba0-bcf8-2bc42a24c85b','f5cef31d-cb13-4195-86f3-7e2c96f345ee',2,'部门经理','d8842622-f5e8-4336-b9cd-4383e5bcec3d',2,'财务总监',0,0,1558148564,'4','王五123'),('85163dd2-b37a-4088-92b7-f8a7a14ed474','2484c3b5-96d1-4b03-be08-6fd035770026','f5cef31d-cb13-4195-86f3-7e2c96f345ee',2,'部门经理','d8842622-f5e8-4336-b9cd-4383e5bcec3d',2,'财务总监',0,0,1559026034,'4','王五123'),('9c7aaeed-091f-4a95-9dbd-b79d26e55466','2484c3b5-96d1-4b03-be08-6fd035770026','f9104625-252a-49c8-91d4-9401509fceb5',3,'开始','f5cef31d-cb13-4195-86f3-7e2c96f345ee',2,'部门经理',0,0,1559025956,'1','wms'),('afb393f9-fc99-4b2c-8972-cf6bbf4ce126','eb8e78f7-1a18-4ba0-bcf8-2bc42a24c85b','d8842622-f5e8-4336-b9cd-4383e5bcec3d',2,'财务总监','38ebf6f4-5a82-4fb6-9342-94c0f95f6820',4,'结束',0,1,1558148898,'5','张三'),('bcb64644-be1e-47bc-807b-fe58803c08bb','eb8e78f7-1a18-4ba0-bcf8-2bc42a24c85b','f9104625-252a-49c8-91d4-9401509fceb5',3,'开始','f5cef31d-cb13-4195-86f3-7e2c96f345ee',2,'部门经理',0,0,1558148487,'1','wms'),('cc90886f-5696-4b7d-bdfb-0f961df26059','d5a6a625-34b7-490a-894e-c6f563e1708c','77825e68-4a61-43b8-9081-504088b332e6',3,'开始','33e53484-5b48-4210-a62c-949dd7d6dbaa',2,'节点1',0,0,1559098144,'1','wms'),('da9b32bb-65c0-4c12-9cd0-52edf80b22b2','d5a6a625-34b7-490a-894e-c6f563e1708c','33e53484-5b48-4210-a62c-949dd7d6dbaa',2,'节点1','5fb04da8-7113-4f80-9c91-be19db2c1a9c',2,'节点2',1,2,1559098469,'4','王五123'),('f184c8b3-55e5-4c8e-bdf1-08ec24438ce2','899b7f1f-0844-4358-b605-b69fedff8ff6','77825e68-4a61-43b8-9081-504088b332e6',3,'开始','33e53484-5b48-4210-a62c-949dd7d6dbaa',2,'节点1',0,0,1558149072,'1','wms'),('fb4c361b-3607-4b4b-b218-df69a8514682','899b7f1f-0844-4358-b605-b69fedff8ff6','33e53484-5b48-4210-a62c-949dd7d6dbaa',2,'节点1','5fb04da8-7113-4f80-9c91-be19db2c1a9c',2,'节点2',1,2,1559099810,'4','王五123');

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

/*Data for the table `wx_account` */

insert  into `wx_account`(`WeixinId`,`AppId`,`AppSecret`,`WeixinName`,`WeixinQRCode`,`AccessToken`,`AccessTokenCreateTime`,`JsApiTicket`,`JsApiTicketCreateTime`,`SubscribePageUrl`,`CreateTime`,`Token`,`EncodingAESKey`) values ('gh_3f66fb647ff1','wxeb8c08a03de853d5','485c8b1eeb798401c9af06798f91ec6c','自由reading',NULL,'17_ESnucPG7Ccpi7_t7AfPIUMzUN2v9Wi2uDYlsJRyh4Ho7bivZh9AmoIkXjTXZYUKcTyP1IOo7WifJJiyjnRfAnRaDpyv0zYO4OFiSAUYLGF-pVAAV5vZcDREipUIole9JwQVmH07mOo7yKO6NUOViABALFM','2019-01-12 10:29:53',NULL,NULL,NULL,1546912897,'wangmaosheng','w9jBowtJ9rqgzcHFFyJ0VakWDdNfknphEmFB7pocglk');

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

/*Data for the table `wx_account_miniprogram` */

insert  into `wx_account_miniprogram`(`Id`,`WeixinId`,`AppId`,`AppSecret`) values (1,'gh_3f66fb647ff1','wx20a06c30c8b81e61','6223a1e06f10ebc8ae9fe9eed65042bb');

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

/*Data for the table `wx_keyword` */

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

/*Data for the table `wx_menu` */

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

/*Data for the table `wx_miniprogram_user` */

insert  into `wx_miniprogram_user`(`Id`,`OpenId`,`UnionId`,`NickName`,`City`,`Country`,`Province`,`Language`,`Gender`,`AvatarUrl`,`CreateTime`) values (1,'ofovX1PhIU2fMvagdO1wtJj2U8Bc',NULL,'岁月静好','Lianyungang','China','Jiangsu','zh_CN',1,'https://wx.qlogo.cn/mmopen/vi_32/OmJum4poEd64ibQjFZ4DWqND7OtN2ia9akuYSn9fxWrwT4PaH71mqr7ls3jdYHDlj10Oq8vS9pjw27WjebgMYWWA/132',1555578279);

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

/*Data for the table `wx_news_response` */

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

/*Data for the table `wx_rule` */

insert  into `wx_rule`(`Id`,`RuleName`,`RuleType`,`RequestMsgType`,`ResponseMsgType`,`CreateTime`) values (1,'自动回复',2,1,1,1547103227),(2,'关注回复',1,8,1,1547112830);

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

/*Data for the table `wx_text_response` */

insert  into `wx_text_response`(`Id`,`RuleId`,`Content`,`CreateTime`) values (1,1,'亲，您发的我识别不了啊',1547103227),(2,2,'终于等到你了~~',1547112831);

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

/*Data for the table `wx_user` */

insert  into `wx_user`(`OpenId`,`Subscribe`,`UserName`,`NickName`,`Mobile`,`Sex`,`City`,`Country`,`Province`,`Headimgurl`,`Birthday`,`CreateTime`,`IsSync`) values ('ouUD4sr2rk1MT2UqTTqpGNFot-GI',1,NULL,'不覊之士',NULL,1,'连云港','中国','江苏','http://thirdwx.qlogo.cn/mmopen/eDicfvb2fvAYRduibVLeexKc0ZdJ67jryCbibbwWwTaypS2G5rdajCzRzq4QKbYVazuoe2Z9VTNlTzB6O6xOSep0cB24cfgPOXQ/132',NULL,1547123099,1);

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
