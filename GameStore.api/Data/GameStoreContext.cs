using GameStore.api.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;

/// <summary>
/// Summary description for Class1
/// </summary>
/// 

namespace GameStore.api.Data;
public class GameStoreContext : DbContext
{
	public GameStoreContext(DbContextOptions<GameStoreContext> options) : base(options)
	{
		//
		// TODO: Add constructor logic here
		//
	}

	// Inital dataset (Table) instance vaule set for the property, used for querying.  
	public DbSet<Game> Games => Set<Game>();	

		
}
