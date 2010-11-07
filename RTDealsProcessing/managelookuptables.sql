select * from category a inner join subcategory b on a.categoryid=b.CategoryID

-- ==== pattern ===
--   insert  pattern (description, patterns, multiitemrelation) values ('gaming headset', 'gaming\\s*headset','O');
--  
--     select * from pattern;

--  delete from pattern where patternid=1;

-- ==== pattern_match ===
   insert  pattern_match 
  (description, matchpatternid, ExcludePatternID, Accuracy, SubcategoryID,ProductID,DealType,StoreID,FreeShipping)
   values ('gaming headset',6,NULL,90,1,NULL,NULL,NULL,null);
--  
    select matchid,a.Description MatchDesc, a.MatchPatternID,b.Description MPDesc,c.Description EXDesc, 
      a.SubcategoryID,dc.Name CatgName,d.Name SubcName,a.ProductID, e.Name ProdName, f.Name BrandName, a.DealType, 
      a.StoreID, g.Name StoreName, a.FreeShipping
      from pattern_match a inner join pattern b on a.MatchPatternID=b.PatternID
      left join pattern c on a.ExcludePatternID=c.PatternID
      left join Subcategory d on a.SubcategoryID=d.SubCategoryID
      left join Category dc on dc.categoryid = d.CategoryID
      left join Product e on a.ProductID=e.ProductID
      left join Brand f on e.BrandID=f.BrandID
      left join Store g on a.StoreID = g.StoreID;

select a.DealID,a.Title,a.CleanTitle,a.RawDealID,
  b.SubcategoryID,dc.Name CatgName,d.Name SubcName,
  a.ProductID, e.Name ProdName, f.Name BrandName, 
  a.StoreID, g.Name StoreName,
  a.DealType, a.FreeShipping, a.InTime from deals_processed a 
  left join deal_subcategory b on a.DealID=b.DealID
      left join Subcategory d on b.SubcategoryID=d.SubCategoryID
      left join Category dc on dc.categoryid = d.CategoryID
      left join Product e on a.ProductID=e.ProductID
      left join Brand f on e.BrandID=f.BrandID
      left join Store g on a.StoreID = g.StoreID;