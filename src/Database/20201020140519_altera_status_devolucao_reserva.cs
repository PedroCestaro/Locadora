using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Migrator.Framework;
using Simple.Migrator.Fluent;
using System.Data;

namespace Locadora.Database
{
    [Migration(20201020140519)]
    public class Migration20201020140519 : FluentMigration
    {
        public override void Up(SchemaAction schema)
        {
            schema.ChangeTable("t_reservations", t =>
            {
                t.RemoveColumn("returned");
                t.AddBoolean("returned").NotNullable().Default(false);
            });

        }

        public override void Down(SchemaAction schema)
        {
            schema.ChangeTable("t_reservations", t =>
            {
                t.RemoveColumn("returned");
            });
        }
    }

}