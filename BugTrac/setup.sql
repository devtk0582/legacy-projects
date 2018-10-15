if exists (select * from dbo.sysobjects where id = object_id(N'[bug_posts]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [bug_posts]

if exists (select * from dbo.sysobjects where id = object_id(N'[bug_subscriptions]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [bug_subscriptions]

if exists (select * from dbo.sysobjects where id = object_id(N'[bugs]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [bugs]

if exists (select * from dbo.sysobjects where id = object_id(N'[categories]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [categories]

if exists (select * from dbo.sysobjects where id = object_id(N'[priorities]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [priorities]

if exists (select * from dbo.sysobjects where id = object_id(N'[project_user_xref]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [project_user_xref]

if exists (select * from dbo.sysobjects where id = object_id(N'[projects]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [projects]

if exists (select * from dbo.sysobjects where id = object_id(N'[queries]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [queries]

if exists (select * from dbo.sysobjects where id = object_id(N'[reports]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [reports]

if exists (select * from dbo.sysobjects where id = object_id(N'[sessions]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [sessions]

if exists (select * from dbo.sysobjects where id = object_id(N'[statuses]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [statuses]

if exists (select * from dbo.sysobjects where id = object_id(N'[user_defined_attribute]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [user_defined_attribute]

if exists (select * from dbo.sysobjects where id = object_id(N'[users]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [users]

if exists (select * from dbo.sysobjects where id = object_id(N'[bug_relationships]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [bug_relationships]

if exists (select * from dbo.sysobjects where id = object_id(N'[custom_col_metadata]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [custom_col_metadata]

if exists (select * from dbo.sysobjects where id = object_id(N'[svn_affected_paths]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [svn_affected_paths]

if exists (select * from dbo.sysobjects where id = object_id(N'[svn_revisions]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [svn_revisions]

if exists (select * from dbo.sysobjects where id = object_id(N'[bug_post_attachments]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [bug_post_attachments]

if exists (select * from dbo.sysobjects where id = object_id(N'[orgs]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [orgs]

if exists (select * from dbo.sysobjects where id = object_id(N'[bug_user_flags]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [bug_user_flags]

if exists (select * from dbo.sysobjects where id = object_id(N'[bug_user_seen]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [bug_user_seen]

if exists (select * from dbo.sysobjects where id = object_id(N'[emailed_links]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [emailed_links]

if exists (select * from dbo.sysobjects where id = object_id(N'[queued_notifications]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [queued_notifications]

if exists (select * from dbo.sysobjects where id = object_id(N'[dashboard_items]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dashboard_items]

if exists (select * from dbo.sysobjects where id = object_id(N'[bug_tasks]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [bug_tasks]



/* org */

create table orgs
(
og_id int identity primary key not null,
og_name nvarchar(80) not null,
og_non_admins_can_use int not null default(0),
og_external_user int not null default(0), /* external user can't view post marked internal */
og_can_be_assigned_to int not null default(1),
og_can_edit_sql int not null default(0),
og_can_delete_bug int not null default(0),
og_can_edit_and_delete_posts int not null default(0),
og_can_merge_bugs int not null default(0),
og_can_mass_edit_bugs int not null default(0),
og_can_use_reports int not null default(0),
og_can_edit_reports int not null default(0),
og_can_view_tasks int not null default(0),
og_can_edit_tasks int not null default(0),
og_other_orgs_permission_level int not null default(2), -- 0=none, 1=read, 2=edit

og_category_field_permission_level int not null default(2), -- 0=none, 1=read, 2=edit
og_priority_field_permission_level int not null default(2),
og_assigned_to_field_permission_level int not null default(2),
og_status_field_permission_level int not null default(2),
og_project_field_permission_level int not null default(2),
og_org_field_permission_level int not null default(2),
og_udf_field_permission_level int not null default(2),
og_tags_field_permission_level int not null default(2)
)

create unique index unique_og_name on orgs (og_name)

insert into orgs (og_name, og_external_user, og_can_be_assigned_to, og_other_orgs_permission_level) values ('org1',0,1,2)
insert into orgs (og_name, og_external_user, og_can_be_assigned_to, og_other_orgs_permission_level) values ('developers',0,1,2)
insert into orgs (og_name, og_external_user, og_can_be_assigned_to, og_other_orgs_permission_level) values ('testers',0,1,2)
insert into orgs (og_name, og_external_user, og_can_be_assigned_to, og_other_orgs_permission_level) values ('client one',1,0,0)
insert into orgs (og_name, og_external_user, og_can_be_assigned_to, og_other_orgs_permission_level) values ('client two',1,0,0)

/* USER */

create table users
(
us_id int identity primary key not null,
us_username nvarchar(40) not null,
us_salt int null,
us_password nvarchar(64) not null,
us_firstname nvarchar(60) null,
us_lastname nvarchar(60) null,
us_email nvarchar(120) null,
us_admin int not null default(0),
us_default_query int not null default(0),
us_enable_notifications int not null default(1),
us_auto_subscribe int not null default(0),
us_auto_subscribe_own_bugs int null default(0),
us_auto_subscribe_reported_bugs int null default(0),
us_send_notifications_to_self int null default(0),
us_active int not null default(1),
us_bugs_per_page int null,
us_forced_project int null,
us_reported_notifications int not null default(4),
us_assigned_notifications int not null default(4),
us_subscribed_notifications int not null default(4),
us_signature nvarchar(1000) null,
us_use_fckeditor int not null default(0),
us_enable_bug_list_popups int not null default(1),
/* who created this user */
us_created_user int not null default(1),
us_org int not null default(1),
us_most_recent_login_datetime datetime null
)

create unique index unique_us_username on users (us_username)

insert into users (
us_username, us_firstname, us_lastname, us_password, us_admin, us_default_query, us_org)
values ('admin', 'System', 'Administrator', 'admin', 1, 1, 1)

insert into users (
us_username, us_firstname, us_lastname, us_password, us_admin, us_default_query, us_org)
values ('developer', 'Al', 'Kaline', 'admin', 0, 2, 2)

insert into users (
us_username, us_firstname, us_lastname, us_password, us_admin, us_default_query, us_org)
values ('tester', 'Norman', 'Cash', 'admin', 0, 4, 4)

insert into users (
us_username, us_firstname, us_lastname, us_password, us_admin, us_default_query, us_org)
values ('customer1', 'Bill', 'Freehan', 'admin', 0, 1, 4)

insert into users (
us_username, us_firstname, us_lastname, us_password, us_admin, us_default_query, us_org)
values ('customer2', 'Denny', 'McClain', 'admin', 0, 1, 5)

insert into users (
us_username, us_firstname, us_lastname, us_password, us_admin, us_default_query)
values ('email', 'for POP3', 'btnet_service.exe', 'x', 0, 1)

insert into users (
us_username, us_firstname, us_lastname, us_password, us_admin, us_default_query, us_forced_project)
values ('viewer', 'Read', 'Only', 'admin', 0, 1, 1)

insert into users (
us_username, us_firstname, us_lastname, us_password, us_admin, us_default_query, us_forced_project)
values ('reporter', 'Report And', 'Comment Only', 'admin', 0, 1, 1)

insert into users (
us_username, us_firstname, us_lastname, us_password, us_admin, us_default_query, us_forced_project, us_active)
values ('guest', 'Special', 'cannot save searches, settings', 'guest', 0, 1, 1, 0)

/* SESSIONS */

create table sessions
(
	se_id char(37) not null,
	se_date datetime not null default(getdate()),
	se_user int not null
)


/* EMAILED LINKS */


create table emailed_links
(
el_id char(37) not null,
el_date datetime not null default(getdate()),
el_email nvarchar(120) not null,
el_action nvarchar(20) not null, -- "registration" or "forgot"
el_username nvarchar(40) null,
el_user_id int null,
el_salt int null,
el_password nvarchar(64) null,
el_firstname nvarchar(60) null,
el_lastname nvarchar(60) null
)

/* CATEGORIES */

create table categories
(
ct_id int identity primary key not null,
ct_name nvarchar(80) not null,
ct_sort_seq int not null default(0),
ct_default int not null default(0)
)

create unique index unique_ct_name on categories (ct_name)


insert into categories (ct_name) values('bug')
insert into categories (ct_name) values('enhancement')
insert into categories (ct_name) values('task')
insert into categories (ct_name) values('question')
insert into categories (ct_name) values('ticket')

/* PROJECTS */

create table projects
(
pj_id int identity primary key not null,
pj_name nvarchar(80) not null,
pj_active int not null default(1),
pj_default_user int null,
pj_auto_assign_default_user int null,
pj_auto_subscribe_default_user int null,
pj_enable_pop3 int null,
pj_pop3_username varchar(50) null,
pj_pop3_password nvarchar(20) null,
pj_pop3_email_from nvarchar(120) null,
pj_enable_custom_dropdown1 int not null default(0),
pj_enable_custom_dropdown2 int not null default(0),
pj_enable_custom_dropdown3 int not null default(0),
pj_custom_dropdown_label1 nvarchar(80) null,
pj_custom_dropdown_label2 nvarchar(80) null,
pj_custom_dropdown_label3 nvarchar(80) null,
pj_custom_dropdown_values1 nvarchar(800) null,
pj_custom_dropdown_values2 nvarchar(800) null,
pj_custom_dropdown_values3 nvarchar(800) null,
pj_default int not null default(0),
pj_description nvarchar(200) null,
pj_subversion_repository_url nvarchar(255) null,
pj_subversion_username nvarchar(120) null,
pj_subversion_password nvarchar(80) null,
pj_websvn_url nvarchar(255) null
)

create unique index unique_pj_name on projects (pj_name)

insert into projects (pj_name) values('project 1')
insert into projects (pj_name) values('project 2')


/* BUGS */

create table bugs 
(
bg_id int identity primary key not null,
bg_short_desc nvarchar(200) not null,
bg_reported_user int not null,
bg_reported_date datetime not null,
bg_status int not null,
bg_priority int not null,
bg_org int not null,
bg_category int not null,
bg_project int not null,
bg_assigned_to_user int null,
bg_last_updated_user int null,
bg_last_updated_date datetime null,
bg_user_defined_attribute int null,
bg_project_custom_dropdown_value1 nvarchar(120) null,
bg_project_custom_dropdown_value2 nvarchar(120) null,
bg_project_custom_dropdown_value3 nvarchar(120) null,
bg_tags nvarchar(200) null
)

/* BUG POSTS  - comments, attachments, change history */

create table bug_posts
(
bp_id int identity primary key not null,
bp_bug int not null,
bp_type varchar(8) not null,
bp_user int not null,
bp_date datetime not null,
bp_comment ntext not null,
bp_comment_search ntext null,
bp_email_from nvarchar(800) null,
bp_email_to nvarchar(800) null,
bp_file nvarchar(1000) null,
bp_size int null,
bp_content_type nvarchar(200) null,
bp_parent int null,
bp_original_comment_id int null,
bp_hidden_from_external_users int not null default(0),
bp_email_cc nvarchar(800) null
)

create index bp_index_1 on bug_posts (bp_bug)


/* BUG TASKS - subtasks and time tracking */

create table bug_tasks
(
tsk_id int identity primary key not null,
tsk_bug int not null,
tsk_created_user int not null,
tsk_created_date datetime not null,
tsk_last_updated_user int not null,
tsk_last_updated_date datetime not null,

tsk_assigned_to_user int null,
tsk_planned_start_date datetime null,
tsk_actual_start_date datetime null,
tsk_planned_end_date datetime null,
tsk_actual_end_date datetime null,
tsk_planned_duration decimal(6,2) null,
tsk_actual_duration decimal(6,2) null,
tsk_duration_units nvarchar(20) null,
tsk_percent_complete int null,
tsk_status int null,
tsk_sort_sequence int null,
tsk_description nvarchar(400) null,
)

create index tsk_index_1 on bug_tasks (tsk_bug)

/* BUG POST ATTACHMENTS -- database storage of attachments */

create table bug_post_attachments
(
bpa_id int identity primary key not null,
bpa_post int not null,
bpa_content image not null
)


create unique index bpa_index on bug_post_attachments (bpa_post)

/* BUG USER SUBSCRIPTIONS */

create table bug_subscriptions
(
bs_id int identity primary key not null,
bs_bug int not null,
bs_user int not null,
)


create unique index bs_index_1 on bug_subscriptions (bs_user, bs_bug)
create unique index bs_index_2 on bug_subscriptions (bs_bug, bs_user)

/* BUG USER FLAGS */

create table bug_user_flags
(
fl_bug int not null,
fl_user int not null,
fl_flag int not null
)

create unique index fl_index_1 on bug_user_flags (fl_bug, fl_user)


/* BUG USER SEEN */

create table bug_user_seen
(
sn_bug int not null,
sn_user int not null,
sn_seen int not null
)

create unique index sn_index_1 on bug_user_seen (sn_bug, sn_user)



/* BUG RELATIONSHIPS */

create table bug_relationships
(
re_id int identity primary key not null,
re_bug1 int not null,
re_bug2 int not null,
re_type nvarchar(500) null,
re_direction int not null
)

create unique index re_index_1 on bug_relationships (re_bug1, re_bug2)
create unique index re_index_2 on bug_relationships (re_bug2, re_bug1)


/* PROJECT USER XREF */

create table project_user_xref
(
pu_id int identity primary key not null,
pu_project int not null,
pu_user int not null,
pu_auto_subscribe int not null default(0),
/* 0=none, 1=view only, 2=edit, 3=reporter */
pu_permission_level int not null default(2),
pu_admin int not null default(0),
)

create unique index pu_index_1 on project_user_xref (pu_project, pu_user)
create unique index pu_index_2 on project_user_xref (pu_user, pu_project)

insert into project_user_xref (pu_project, pu_user, pu_permission_level) values (1,7,1)
insert into project_user_xref (pu_project, pu_user, pu_permission_level) values (2,7,1)
insert into project_user_xref (pu_project, pu_user, pu_permission_level) values (1,8,3)
insert into project_user_xref (pu_project, pu_user, pu_permission_level) values (2,8,0)
insert into project_user_xref (pu_project, pu_user, pu_permission_level) values (1,9,1)
insert into project_user_xref (pu_project, pu_user, pu_permission_level) values (2,9,1)

/* USER DEFINED ATTRIBUTE */

create table user_defined_attribute
(
udf_id int identity primary key not null,
udf_name nvarchar(60) not null,
udf_sort_seq int not null default(0),
udf_default int not null default(0)
)

create unique index unique_udf_name on user_defined_attribute (udf_name)

insert into user_defined_attribute (udf_name) values ('whatever')
insert into user_defined_attribute (udf_name) values ('anything')


/* STATUSES */

create table statuses
(
st_id int identity primary key not null,
st_name nvarchar(60) not null,
st_sort_seq int not null default(0),
st_style nvarchar(30) null,
st_default int not null default(0)
)

create unique index unique_st_name on statuses (st_name)

insert into statuses (st_name, st_sort_seq, st_style, st_default) values ('new', 1, 'st1', 1)
insert into statuses (st_name, st_sort_seq, st_style, st_default) values ('in progress', 2, 'st2', 0)
insert into statuses (st_name, st_sort_seq, st_style, st_default) values ('checked in', 3, 'st3', 0)
insert into statuses (st_name, st_sort_seq, st_style, st_default) values ('re-opened', 4, 'st4', 0)
insert into statuses (st_name, st_sort_seq, st_style, st_default) values ('closed', 5, 'st5', 0)

/* PRIORITIES */

create table priorities
(
pr_id int identity primary key not null,
pr_name nvarchar(60) not null,
pr_sort_seq int not null default(0),
pr_background_color nvarchar(14) not null,
pr_style nvarchar(30) null,
pr_default int not null default(0)
)

create unique index unique_pr_name on priorities (pr_name)

insert into priorities (pr_name, pr_sort_seq, pr_background_color, pr_style) values ('high', 1, '#ff9999', 'pr1_')
insert into priorities (pr_name, pr_sort_seq, pr_background_color, pr_style) values ('med', 2, '#ffdddd', 'pr2_')
insert into priorities (pr_name, pr_sort_seq, pr_background_color, pr_style) values ('low', 3, '#ffffff', 'pr3_')


/* CUSTOM COL METADATA */

create table custom_col_metadata
(
ccm_colorder int not null,
ccm_dropdown_vals nvarchar(1000) not null default(''),
ccm_sort_seq int default(0),
ccm_dropdown_type varchar(20) null
)

create unique index cdv_index on custom_col_metadata (ccm_colorder)



create table svn_revisions
(
svnrev_id int identity primary key not null,
svnrev_revision int not null,
svnrev_bug int not null,
svnrev_repository nvarchar(400) not null,
svnrev_author nvarchar(100) not null,
svnrev_svn_date nvarchar(100) not null,
svnrev_btnet_date datetime not null,
svnrev_msg ntext not null
)

create index svn_bug_index on svn_revisions (svnrev_bug)


/* SVN AFFECTED PATH */

create table svn_affected_paths
(
svnap_id int identity primary key not null,
svnap_svnrev_id int not null,
svnap_action nvarchar(8) not null,
svnap_path nvarchar(400) not null
)

create index svn_revision_index on svn_affected_paths (svnap_svnrev_id)


/* REPORTS */
create table reports
(
rp_id int identity primary key not null,
rp_desc nvarchar(200) not null,
rp_sql ntext not null,
rp_chart_type varchar(8) not null
)

create unique index unique_rp_desc on reports (rp_desc)

/* Some examples to get you started */

insert into reports (rp_desc, rp_sql, rp_chart_type)
values('Bugs by Status',
'select st_name [status], count(1) [count] from bugs inner join statuses on bg_status = st_id group by st_name order by st_name',
'pie')

insert into reports (rp_desc, rp_sql, rp_chart_type)
values('Bugs by Priority',
'select pr_name [priority], count(1) [count] from bugs inner join priorities on bg_priority = pr_id group by pr_name order by pr_name',
'pie')

insert into reports (rp_desc, rp_sql, rp_chart_type)
values('Bugs by Category',
'select ct_name [category], count(1) [count] from bugs inner join categories on bg_category = ct_id group by ct_name order by ct_name',
'pie')

insert into reports (rp_desc, rp_sql, rp_chart_type)
values('Bugs by Month',
'select month(bg_reported_date) [month], count(1) [count] from bugs group by year(bg_reported_date), month(bg_reported_date) order by year(bg_reported_date), month(bg_reported_date)',
'bar')

insert into reports (rp_desc, rp_sql, rp_chart_type)
values('Bugs by Day of Year',
'select datepart(dy, bg_reported_date) [day of year], count(1) [count] from bugs group by datepart(dy, bg_reported_date), datepart(dy,bg_reported_date) order by 1',
'line')

insert into reports (rp_desc, rp_sql, rp_chart_type)
values('Bugs by User',
'select bg_reported_user, count(1) [r] into #t from bugs group by bg_reported_user; select bg_assigned_to_user, count(1) [a] into #t2 from bugs group by bg_assigned_to_user; select us_username, r [reported], a [assigned] from users left outer join #t on bg_reported_user = us_id left outer join #t2 on bg_assigned_to_user = us_id order by 1', 
'table')

insert into reports (rp_desc, rp_sql, rp_chart_type)
values ('Hours by Org, Year, Month',
'select og_name [organization], '
+ char(10) + ' datepart(year,tsk_created_date) [year],  '
+ char(10) + ' datepart(month,tsk_created_date) [month], '
+ char(10) + ' convert(decimal(8,1),sum( '
+ char(10) + ' case when tsk_duration_units = ''minutes'' then tsk_actual_duration / 60.0 '
+ char(10) + ' when tsk_duration_units = ''days'' then tsk_actual_duration * 8.0  '
+ char(10) + ' else tsk_actual_duration * 1.0 end)) [total hours] '
+ char(10) + ' from bug_tasks '
+ char(10) + ' inner join bugs on tsk_bug = bg_id '
+ char(10) + ' inner join orgs on bg_org = og_id '
+ char(10) + ' where isnull(tsk_actual_duration,0) <> 0 '
+ char(10) + ' group by og_name,datepart(year,tsk_created_date), datepart(month,tsk_created_date) ',
'table')

insert into reports (rp_desc, rp_sql, rp_chart_type)
values ('Hours Remaining by Project',
'select pj_name [project], '
+ char(10) + ' convert(decimal(8,1),sum( '
+ char(10) + ' case  '
+ char(10) + ' when tsk_duration_units = ''minutes'' then  '
+ char(10) + ' tsk_planned_duration / 60.0 * .01 * (100 - isnull(tsk_percent_complete,0)) '
+ char(10) + ' when tsk_duration_units = ''days'' then ' 
+ char(10) + ' tsk_planned_duration * 8.0  * .01 * (100 - isnull(tsk_percent_complete,0)) '
+ char(10) + ' else tsk_planned_duration * .01 * (100 - isnull(tsk_percent_complete,0)) '
+ char(10) + ' end)) [total hours] '
+ char(10) + ' from bug_tasks '
+ char(10) + ' inner join bugs on tsk_bug = bg_id '
+ char(10) + ' inner join projects on bg_project = pj_id '
+ char(10) + ' where isnull(tsk_planned_duration,0) <> 0 '
+ char(10) + ' group by pj_name ',
'table')



/* QUERIES */

create table queries
(
qu_id int identity primary key not null,
qu_desc nvarchar(200) not null,
qu_sql ntext not null,
qu_default int null,
qu_user int null,
qu_org int null
)

create unique index unique_qu_desc on queries (qu_desc, qu_user, qu_org)


create table queued_notifications
(
qn_id int identity primary key not null,
qn_date_created datetime not null,
qn_bug int not null,
qn_user int not null,
qn_status nvarchar(30) not null,
qn_retries int not null,
qn_last_exception nvarchar(1000),
qn_to nvarchar(200) not null,
qn_from nvarchar(200) not null,
qn_subject nvarchar(400) not null,
qn_body ntext not null
)


create table dashboard_items
(
ds_id int identity primary key not null,
ds_user int not null,
ds_report int not null,
ds_chart_type varchar(8) not null,
ds_col int not null,
ds_row int not null
)

create index ds_user_index on dashboard_items (ds_user)


/*

The web pages that display the bugs expect the first two columns of the
queries to be the color or style of the row and the bug id.

Here are examples to get you started.

*/

insert into queries (qu_desc, qu_sql, qu_default) values (
'all bugs',
'select isnull(pr_background_color,''#ffffff''), bg_id [id], isnull(fl_flag,0) [$FLAG], '
+ char(10) + ' bg_short_desc [desc], isnull(pj_name,'''') [project], isnull(og_name,'''') [organization], isnull(ct_name,'''') [category], rpt.us_username [reported by],'
+ char(10) + ' bg_reported_date [reported on], isnull(pr_name,'''') [priority], isnull(asg.us_username,'''') [assigned to],'
+ char(10) + ' isnull(st_name,'''') [status], isnull(lu.us_username,'''') [last updated by], bg_last_updated_date [last updated on]'
+ char(10) + ' from bugs '
+ char(10) + ' left outer join bug_user_flags on fl_bug = bg_id and fl_user = $ME '
+ char(10) + ' left outer join users rpt on rpt.us_id = bg_reported_user'
+ char(10) + ' left outer join users asg on asg.us_id = bg_assigned_to_user'
+ char(10) + ' left outer join users lu on lu.us_id = bg_last_updated_user'
+ char(10) + ' left outer join projects on pj_id = bg_project'
+ char(10) + ' left outer join orgs on og_id = bg_org'
+ char(10) + ' left outer join categories on ct_id = bg_category'
+ char(10) + ' left outer join priorities on pr_id = bg_priority'
+ char(10) + ' left outer join statuses on st_id = bg_status'
+ char(10) + ' order by bg_id desc',
1)

insert into queries (qu_desc, qu_sql, qu_default) values (
'open bugs',
'select isnull(pr_background_color,''#ffffff''), bg_id [id], isnull(fl_flag,0) [$FLAG], '
+ char(10) + ' bg_short_desc [desc], isnull(pj_name,'''') [project], isnull(og_name,'''') [organization], isnull(ct_name,'''') [category], rpt.us_username [reported by],'
+ char(10) + ' bg_reported_date [reported on], isnull(pr_name,'''') [priority], isnull(asg.us_username,'''') [assigned to],'
+ char(10) + ' isnull(st_name,'''') [status], isnull(lu.us_username,'''') [last updated by], bg_last_updated_date [last updated on]'
+ char(10) + ' from bugs '
+ char(10) + ' left outer join bug_user_flags on fl_bug = bg_id and fl_user = $ME '
+ char(10) + ' left outer join users rpt on rpt.us_id = bg_reported_user'
+ char(10) + ' left outer join users asg on asg.us_id = bg_assigned_to_user'
+ char(10) + ' left outer join users lu on lu.us_id = bg_last_updated_user'
+ char(10) + ' left outer join projects on pj_id = bg_project'
+ char(10) + ' left outer join orgs on og_id = bg_org'
+ char(10) + ' left outer join categories on ct_id = bg_category'
+ char(10) + ' left outer join priorities on pr_id = bg_priority'
+ char(10) + ' left outer join statuses on st_id = bg_status'
+ char(10) + ' where bg_status <> 5 order by bg_id desc',
0)

insert into queries (qu_desc, qu_sql, qu_default) values (
'open bugs assigned to me',
'select isnull(pr_background_color,''#ffffff''), bg_id [id], isnull(fl_flag,0) [$FLAG], '
+ char(10) + ' bg_short_desc [desc], isnull(pj_name,'''') [project], isnull(og_name,'''') [organization], isnull(ct_name,'''') [category], rpt.us_username [reported by],'
+ char(10) + ' bg_reported_date [reported on], isnull(pr_name,'''') [priority], isnull(asg.us_username,'''') [assigned to],'
+ char(10) + ' isnull(st_name,'''') [status], isnull(lu.us_username,'''') [last updated by], bg_last_updated_date [last updated on]'
+ char(10) + ' from bugs '
+ char(10) + ' left outer join bug_user_flags on fl_bug = bg_id and fl_user = $ME '
+ char(10) + ' left outer join users rpt on rpt.us_id = bg_reported_user'
+ char(10) + ' left outer join users asg on asg.us_id = bg_assigned_to_user'
+ char(10) + ' left outer join users lu on lu.us_id = bg_last_updated_user'
+ char(10) + ' left outer join projects on pj_id = bg_project'
+ char(10) + ' left outer join orgs on og_id = bg_org'
+ char(10) + ' left outer join categories on ct_id = bg_category'
+ char(10) + ' left outer join priorities on pr_id = bg_priority'
+ char(10) + ' left outer join statuses on st_id = bg_status'
+ char(10) + ' where bg_status <> 5 and bg_assigned_to_user = $ME order by bg_id desc',
0)

insert into queries (qu_desc, qu_sql, qu_default) values (
'checked in bugs - for QA',
'select isnull(pr_background_color,''#ffffff''), bg_id [id], isnull(fl_flag,0) [$FLAG], '
+ char(10) + ' bg_short_desc [desc], isnull(pj_name,'''') [project], isnull(og_name,'''') [organization], isnull(ct_name,'''') [category], rpt.us_username [reported by],'
+ char(10) + ' bg_reported_date [reported on], isnull(pr_name,'''') [priority], isnull(asg.us_username,'''') [assigned to],'
+ char(10) + ' isnull(st_name,'''') [status], isnull(lu.us_username,'''') [last updated by], bg_last_updated_date [last updated on]'
+ char(10) + ' from bugs '
+ char(10) + ' left outer join bug_user_flags on fl_bug = bg_id and fl_user = $ME '
+ char(10) + ' left outer join users rpt on rpt.us_id = bg_reported_user'
+ char(10) + ' left outer join users asg on asg.us_id = bg_assigned_to_user'
+ char(10) + ' left outer join users lu on lu.us_id = bg_last_updated_user'
+ char(10) + ' left outer join projects on pj_id = bg_project'
+ char(10) + ' left outer join orgs on og_id = bg_org'
+ char(10) + ' left outer join categories on ct_id = bg_category'
+ char(10) + ' left outer join priorities on pr_id = bg_priority'
+ char(10) + ' left outer join statuses on st_id = bg_status'
+ char(10) + ' where bg_status = 3 order by bg_id desc',
0)


insert into queries (qu_desc, qu_sql, qu_default) values (
'demo use of css classes',
'select isnull(pr_style + st_style,''datad''), bg_id [id], isnull(fl_flag,0) [$FLAG], bg_short_desc [desc], isnull(pr_name,'''') [priority], isnull(st_name,'''') [status]'
+ char(10) + ' from bugs '
+ char(10) + ' left outer join bug_user_flags on fl_bug = bg_id and fl_user = $ME '
+ char(10) + ' left outer join priorities on pr_id = bg_priority '
+ char(10) + ' left outer join statuses on st_id = bg_status '
+ char(10) + ' order by bg_id desc',
0)

insert into queries (qu_desc, qu_sql, qu_default) values (
'demo last comment as column',
'select ''#ffffff'', bg_id [id], bg_short_desc [desc], ' 
+ char(10) + ' substring(bp_comment_search,1,40) [last comment], bp_date [last comment date]'
+ char(10) + ' from bugs'
+ char(10) + ' left outer join bug_posts on bg_id = bp_bug'
+ char(10) + ' and bp_type = ''comment''' 
+ char(10) + ' and bp_date in (select max(bp_date) from bug_posts where bp_bug = bg_id)'
+ char(10) + ' WhErE 1 = 1'
+ char(10) + ' order by bg_id desc',
0)


insert into queries (qu_desc, qu_sql, qu_default) values (
'days in status',
'select case '
+ char(10) + ' when datediff(d, isnull(bp_date,bg_reported_date), getdate()) > 90 then ''#ff9999'' '
+ char(10) + ' when datediff(d, isnull(bp_date,bg_reported_date), getdate()) > 30 then ''#ffcccc'' '
+ char(10) + ' when datediff(d, isnull(bp_date,bg_reported_date), getdate()) > 7 then ''#ffdddd''  '
+ char(10) + ' else ''#ffffff'' end, '
+ char(10) + ' bg_id [id], bg_short_desc [desc], '
+ char(10) + ' datediff(d, isnull(bp_date,bg_reported_date), getdate()) [days in status],'
+ char(10) + ' st_name [status], '
+ char(10) + ' isnull(bp_comment,'''') [last status change], isnull(bp_date,bg_reported_date) [status date]'
+ char(10) + ' from bugs'
+ char(10) + ' inner join statuses on bg_status = st_id'
+ char(10) + ' left outer join bug_posts on bg_id = bp_bug'
+ char(10) + ' and bp_type = ''update'' '
+ char(10) + ' and bp_comment like ''changed status from%'' '
+ char(10) + ' and bp_date in (select max(bp_date) from bug_posts where bp_bug = bg_id)'
+ char(10) + ' WhErE 1 = 1 '
+ char(10) + ' order by 4 desc ',
0)


insert into queries (qu_desc, qu_sql, qu_default) values (
'bugs with attachments',
+ char(10) + ' select bp_bug, sum(bp_size) bytes '
+ char(10) + ' into #t '
+ char(10) + ' from bug_posts '
+ char(10) + ' where bp_type = ''file'' '
+ char(10) + ' group by bp_bug '
+ char(10) + ' select ''#ffffff'', bg_id [id], bg_short_desc [desc], '
+ char(10) + ' bytes, rpt.us_username [reported by] ' 
+ char(10) + ' from bugs '
+ char(10) + ' inner join #t on bp_bug = bg_id '
+ char(10) + ' left outer join users rpt on rpt.us_id = bg_reported_user '
+ char(10) + ' WhErE 1 = 1 '
+ char(10) + ' order by bytes desc ' 
+ char(10) + ' drop table #t ',
0)

insert into queries (qu_desc, qu_sql, qu_default) values (
'bugs with related bugs',
+ char(10) + ' select ''#ffffff'', bg_id [id], bg_short_desc [desc], '
+ char(10) + ' isnull(st_name,'''') [status], '
+ char(10) + ' count(*) [number of related bugs] '
+ char(10) + ' from bugs '
+ char(10) + ' inner join bug_relationships on re_bug1 = bg_id '
+ char(10) + ' inner join statuses on bg_status = st_id '
+ char(10) + ' /*ENDWHR*/ '
+ char(10) + ' group by bg_id, bg_short_desc, isnull(st_name,'''') '
+ char(10) + ' order by bg_id desc ',
0)