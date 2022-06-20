//Define a Style like so

//<Window.Resources>
//    ....
//    <Style x:Key = "myHeaderStyle" TargetType = "{x:Type GridViewColumnHeader}" >
  
//          < Setter Property = "Visibility" Value = "Collapsed" />
     
//         </ Style >
//     </ Window.Resources >
//     Apply it like so

//<GridView ColumnHeaderContainerStyle="{StaticResource myHeaderStyle}">
//    ....
//</GridView>