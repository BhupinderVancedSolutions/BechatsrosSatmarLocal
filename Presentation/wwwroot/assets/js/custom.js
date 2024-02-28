$(document).on('click', '.toggle-password', function () {
    $(this).toggleClass("fa-eye fa-eye-slash");
    var input = $("#password");
    input.attr('type') === 'password' ? input.attr('type', 'text') : input.attr('type', 'password')
});

// Sidebar Js
document.addEventListener("DOMContentLoaded", function(event) { 
    const showNavbar = (toggleId, bodyId) =>{
        const toggle = document.getElementById(toggleId),
        bodypd = document.getElementById(bodyId)
        
        // Validate that all variables exist
        if(toggle && bodypd){
            toggle.addEventListener('click', ()=>{
                bodypd.classList.toggle('sidebar-close')
            })
        }
    }
    showNavbar('sidebar-toggler','wrapper') 
    
});

// Uload Multiple file Js
	// ------------  File upload BEGIN ------------
	$('.fileInput').on('change',function(event){
		var files = event.target.files;
		for (var i = 0; i < files.length; i++) {
			var file = files[i];
			$("<div class='file_preview'><div class='text'><svg width='11' height='10' class='me-2'><use xlink:href='assets/images/sprite-icons.svg#attached-icon'></use></svg>" + file.name + "</div><div class='file-remove' data-id='" + file.name + "' ><svg width='6' height='6'><use xlink:href='assets/images/sprite-icons.svg#close-icon'></use></svg></div></div>").insertAfter('#upload-area');
		}	
	});

    
	
	//Click to remove item
	$('body').on('click', '.file_preview', function() {
		$(this).remove();
	});
	// ------------ File upload END ------------ 

	// ------------  Side Panel ------------
    $(document).on('click', '[data-toggle="slide-panel"]', function () {
        var $target = $($(this).data('target')); 
        $target.toggleClass('show');
        $target.removeClass('full-screen');
        return false;
    });
    $(document).on('click', '[data-toggle="screen-resize"]', function () {
        var $target = $($(this).data('target')); 
        $target.toggleClass('full-screen');
        return false;
    });

    // ------------  Import Loading------------
    $(document).ready(function () {
        //$(".btn-import").click(function () {
        //    $(".import-loading").show(); 
        //    setTimeout(function() {
        //        $(".import-loading").hide(); 
        //        $(".import-complete").show(); 
        //    }, 5000);
        //});
        $(".btn-yes").click(function () {
            $(".import-complete").show(); 
        });

        $("#user-detail .btn-edit").click(function () { 
            $(".slide-title .balance .btn").addClass('btn-border');  
            $(".slide-title .balance .btn").removeClass('btn-border-white');  
        });
        $(".price-selected .amt-change-btn").click(function () { $(".price-selected .static-amt").hide() });
        $(".price-selected .amt-change-btn").click(function () { $(".price-selected .enter-amt").css("display", "block") });
    });

    // ------------  Upload Image ------------ 
    function readURL(input) {
        if (input.files && input.files[0]) {
          var reader = new FileReader();
          reader.onload = function(e) {
              $('.file-upload-image').attr('src', e.target.result);              
            $('.image-upload-wrap').addClass('uploaded');
          }; 
          reader.readAsDataURL(input.files[0]); 
        } else {
          removeUpload();
        }
      }

 
