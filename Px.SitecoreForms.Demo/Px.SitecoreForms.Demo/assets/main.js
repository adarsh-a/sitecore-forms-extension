function disabledelete(){
	$(document).find(".js-btn-delete-action")
	.click(function(e)
	{
		if($(document).find(".js-btn-delete-action").size() >1){
			var containerclass = $(this).attr("data-delete-container");
			if(containerclass.length)
			{
				var currentContainer = $(this).closest("."+containerclass);
				if(currentContainer.length)
				{
					$(currentContainer).remove();
					
				}
			}
		}
		
	});
}


function duplicateCont(count){
	$(document).find(".js-btn-add-action")
	.click(function(e)
	{
		console.log("add clicked");
		console.log($(this).attr("data-duplicate-container"));
		
		var containerToAddAttr = $(this).attr("data-duplicate-container");
		if(containerToAddAttr.length)
		{
			
			var container=$(document).find("."+containerToAddAttr).first();
			if(container.length)
			{
				console.log("adding");
				var containerClone = $(container).clone();
				$(containerClone).find('input[type=text]').val("");
				$(containerClone).find('textarea').val("");
				count++;

				/*$(containerClone).find(':input').each(function(index){
					if(this.type!="button")
					{
						var oldName = this.name;
						var oldId = this.id;
						//console.log("name is"+ oldName +" and id is"+oldId);
						var newName = oldName +"-px"+count;
						var newId = oldId+"-px"+count;
						//console.log("new name is"+ newName +" and new id is"+newId);

						$(this).attr('name',newName);
						$(this).attr('id',newId);

					}
					
				});*/

											
							
				//do not delete below
				$(this).before( containerClone );
				disabledelete();
			}
			else
			{
				console.log("container not found");
			}
		}else
		{
			console.log("containerToAddAttr not found");
		}
		
	});
}


function uuidv4() {
  return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
    var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
	
    return v.toString(16);
  });
}


function showhide()
{
	$(document).find(".js-btn-toggle").click(function(e)
	{$('.alert-danger').remove();
		console.log("toggle")
		var containerShowClass=$(this).attr("data-container-show");
		var containerHideClass = $(this).attr("data-container-hide");
		
		var canProceed=true;
		
		var containerHide = $(document).find("."+containerHideClass);
		$(containerHide).find(":input").each(function(e){
			if($(this).attr('data-val-required'))
			{
				console.log("Performing Validation Before moving to next page " + $(this).attr('data-val-required'));
				var divError = "<div class='alert alert-danger'>"+ $(this).attr('data-val-required')+"</div>";
				if($(this).val().length <=0)
				{
					$(this).after(divError);
					console.log("Error found");
					canProceed=false;
					
				}
			}
			
		});
		
		if(canProceed || $(this).hasClass("btn-previous"))
		{
		var containerShow = $(document).find("."+containerShowClass);
		$(containerHide).addClass("hide");
		$(containerShow).removeClass("hide");
		}
	});
}

$(document).ready(function(){
	console.log("ready to attack");
	let count = 0;
	//disabledelete();
	duplicateCont(count);
	showhide();


});