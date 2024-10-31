﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EFCorePractice
{
    /// <summary>
    /// 步骤汇总：
    /// 1、建类库项目，放实体类、DbContext、配置类等
    /// DbContext中不配置数据库连接，而是为DbContext增加一个DbContextOptions类型的构造函数。
    /// 2、EFCore项目安装对应数据库的EFCore Provider
    /// 3、asp.net core项目引用EFCore项目，并且通过AddDbContext来注入DbContext及对DbContext进行配置。
    /// 4、Controller中就可以注入DbContext类使用了。
    /// 5、让开发环境的Add-Migration知道连接哪个数据库
    /// 在EFCore项目中创建一个实现了IDesignTimeDbContextFactory的类。
    /// 并且在CreateDbContext返回一个连接开发数据库的DbContext。
    /// 如果不在乎连接字符串被上传到Git，就可以把连接字符串直接写死到CreateDbContext；如果在乎，那么CreateDbContext里面很难读取到VS中通过简单的方法设置的环境变量，所以必须把连接字符串配置到Windows的正式的环境变量中，然后再 Environment.GetEnvironmentVariable读取。
    /// 6、正常执行Add-Migration INit/Add-Migration Update、Update-Database迁移就行了。需要把EFCore项目设置为启动项目，并且在【程序包管理器控制台】中也要选中EFCore项目，并且安装Microsoft.EntityFrameworkCore.SqlServer、Microsoft.EntityFrameworkCore.Tools
    /// </summary>
    internal class MyDbContextFactory : IDesignTimeDbContextFactory<MyDbContext>
    {
        //Develop use only(Add-Migration INit, Update-DataBase etc.)
        public MyDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<MyDbContext> builder = new DbContextOptionsBuilder<MyDbContext>();
            string connStr = "data source=Rsp-sql-dev;initial catalog=KimmerTestDB;user id=sa;password=p@ssw0rd;MultipleActiveResultSets=True; Encrypt=False";
            builder.UseSqlServer(connStr);
            MyDbContext ctx = new MyDbContext(builder.Options);
            return ctx;
        }
    }
}
