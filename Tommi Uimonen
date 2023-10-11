Simple example of ORA-50028 with Oracle.ManagedDataAccess.Core 23.2.0-dev. Happens with both async and sync versions of GetFieldValue

Connection string needs to be given as argument.

DB setup script:

create type odp_sample_sdo_point_type as object
 (sdo_point_x number, sdo_point_y number, sdo_point_z number);
/

create type odp_sample_sdo_elem_info_type as varray(100) of number;
/

create type odp_sample_sdo_ordinate_type as varray(100) of number;
/

create type odp_sample_sdo_geometry_type as object 
 (sdo_gtype number, sdo_srid number, sdo_point odp_sample_sdo_point_type,
  sdo_elem_info odp_sample_sdo_elem_info_type,
  sdo_ordinate odp_sample_sdo_ordinate_type);
/

create table odp_sample_sdo_geo_obj_tab of odp_sample_sdo_geometry_type;

insert into odp_sample_sdo_geo_obj_tab values(odp_sample_sdo_geometry_type(
 123,123,odp_sample_sdo_point_type(123.45,123.45,123.45),
 odp_sample_sdo_elem_info_type(123,123),
 odp_sample_sdo_ordinate_type(123.45,123.45)));

commit;
