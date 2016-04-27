angular.module('app.views', ['account.html', 'configuration.html', 'dashboard.html', 'fmsresults.html', 'fmstest.html', 'placeholder.html', 'admin/dirPagination.tpl.html', 'capture-demo/index.html', 'capture/index.html', 'demo/trends/index.html', 'fms-bak/demo/analysis.html', 'fms-bak/demo/index.html', 'fms-bak/demo/partials/analysis-coronal.html', 'fms-bak/demo/partials/analysis-layout-emphasized.html', 'fms-bak/demo/partials/analysis-sagittal.html', 'fms-bak/demo/partials/analysis-transverse.html', 'fms-bak/demo/partials/summary-content.html', 'fms-bak/demo/partials/summary-layout.html', 'fms-bak/demo/partials/summary-menu.html', 'fms-bak/demo/partials/summary-movement-overview.html', 'fms-bak/demo/partials/summary-overall-scores.html', 'fms-bak/demo/partials/test-coronal.html', 'fms-bak/demo/partials/test-layout-emphasized.html', 'fms-bak/demo/partials/test-sagittal.html', 'fms-bak/demo/partials/test-transverse.html', 'fms-bak/demo/summary.html', 'fms-bak/demo/test.html', 'fms-bak/live/analysis.html', 'fms-bak/live/index.html', 'fms-bak/live/partials/analysis-menu.html', 'fms-bak/live/partials/breadcrumbs.html', 'fms-bak/live/partials/header.html', 'fms-bak/live/partials/iterations.html', 'fms-bak/live/partials/summary-legend.html', 'fms-bak/live/partials/summary-menu.html', 'fms-bak/live/partials/test-menu.html', 'fms-bak/live/partials/test-trials.html', 'fms-bak/live/summary.html', 'fms-bak/live/test.html', 'group/edit.html', 'group/list.html', 'group/partials/form.html', 'group/partials/upload-photo.html', 'group/view.html', 'import/index.html', 'movements/analysis-demo/index.html', 'movements/analysis/index.html', 'movements/analysis/layouts/emphasis.html', 'movements/comparison/index.html', 'movements/explorer/index.html', 'overlay/movement-editor/footer.html', 'overlay/movement-editor/index.html', 'overlay/thumbnail-selector/footer.html', 'overlay/thumbnail-selector/index.html', 'partials/breadcrumbs.html', 'partials/directives/ui-avatar.html', 'partials/directives/ui-editable-fields/btn-group.html', 'partials/directives/ui-editable-fields/field-horizontal.html', 'partials/directives/ui-editable-fields/field-value-generic.html', 'partials/directives/ui-editable-fields/field-value-timestamp.html', 'partials/directives/ui-editable-fields/field-value-with-units.html', 'partials/directives/ui-editable-fields/field-vertical.html', 'partials/directives/ui-editable-fields/fields.html', 'partials/directives/ui-editable-fields/standalone-field.html', 'partials/directives/ui-filesystem/container.html', 'partials/directives/ui-filesystem/layouts/details.html', 'partials/directives/ui-filesystem/layouts/large-tiles.html', 'partials/directives/ui-filesystem/layouts/small-tiles.html', 'partials/directives/ui-movement/placeholder.html', 'partials/directives/ui-movement/playback.html', 'partials/directives/ui-movement/preview.html', 'partials/directives/ui-page-title.html', 'partials/directives/ui-taggable-input.html', 'partials/header.html', 'partials/loading.html', 'partials/navigation.html', 'partials/overlay.html', 'partials/select-profile.html', 'profile/create.html', 'profile/list.html', 'profile/partials/current-fms-plot.html', 'profile/partials/demo-session.html', 'profile/partials/final-fms-plot.html', 'profile/partials/movement-indicators.html', 'profile/partials/new-profile-modal.html', 'profile/partials/screening-indicators.html', 'profile/partials/upload-photo.html', 'profile/view.html', 'screenings/index.html', 'screenings/live/index.html', 'screenings/view.html']);

angular.module("account.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("account.html",
    "<!-- Copyright Heddoko(TM) 2015, all rights reserved. -->\n" +
    "\n" +
    "<!-- User details -->\n" +
    "<div class=\"page\">\n" +
    "    <header>\n" +
    "        <h1>\n" +
    "            Account <i class=\"fa fa-user\"></i>\n" +
    "            <span></span>\n" +
    "        </h1>\n" +
    "    </header>\n" +
    "\n" +
    "    <div ng-hide=\"user.id === 0\" class=\"row\">\n" +
    "\n" +
    "        <!-- Avatar -->\n" +
    "        <div class=\"col-md-5 text-center\">\n" +
    "            <ui-avatar\n" +
    "                data-upload-endpoint=\"uploadAvatarEndpoint\"\n" +
    "                data-success-callback=\"uploadAvatarCallback\"\n" +
    "                data-src=\"user.avatarSrc\"\n" +
    "                data-match-width=\"{{ calculateAvatarHeight() }}\">\n" +
    "            </ui-avatar>\n" +
    "        </div>\n" +
    "\n" +
    "        <!-- User details -->\n" +
    "        <div class=\"col-md-7\">\n" +
    "            <ui-editable-fields\n" +
    "                data-id=\"userDetails\"\n" +
    "                data-model=\"user\"\n" +
    "                data-save=\"saveUserDetails\"\n" +
    "                data-save-callback=\"saveUserDetailsCallback\"\n" +
    "                data-heading=\"Your Details\">\n" +
    "\n" +
    "                <!-- Username -->\n" +
    "                <ui-editable-field\n" +
    "                    data-label=\"Username\"\n" +
    "                    data-key=\"username\"\n" +
    "                    data-disabled=\"true\">\n" +
    "                </ui-editable-field>\n" +
    "\n" +
    "                <!-- First name -->\n" +
    "                <ui-editable-field\n" +
    "                    data-label=\"First Name\"\n" +
    "                    data-key=\"firstName\"\n" +
    "                    data-required=\"true\">\n" +
    "                </ui-editable-field>\n" +
    "\n" +
    "                <!-- Last name -->\n" +
    "                <ui-editable-field\n" +
    "                    data-label=\"Last Name\"\n" +
    "                    data-key=\"lastName\">\n" +
    "                </ui-editable-field>\n" +
    "\n" +
    "                <!-- Phone -->\n" +
    "                <ui-editable-field\n" +
    "                    data-label=\"Phone #\"\n" +
    "                    data-key=\"phone\">\n" +
    "                </ui-editable-field>\n" +
    "\n" +
    "                <!-- Email -->\n" +
    "                <ui-editable-field\n" +
    "                    data-label=\"Email\"\n" +
    "                    data-key=\"email\"\n" +
    "                    data-required=\"true\">\n" +
    "                </ui-editable-field>\n" +
    "\n" +
    "            </ui-editable-fields>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "\n" +
    "    <!-- Retrieving user data -->\n" +
    "    <div ng-show=\"user.id === 0\" class=\"text-center\">\n" +
    "        <h3>Retrieving your details</h3>\n" +
    "        <br>\n" +
    "        <br>\n" +
    "        <br>\n" +
    "        <br>\n" +
    "        <i class=\"fa fa-spinner fa-spin fa-3x\"></i>\n" +
    "    </div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("configuration.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("configuration.html",
    "<!-- User details -->\n" +
    "<div class=\"page\">\n" +
    "    <header>\n" +
    "        <h1>\n" +
    "            Settings <i class=\"fa fa-cogs\"></i>\n" +
    "            <span></span>\n" +
    "        </h1>\n" +
    "    </header>\n" +
    "\n" +
    "    <div class=\"row\">\n" +
    "        <div class=\"col-md-8\">\n" +
    "\n" +
    "            <!-- Preferences -->\n" +
    "            <div class=\"panel panel-default\">\n" +
    "                <div class=\"panel-heading\">\n" +
    "                    App Settings\n" +
    "                </div>\n" +
    "\n" +
    "                <div class=\"panel-body\">\n" +
    "                    <div style=\"color: #ccc; text-align: center\">\n" +
    "                        In Development\n" +
    "                    </div>\n" +
    "                </div>\n" +
    "            </div>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "\n" +
    "    <!-- Debug -->\n" +
    "    <div ng-show=\"global.isLocal\" class=\"panel panel-default\">\n" +
    "        <div class=\"panel-heading\">Debug</div>\n" +
    "        <div class=\"panel-body\">\n" +
    "\n" +
    "        </div>\n" +
    "    </div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("dashboard.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("dashboard.html",
    "<!-- Copyright Heddoko(TM) 2015, all rights reserved. -->\n" +
    "\n" +
    "<div class=\"dashboard page animate-fade-up\">\n" +
    "\n" +
    "    <div class=\"dashboard-menu\">\n" +
    "\n" +
    "        <!-- Title -->\n" +
    "        <div class=\"row dashboard-title\">\n" +
    "            <div class=\"col-sm-12\">\n" +
    "                <h1\n" +
    "                    ng-show=\"global.state.user.id > 0 && global.state.user.firstName.length\"\n" +
    "                    class=\"text-center\">\n" +
    "                    {{ greeting }} {{ global.state.user.firstName }},\n" +
    "                </h1>\n" +
    "                <h1\n" +
    "                    ng-show=\"global.state.user.id === 0\n" +
    "                        || !global.state.user.firstName\n" +
    "                        || global.state.user.firstName.length === 0\"\n" +
    "                    class=\"text-center\">\n" +
    "                    {{ greeting }}.\n" +
    "                </h1>\n" +
    "\n" +
    "                <h3 class=\"text-center\">\n" +
    "                    What would you like to do?\n" +
    "                </h3>\n" +
    "            </div>\n" +
    "        </div>\n" +
    "\n" +
    "        <!-- Bookmarks -->\n" +
    "        <div class=\"row\">\n" +
    "\n" +
    "            <!-- Padding for bookmarks -->\n" +
    "            <div ng-show=\"bookmarks.length === 4\" class=\"col-md-2\"></div>\n" +
    "            <div ng-show=\"bookmarks.length === 3\" class=\"col-md-3\"></div>\n" +
    "            <div ng-show=\"bookmarks.length === 2\" class=\"col-md-4\"></div>\n" +
    "            <div ng-show=\"bookmarks.length === 1\" class=\"col-md-5\"></div>\n" +
    "\n" +
    "            <!-- Actual bookmarks -->\n" +
    "            <div ng-repeat=\"bookmark in bookmarks\" class=\"col-md-2\">\n" +
    "                <a href=\"{{ bookmark.uri }}\" class=\"btn-dashboard\">\n" +
    "                    <i ng-class=\"'fa fa-2x fa-'+ bookmark.icon\"></i>\n" +
    "                    <span>\n" +
    "                        {{ bookmark.title }}\n" +
    "                    </span>\n" +
    "                    <span></span>\n" +
    "                </a>\n" +
    "            </div>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("fmsresults.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("fmsresults.html",
    "<header class=\"page-main-header\">\n" +
    "    <div class=\"row\">\n" +
    "        <div class=\"col-sm-6\">\n" +
    "            <h4>FMS Results</h4>\n" +
    "            <p ng-if=\"global.state.profile.selected\">\n" +
    "                View FMS results for\n" +
    "                <b>\n" +
    "                    {{ global.state.profile.selected.first_name }} {{ global.state.profile.selected.last_name }}\n" +
    "                </b>\n" +
    "            </p>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "\n" +
    "</header>\n" +
    "<div class=\"page page-dashboard\" data-ng-controller=\"FMSFormController\">\n" +
    "\n" +
    "	<div class=\"row\">\n" +
    "		<div class=\"col-sm-6\">\n" +
    "			<div class=\"panel panel-default\">\n" +
    "				<div class=\"panel-body\">\n" +
    "					<div class=\"text-left col-sm-6\">\n" +
    "						<h3>\n" +
    "							Athlete's FMS Score History\n" +
    "						</h3>\n" +
    "					</div>\n" +
    "\n" +
    "					<div ng-show=\"global.state.profile.selected.id < 1\">\n" +
    "                        Select an athlete under 'Dashboard' in the sidebar before viewing historical FMS data\n" +
    "                    </div>\n" +
    "\n" +
    "					<div ng-hide=\"global.state.profile.selected.id < 1\" class=\"panel-body ng-scope\">\n" +
    "						<table class=\"table\">\n" +
    "							<thead>\n" +
    "								<tr>\n" +
    "									<th>#</th>\n" +
    "									<th>Date</th>\n" +
    "									<th>Final Score</th>\n" +
    "									<th>Suit no.</th>\n" +
    "									<th>Data</th>\n" +
    "								</tr>\n" +
    "							</thead>\n" +
    "							<tbody>\n" +
    "\n" +
    "								<tr\n" +
    "                                    ng-repeat=\"fmsform in global.state.profile.selected.fms_forms track by fmsform.id\"\n" +
    "                                    ng-click=\"fmsdisplay(fmsform)\">\n" +
    "\n" +
    "									<td>{{$index + 1}}</td>\n" +
    "									<td>{{fmsform.created_at}}</td>\n" +
    "									<td>\n" +
    "										{{fmsform.score}}\n" +
    "									</td>\n" +
    "									<td>1</td>\n" +
    "									<td><!--<span class=\"label label-warning\">Missing</span>--></td>\n" +
    "\n" +
    "\n" +
    "								</tr>\n" +
    "\n" +
    "							</tbody>\n" +
    "						</table>\n" +
    "					</div>\n" +
    "					<div ng-show=\"global.state.profile.selected.fms_forms.length == 0\">0 FMS Forms found</div>\n" +
    "				</div>\n" +
    "			</div>\n" +
    "		</div>\n" +
    "\n" +
    "		<div class=\"col-sm-6\">\n" +
    "			<div class=\"panel panel-default\">\n" +
    "				<div class=\"panel-body\">\n" +
    "					<div class=\"text-left col-sm-6\">\n" +
    "						<h3>\n" +
    "							FMS Details\n" +
    "						</h3>\n" +
    "					</div>\n" +
    "\n" +
    "					<div class=\"panel-body ng-scope\" ng-show=\"data.selected_fms_form == null\">\n" +
    "						select an FMS form on the left to view details\n" +
    "					</div>\n" +
    "\n" +
    "					<label ng-hide=\"data.selected_fms_form == null\" class=\"switch switch-info pull-right\">\n" +
    "                        <input type=\"checkbox\" ng-model=\"data.show_fms_edit\" class=\"ng-untouched ng-valid ng-dirty ng-valid-parse\"><i></i>\n" +
    "                    </label>\n" +
    "\n" +
    "					<div class=\"panel-body ng-scope\" ng-show=\"data.selected_fms_form != null\">\n" +
    "\n" +
    "						</br></br></br>\n" +
    "						<form class=\"form-horizontal ng-pristine ng-valid\">\n" +
    "							<fieldset ng-disabled=\"!data.show_fms_edit\">\n" +
    "								<div class=\"form-group\">\n" +
    "									<label for=\"\" class=\"col-sm-2\">Deep squat</label>\n" +
    "									<div class=\"col-sm-6\">\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"0\" ng-model=\"data.selected_fms_form.deepsquat\"><span>0</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"1\" ng-model=\"data.selected_fms_form.deepsquat\"><span>1</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"2\" ng-model=\"data.selected_fms_form.deepsquat\"><span>2</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"3\" ng-model=\"data.selected_fms_form.deepsquat\"><span>3</span></label>\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-4\">\n" +
    "											<input type=\"text\" class=\"form-control\" ng-model=\"data.selected_fms_form.deepsquatcomments\">\n" +
    "									</div>\n" +
    "								</div>\n" +
    "\n" +
    "								<div class=\"form-group\">\n" +
    "									<label for=\"\" class=\"col-sm-2\">Hurdle step L</label>\n" +
    "									<div class=\"col-sm-6\">\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"0\" ng-model=\"data.selected_fms_form.Lhurdle\"><span>0</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"1\" ng-model=\"data.selected_fms_form.Lhurdle\"><span>1</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"2\" ng-model=\"data.selected_fms_form.Lhurdle\"><span>2</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"3\" ng-model=\"data.selected_fms_form.Lhurdle\"><span>3</span></label>\n" +
    "									</div>\n" +
    "								</div>\n" +
    "\n" +
    "								<div class=\"form-group\">\n" +
    "									<label for=\"\" class=\"col-sm-2\">Hurdle step R</label>\n" +
    "									<div class=\"col-sm-6\">\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"0\" ng-model=\"data.selected_fms_form.Rhurdle\"><span>0</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"1\" ng-model=\"data.selected_fms_form.Rhurdle\"><span>1</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"2\" ng-model=\"data.selected_fms_form.Rhurdle\"><span>2</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"3\" ng-model=\"data.selected_fms_form.Rhurdle\"><span>3</span></label>\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-4\">\n" +
    "											<input type=\"text\" class=\"form-control\" ng-model=\"data.selected_fms_form.hurdlecomments\">\n" +
    "									</div>\n" +
    "								</div>\n" +
    "\n" +
    "								<div class=\"form-group\">\n" +
    "									<label for=\"\" class=\"col-sm-2\">Inline lunge step L</label>\n" +
    "									<div class=\"col-sm-6\">\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"0\" ng-model=\"data.selected_fms_form.Llunge\"><span>0</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"1\" ng-model=\"data.selected_fms_form.Llunge\"><span>1</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"2\" ng-model=\"data.selected_fms_form.Llunge\"><span>2</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"3\" ng-model=\"data.selected_fms_form.Llunge\"><span>3</span></label>\n" +
    "									</div>\n" +
    "								</div>\n" +
    "\n" +
    "								<div class=\"form-group\">\n" +
    "									<label for=\"\" class=\"col-sm-2\">Inline lunge step R</label>\n" +
    "									<div class=\"col-sm-6\">\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"0\" ng-model=\"data.selected_fms_form.Rlunge\"><span>0</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"1\" ng-model=\"data.selected_fms_form.Rlunge\"><span>1</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"2\" ng-model=\"data.selected_fms_form.Rlunge\"><span>2</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"3\" ng-model=\"data.selected_fms_form.Rlunge\"><span>3</span></label>\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-4\">\n" +
    "											<input type=\"text\" class=\"form-control\" ng-model=\"data.selected_fms_form.lungecomments\">\n" +
    "									</div>\n" +
    "								</div>\n" +
    "\n" +
    "								<div class=\"form-group\">\n" +
    "									<label for=\"\" class=\"col-sm-2\">Shoulder Mobility L</label>\n" +
    "									<div class=\"col-sm-6\">\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"0\" ng-model=\"data.selected_fms_form.Lshoulder\"><span>0</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"1\" ng-model=\"data.selected_fms_form.Lshoulder\"><span>1</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"2\" ng-model=\"data.selected_fms_form.Lshoulder\"><span>2</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"3\" ng-model=\"data.selected_fms_form.Lshoulder\"><span>3</span></label>\n" +
    "									</div>\n" +
    "								</div>\n" +
    "\n" +
    "								<div class=\"form-group\">\n" +
    "									<label for=\"\" class=\"col-sm-2\">Shoulder Mobility R</label>\n" +
    "									<div class=\"col-sm-6\">\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"0\" ng-model=\"data.selected_fms_form.Rshoulder\"><span>0</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"1\" ng-model=\"data.selected_fms_form.Rshoulder\"><span>1</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"2\" ng-model=\"data.selected_fms_form.Rshoulder\"><span>2</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"3\" ng-model=\"data.selected_fms_form.Rshoulder\"><span>3</span></label>\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-4\">\n" +
    "											<input type=\"text\" class=\"form-control\" ng-model=\"data.selected_fms_form.shouldercomments\">\n" +
    "									</div>\n" +
    "								</div>\n" +
    "\n" +
    "								<div class=\"form-group\">\n" +
    "									<label for=\"\" class=\"col-sm-2\">Impingement clearing test L</label>\n" +
    "									<div class=\"col-sm-6\">\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"0\" ng-model=\"data.selected_fms_form.Limpingement\"><span>0</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"1\" ng-model=\"data.selected_fms_form.Limpingement\"><span>1</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"2\" ng-model=\"data.selected_fms_form.Limpingement\"><span>2</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"3\" ng-model=\"data.selected_fms_form.Limpingement\"><span>3</span></label>\n" +
    "									</div>\n" +
    "								</div>\n" +
    "\n" +
    "								<div class=\"form-group\">\n" +
    "									<label for=\"\" class=\"col-sm-2\">Impingement clearing test R</label>\n" +
    "									<div class=\"col-sm-6\">\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"0\" ng-model=\"data.selected_fms_form.Rimpingement\"><span>0</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"1\" ng-model=\"data.selected_fms_form.Rimpingement\"><span>1</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"2\" ng-model=\"data.selected_fms_form.Rimpingement\"><span>2</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"3\" ng-model=\"data.selected_fms_form.Rimpingement\"><span>3</span></label>\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-4\">\n" +
    "											<input type=\"text\" class=\"form-control\" ng-model=\"data.selected_fms_form.impingementcomments\">\n" +
    "									</div>\n" +
    "								</div>\n" +
    "\n" +
    "								<div class=\"form-group\">\n" +
    "									<label for=\"\" class=\"col-sm-2\">Active straight-leg raise L</label>\n" +
    "									<div class=\"col-sm-6\">\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"0\" ng-model=\"data.selected_fms_form.Lactive\"><span>0</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"1\" ng-model=\"data.selected_fms_form.Lactive\"><span>1</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"2\" ng-model=\"data.selected_fms_form.Lactive\"><span>2</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"3\" ng-model=\"data.selected_fms_form.Lactive\"><span>3</span></label>\n" +
    "									</div>\n" +
    "								</div>\n" +
    "\n" +
    "								<div class=\"form-group\">\n" +
    "									<label for=\"\" class=\"col-sm-2\">Active straight-leg raise R</label>\n" +
    "									<div class=\"col-sm-6\">\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"0\" ng-model=\"data.selected_fms_form.Ractive\"><span>0</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"1\" ng-model=\"data.selected_fms_form.Ractive\"><span>1</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"2\" ng-model=\"data.selected_fms_form.Ractive\"><span>2</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"3\" ng-model=\"data.selected_fms_form.Ractive\"><span>3</span></label>\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-4\">\n" +
    "											<input type=\"text\" class=\"form-control\" ng-model=\"data.selected_fms_form.activecomments\">\n" +
    "									</div>\n" +
    "								</div>\n" +
    "\n" +
    "								<div class=\"form-group\">\n" +
    "									<label for=\"\" class=\"col-sm-2\">Trunk stability pushup</label>\n" +
    "									<div class=\"col-sm-6\">\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"0\" ng-model=\"data.selected_fms_form.trunk\"><span>0</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"1\" ng-model=\"data.selected_fms_form.trunk\"><span>1</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"2\" ng-model=\"data.selected_fms_form.trunk\"><span>2</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"3\" ng-model=\"data.selected_fms_form.trunk\"><span>3</span></label>\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-4\">\n" +
    "											<input type=\"text\" class=\"form-control\" ng-model=\"data.selected_fms_form.trunkcomments\">\n" +
    "									</div>\n" +
    "								</div>\n" +
    "\n" +
    "								<div class=\"form-group\">\n" +
    "									<label for=\"\" class=\"col-sm-2\">Press-up clearing test</label>\n" +
    "									<div class=\"col-sm-6\">\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"0\" ng-model=\"data.selected_fms_form.press\"><span>0</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"1\" ng-model=\"data.selected_fms_form.press\"><span>1</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"2\" ng-model=\"data.selected_fms_form.press\"><span>2</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"3\" ng-model=\"data.selected_fms_form.press\"><span>3</span></label>\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-4\">\n" +
    "											<input type=\"text\" class=\"form-control\" ng-model=\"data.selected_fms_form.presscomments\">\n" +
    "									</div>\n" +
    "								</div>\n" +
    "\n" +
    "								<div class=\"form-group\">\n" +
    "									<label for=\"\" class=\"col-sm-2\">Rotary stability L</label>\n" +
    "									<div class=\"col-sm-6\">\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"0\" ng-model=\"data.selected_fms_form.Lrotary\"><span>0</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"1\" ng-model=\"data.selected_fms_form.Lrotary\"><span>1</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"2\" ng-model=\"data.selected_fms_form.Lrotary\"><span>2</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"3\" ng-model=\"data.selected_fms_form.Lrotary\"><span>3</span></label>\n" +
    "									</div>\n" +
    "								</div>\n" +
    "\n" +
    "								<div class=\"form-group\">\n" +
    "									<label for=\"\" class=\"col-sm-2\">Rotary stability R</label>\n" +
    "									<div class=\"col-sm-6\">\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"0\" ng-model=\"data.selected_fms_form.Rrotary\"><span>0</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"1\" ng-model=\"data.selected_fms_form.Rrotary\"><span>1</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"2\" ng-model=\"data.selected_fms_form.Rrotary\"><span>2</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"3\" ng-model=\"data.selected_fms_form.Rrotary\"><span>3</span></label>\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-4\">\n" +
    "											<input type=\"text\" class=\"form-control\" ng-model=\"data.selected_fms_form.rotarycomments\">\n" +
    "									</div>\n" +
    "								</div>\n" +
    "\n" +
    "								<div class=\"form-group\">\n" +
    "									<label for=\"\" class=\"col-sm-2\">Posterior rocking clearing test</label>\n" +
    "									<div class=\"col-sm-6\">\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"0\" ng-model=\"data.selected_fms_form.posterior\"><span>0</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"1\" ng-model=\"data.selected_fms_form.posterior\"><span>1</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"2\" ng-model=\"data.selected_fms_form.posterior\"><span>2</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"3\" ng-model=\"data.selected_fms_form.posterior\"><span>3</span></label>\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-4\">\n" +
    "											<input type=\"text\" class=\"form-control\" ng-model=\"data.selected_fms_form.posteriorcomments\">\n" +
    "									</div>\n" +
    "								</div>\n" +
    "							</fieldset>\n" +
    "\n" +
    "            </form>\n" +
    "\n" +
    "						<div ng-hide=\"!data.show_fms_edit\">\n" +
    "							<button type=\"button\" class=\"btn btn-info btn-sm disabled\" ng-click=\"updateFMS();\">Update this FMS Form</button>\n" +
    "						</div>\n" +
    "\n" +
    "\n" +
    "					</div>\n" +
    "				</div>\n" +
    "			</div>\n" +
    "		</div>\n" +
    "\n" +
    "	</div>\n" +
    "\n" +
    "\n" +
    "\n" +
    "\n" +
    "\n" +
    "\n" +
    "\n" +
    "\n" +
    "\n" +
    "\n" +
    "</div>\n" +
    "");
}]);

angular.module("fmstest.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("fmstest.html",
    "<header class=\"page-main-header\">\n" +
    "    <div class=\"row\">\n" +
    "        <div class=\"col-sm-6\">\n" +
    "            <h4>Submit FMS Results</h4>\n" +
    "            <p ng-if=\"data.member.selected\">\n" +
    "                Submit FMS results for\n" +
    "                <b>\n" +
    "                    {{ global.state.profile.selected.first_name }} {{ global.state.profile.selected.last_name }}\n" +
    "                </b>\n" +
    "            </p>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "</header>\n" +
    "\n" +
    "<div class=\"page page-dashboard\" data-ng-controller=\"FMSFormController\">\n" +
    "\n" +
    "	<section>\n" +
    "		<div class=\"row\">\n" +
    "\n" +
    "			<div class=\"col-sm-12\" ng-hide=\"waiting_server_response\">\n" +
    "				<div class=\"panel panel-default\">\n" +
    "					<div class=\"panel-body\">\n" +
    "						<div class=\"text-left col-sm-6\">\n" +
    "							<h3>\n" +
    "								Add FMS Test Scores\n" +
    "							</h3>\n" +
    "						</div>\n" +
    "\n" +
    "						<div class=\"panel-body col-sm-12 ng-scope\">\n" +
    "							<form ng-submit=\"submitFMSForm()\" ng-hide=\"data.member.selected.id < 1\">\n" +
    "								<div class=\"row\">\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										Deep Squat\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"0\" ng-model=\"data.fms_form_data.deepsquat\"><span>0</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"1\" ng-model=\"data.fms_form_data.deepsquat\"><span>1</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"2\" ng-model=\"data.fms_form_data.deepsquat\"><span>2</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"3\" ng-model=\"data.fms_form_data.deepsquat\"><span>3</span></label>\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										<input placeholder=\"Comments\" type=\"text\" ng-model=\"data.fms_form_data.deepsquatcomments\">\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										<span ng-class=\"{btn: true, 'btn-file': true, 'btn-default': files.ds == null, 'btn-success': files.ds != null}\">\n" +
    "                                            Select Movement File <input type=\"file\" file-model=\"files.ds\">\n" +
    "                                        </span>\n" +
    "									</div>\n" +
    "								</div>\n" +
    "\n" +
    "                                <br>\n" +
    "\n" +
    "								<div class=\"row\">\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										Hurdle step (L)\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"0\" ng-model=\"data.fms_form_data.Lhurdle\"><span>0</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"1\" ng-model=\"data.fms_form_data.Lhurdle\"><span>1</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"2\" ng-model=\"data.fms_form_data.Lhurdle\"><span>2</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"3\" ng-model=\"data.fms_form_data.Lhurdle\"><span>3</span></label>\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "                                        <input placeholder=\"Comments\" type=\"text\" ng-model=\"data.fms_form_data.Lhurdlecomments\">\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										<span ng-class=\"{btn: true, 'btn-file': true, 'btn-default': files.hsl == null, 'btn-success': files.hsl != null}\">\n" +
    "                                            Select Movement File <input type=\"file\" file-model=\"files.hsl\">\n" +
    "                                        </span>\n" +
    "									</div>\n" +
    "								</div>\n" +
    "\n" +
    "                                <br>\n" +
    "\n" +
    "								<div class=\"row\">\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										Hurdle step (R)\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"0\" ng-model=\"data.fms_form_data.Rhurdle\"><span>0</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"1\" ng-model=\"data.fms_form_data.Rhurdle\"><span>1</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"2\" ng-model=\"data.fms_form_data.Rhurdle\"><span>2</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"3\" ng-model=\"data.fms_form_data.Rhurdle\"><span>3</span></label>\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										<input placeholder=\"Comments\" type=\"text\" ng-model=\"data.fms_form_data.Rhurdlecomments\">\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										<span ng-class=\"{btn: true, 'btn-file': true, 'btn-default': files.hsr == null, 'btn-success': files.hsr != null}\">\n" +
    "                                            Select Movement File <input type=\"file\" file-model=\"files.hsr\">\n" +
    "                                        </span>\n" +
    "									</div>\n" +
    "								</div>\n" +
    "\n" +
    "                                <br>\n" +
    "\n" +
    "								<div class=\"row\">\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										Inline lunge (L)\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"0\" ng-model=\"data.fms_form_data.Llunge\"><span>0</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"1\" ng-model=\"data.fms_form_data.Llunge\"><span>1</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"2\" ng-model=\"data.fms_form_data.Llunge\"><span>2</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"3\" ng-model=\"data.fms_form_data.Llunge\"><span>3</span></label>\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "                                        <input placeholder=\"Comments\" type=\"text\" ng-model=\"data.fms_form_data.Llungecomments\">\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										<span ng-class=\"{btn: true, 'btn-file': true, 'btn-default': files.ill == null, 'btn-success': files.ill != null}\">\n" +
    "                                            Select Movement File <input type=\"file\" file-model=\"files.ill\">\n" +
    "                                        </span>\n" +
    "									</div>\n" +
    "								</div>\n" +
    "\n" +
    "                                <br>\n" +
    "\n" +
    "								<div class=\"row\">\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										Inline lunge (R)\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"0\" ng-model=\"data.fms_form_data.Rlunge\"><span>0</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"1\" ng-model=\"data.fms_form_data.Rlunge\"><span>1</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"2\" ng-model=\"data.fms_form_data.Rlunge\"><span>2</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"3\" ng-model=\"data.fms_form_data.Rlunge\"><span>3</span></label>\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										<input placeholder=\"Comments\" type=\"text\" ng-model=\"data.fms_form_data.Rlungecomments\">\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										<span ng-class=\"{btn: true, 'btn-file': true, 'btn-default': files.ilr == null, 'btn-success': files.ilr != null}\">\n" +
    "                                            Select Movement File <input type=\"file\" file-model=\"files.ilr\">\n" +
    "                                        </span>\n" +
    "									</div>\n" +
    "								</div>\n" +
    "\n" +
    "                                <br>\n" +
    "\n" +
    "								<div class=\"row\">\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										Shoulder Mobility (L)\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"0\" ng-model=\"data.fms_form_data.Lshoulder\"><span>0</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"1\" ng-model=\"data.fms_form_data.Lshoulder\"><span>1</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"2\" ng-model=\"data.fms_form_data.Lshoulder\"><span>2</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"3\" ng-model=\"data.fms_form_data.Lshoulder\"><span>3</span></label>\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "                                        <input placeholder=\"Comments\" type=\"text\" ng-model=\"data.fms_form_data.Lshouldercomments\">\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										<span ng-class=\"{btn: true, 'btn-file': true, 'btn-default': files.sml == null, 'btn-success': files.sml != null}\">\n" +
    "    Select Movement File <input type=\"file\" file-model=\"files.sml\">\n" +
    "</span>\n" +
    "									</div>\n" +
    "								</div>\n" +
    "\n" +
    "                                <br>\n" +
    "\n" +
    "								<div class=\"row\">\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										Shoulder Mobility (R)\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"0\" ng-model=\"data.fms_form_data.Rshoulder\"><span>0</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"1\" ng-model=\"data.fms_form_data.Rshoulder\"><span>1</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"2\" ng-model=\"data.fms_form_data.Rshoulder\"><span>2</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"3\" ng-model=\"data.fms_form_data.Rshoulder\"><span>3</span></label>\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										<input placeholder=\"Comments\" type=\"text\" ng-model=\"data.fms_form_data.Rshouldercomments\">\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										<span ng-class=\"{btn: true, 'btn-file': true, 'btn-default': files.smr == null, 'btn-success': files.smr != null}\">\n" +
    "    Select Movement File <input type=\"file\" file-model=\"files.smr\">\n" +
    "</span>\n" +
    "									</div>\n" +
    "								</div>\n" +
    "\n" +
    "                                <br>\n" +
    "\n" +
    "								<div class=\"row\">\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										Impingement clearing test (L)\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										<label class=\"ui-checkbox\"><input ng-model=\"data.fms_form_data.Limpingement\" type=\"checkbox\"><span>pain</span></label>\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "                                        <input placeholder=\"Comments\" type=\"text\" ng-model=\"data.fms_form_data.Limpingementcomments\">\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										<span ng-class=\"{btn: true, 'btn-file': true, 'btn-default': files.ictl == null, 'btn-success': files.ictl != null}\">\n" +
    "    Select Movement File <input type=\"file\" file-model=\"files.ictl\">\n" +
    "</span>\n" +
    "									</div>\n" +
    "								</div>\n" +
    "\n" +
    "                                <br>\n" +
    "\n" +
    "								<div class=\"row\">\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										Impingement clearing test (R)\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										<label class=\"ui-checkbox\"><input ng-model=\"data.fms_form_data.Rimpingement\" type=\"checkbox\"><span>pain</span></label>\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										<input placeholder=\"Comments\" type=\"text\" ng-model=\"data.fms_form_data.impingementcomments\">\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										<span ng-class=\"{btn: true, 'btn-file': true, 'btn-default': files.ictr == null, 'btn-success': files.ictr != null}\">\n" +
    "    Select Movement File <input type=\"file\" file-model=\"files.ictr\">\n" +
    "</span>\n" +
    "									</div>\n" +
    "								</div>\n" +
    "\n" +
    "                                <br>\n" +
    "\n" +
    "								<div class=\"row\">\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										Active straight-leg raise (L)\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"0\" ng-model=\"data.fms_form_data.Lactive\"><span>0</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"1\" ng-model=\"data.fms_form_data.Lactive\"><span>1</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"2\" ng-model=\"data.fms_form_data.Lactive\"><span>2</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"3\" ng-model=\"data.fms_form_data.Lactive\"><span>3</span></label>\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "                                        <input placeholder=\"Comments\" type=\"text\" ng-model=\"data.fms_form_data.Lactivecomments\">\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										<span ng-class=\"{btn: true, 'btn-file': true, 'btn-default': files.aslrl == null, 'btn-success': files.aslrl != null}\">\n" +
    "    Select Movement File <input type=\"file\" file-model=\"files.aslrl\">\n" +
    "</span>\n" +
    "									</div>\n" +
    "								</div>\n" +
    "\n" +
    "                                <br>\n" +
    "\n" +
    "								<div class=\"row\">\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										Active straight-leg raise (R)\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"0\" ng-model=\"data.fms_form_data.Ractive\"><span>0</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"1\" ng-model=\"data.fms_form_data.Ractive\"><span>1</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"2\" ng-model=\"data.fms_form_data.Ractive\"><span>2</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"3\" ng-model=\"data.fms_form_data.Ractive\"><span>3</span></label>\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										<input placeholder=\"Comments\" type=\"text\" ng-model=\"data.fms_form_data.Ractivecomments\">\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										<span ng-class=\"{btn: true, 'btn-file': true, 'btn-default': files.aslrr == null, 'btn-success': files.aslrr != null}\">\n" +
    "                                            Select Movement File <input type=\"file\" file-model=\"files.aslrr\">\n" +
    "                                        </span>\n" +
    "									</div>\n" +
    "								</div>\n" +
    "\n" +
    "                                <br>\n" +
    "\n" +
    "								<div class=\"row\">\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										Trunk stability push-up\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"0\" ng-model=\"data.fms_form_data.trunk\"><span>0</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"1\" ng-model=\"data.fms_form_data.trunk\"><span>1</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"2\" ng-model=\"data.fms_form_data.trunk\"><span>2</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"3\" ng-model=\"data.fms_form_data.trunk\"><span>3</span></label>\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										<input placeholder=\"Comments\" type=\"text\" ng-model=\"data.fms_form_data.trunkcomments\">\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										<span ng-class=\"{btn: true, 'btn-file': true, 'btn-default': files.tspu == null, 'btn-success': files.tspu != null}\">\n" +
    "                                            Select Movement File <input type=\"file\" file-model=\"files.tspu\">\n" +
    "                                        </span>\n" +
    "									</div>\n" +
    "								</div>\n" +
    "\n" +
    "                                <br>\n" +
    "\n" +
    "								<div class=\"row\">\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										Press-up clearing test\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										<label class=\"ui-checkbox\"><input ng-model=\"data.fms_form_data.press\" type=\"checkbox\"><span>pain</span></label>\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										<input placeholder=\"Comments\" type=\"text\" ng-model=\"data.fms_form_data.presscomments\">\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										<span ng-class=\"{btn: true, 'btn-file': true, 'btn-default': files.puct == null, 'btn-success': files.puct != null}\">\n" +
    "                                            Select Movement File <input type=\"file\" file-model=\"files.puct\">\n" +
    "                                        </span>\n" +
    "									</div>\n" +
    "								</div>\n" +
    "\n" +
    "                                <br>\n" +
    "\n" +
    "								<div class=\"row\">\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										Rotary stability (L)\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"0\" ng-model=\"data.fms_form_data.Lrotary\"><span>0</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"1\" ng-model=\"data.fms_form_data.Lrotary\"><span>1</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"2\" ng-model=\"data.fms_form_data.Lrotary\"><span>2</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"3\" ng-model=\"data.fms_form_data.Lrotary\"><span>3</span></label>\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "                                        <input placeholder=\"Comments\" type=\"text\" ng-model=\"data.fms_form_data.Lrotarycomments\">\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										<span ng-class=\"{btn: true, 'btn-file': true, 'btn-default': files.rsl == null, 'btn-success': files.rsl != null}\">\n" +
    "                                            Select Movement File <input type=\"file\" file-model=\"files.rsl\">\n" +
    "                                        </span>\n" +
    "									</div>\n" +
    "								</div>\n" +
    "\n" +
    "                                <br>\n" +
    "\n" +
    "								<div class=\"row\">\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										Rotary stability (R)\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"0\" ng-model=\"data.fms_form_data.Rrotary\"><span>0</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"1\" ng-model=\"data.fms_form_data.Rrotary\"><span>1</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"2\" ng-model=\"data.fms_form_data.Rrotary\"><span>2</span></label>\n" +
    "										<label class=\"ui-radio\"><input type=\"radio\" value=\"3\" ng-model=\"data.fms_form_data.Rrotary\"><span>3</span></label>\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										<input placeholder=\"Comments\" type=\"text\" ng-model=\"data.fms_form_data.Rrotarycomments\">\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										<span ng-class=\"{btn: true, 'btn-file': true, 'btn-default': files.rsr == null, 'btn-success': files.rsr != null}\">\n" +
    "    Select Movement File <input type=\"file\" file-model=\"files.rsr\">\n" +
    "</span>\n" +
    "									</div>\n" +
    "								</div>\n" +
    "\n" +
    "                                <br>\n" +
    "\n" +
    "								<div class=\"row\">\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										Posterior rocking clearing test\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										<label class=\"ui-checkbox\"><input ng-model=\"data.fms_form_data.posterior\" type=\"checkbox\"><span>pain</span></label>\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										<input placeholder=\"Comments\" type=\"text\" ng-model=\"data.fms_form_data.posteriorcomments\">\n" +
    "									</div>\n" +
    "									<div class=\"col-sm-3\">\n" +
    "										<span ng-class=\"{btn: true, 'btn-file': true, 'btn-default': files.prct == null, 'btn-success': files.prct != null}\">\n" +
    "                                            Select Movement File <input type=\"file\" file-model=\"files.prct\">\n" +
    "                                        </span>\n" +
    "									</div>\n" +
    "								</div>\n" +
    "								<div class=\"row\">\n" +
    "									<div class=\"col-sm-12\">\n" +
    "										<h3>Other Comments</h3>\n" +
    "										<textarea name=\"\" id=\"\" class=\"form-control\" ng-model=\"data.fms_form_data.comment\"></textarea>\n" +
    "										<br>\n" +
    "\n" +
    "										<div class=\"text-center\">\n" +
    "											<button type=\"submit\" class=\"btn btn-primary\">Submit Results and Movement Files</button>\n" +
    "										</div>\n" +
    "									</div>\n" +
    "								</div>\n" +
    "							</form>\n" +
    "						</div>\n" +
    "					</div>\n" +
    "				</div>\n" +
    "			</div>\n" +
    "		</div>\n" +
    "	</section>\n" +
    "</div>\n" +
    "");
}]);

angular.module("placeholder.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("placeholder.html",
    "<!-- Temporary page -->\n" +
    "<div class=\"col-sm-12\">\n" +
    "    <h1 style=\"text-align: center; color: #ccc;\">\n" +
    "        In Development\n" +
    "    </h1>\n" +
    "</div>\n" +
    "");
}]);

angular.module("admin/dirPagination.tpl.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("admin/dirPagination.tpl.html",
    "<ul class=\"pagination\" ng-if=\"1 < pages.length || !autoHide\">\n" +
    "    <li ng-if=\"boundaryLinks\" ng-class=\"{ disabled : pagination.current == 1 }\">\n" +
    "        <a href=\"\" ng-click=\"setCurrent(1)\">&laquo;</a>\n" +
    "    </li>\n" +
    "    <li ng-if=\"directionLinks\" ng-class=\"{ disabled : pagination.current == 1 }\">\n" +
    "        <a href=\"\" ng-click=\"setCurrent(pagination.current - 1)\">&lsaquo;</a>\n" +
    "    </li>\n" +
    "    <li ng-repeat=\"pageNumber in pages track by $index\" ng-class=\"{ active : pagination.current == pageNumber, disabled : pageNumber == '...' }\">\n" +
    "        <a href=\"\" ng-click=\"setCurrent(pageNumber)\">{{ pageNumber }}</a>\n" +
    "    </li>\n" +
    "\n" +
    "    <li ng-if=\"directionLinks\" ng-class=\"{ disabled : pagination.current == pagination.last }\">\n" +
    "        <a href=\"\" ng-click=\"setCurrent(pagination.current + 1)\">&rsaquo;</a>\n" +
    "    </li>\n" +
    "    <li ng-if=\"boundaryLinks\"  ng-class=\"{ disabled : pagination.current == pagination.last }\">\n" +
    "        <a href=\"\" ng-click=\"setCurrent(pagination.last)\">&raquo;</a>\n" +
    "    </li>\n" +
    "</ul>");
}]);

angular.module("capture-demo/index.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("capture-demo/index.html",
    "<div class=\"page demo-analysis-page\">\n" +
    "    <header>\n" +
    "        <h1>\n" +
    "            Capture <i class=\"fa fa-video-camera\"></i>\n" +
    "            <span></span>\n" +
    "        </h1>\n" +
    "    </header>\n" +
    "\n" +
    "    <div class=\"row\">\n" +
    "        <div class=\"col-sm-9 col-sm-offset-3\">\n" +
    "            <h3>\n" +
    "                <small>Current Movement:</small> {{ movements[currentMovementIndex].name }}\n" +
    "            </h3>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "    <br>\n" +
    "\n" +
    "    <div class=\"row\">\n" +
    "\n" +
    "        <div class=\"col-sm-12\">\n" +
    "\n" +
    "            <!-- FMS List -->\n" +
    "            <div class=\"col-sm-3\">\n" +
    "                <div class=\"panel panel-default\">\n" +
    "                    <div class=\"panel-heading\">\n" +
    "                        Movements To Test\n" +
    "                    </div>\n" +
    "\n" +
    "                    <div class=\"panel-body\">\n" +
    "                        <div\n" +
    "                            ng-repeat=\"movement in movements\"\n" +
    "                            class=\"demo-movement-li\"\n" +
    "                            ng-class=\"{live: movement.current, done: movement.done}\">\n" +
    "\n" +
    "                            {{ $index }}. {{ movement.name }}\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "                </div>\n" +
    "            </div>\n" +
    "\n" +
    "            <!-- Emphasized view -->\n" +
    "            <div class=\"col-sm-6\" id=\"emphasizedViewContainer\">\n" +
    "                <div class=\"panel panel-default\">\n" +
    "                    <div class=\"panel-heading text-right\">\n" +
    "                        Side/Sagittal\n" +
    "                    </div>\n" +
    "\n" +
    "                    <div class=\"panel-body\">\n" +
    "                        <video id=\"sagittalVideoCapture\" class=\"demo-analysis\">\n" +
    "                            <source ng-src=\"../../../../demo/aslr/left/sagittal.webm\" type=\"video/webm\" />\n" +
    "                            <source src=\"../../../../demo/aslr/left/sagittal.mp4\" type=\"video/mp4\" />\n" +
    "                            Sorry, your browser doesn't support HTML5 video.\n" +
    "                        </video>\n" +
    "                    </div>\n" +
    "                </div>\n" +
    "            </div>\n" +
    "\n" +
    "            <!-- Secondary views -->\n" +
    "            <div class=\"col-sm-3\" id=\"secondaryViewsContainer\">\n" +
    "\n" +
    "                <!-- Coronal view -->\n" +
    "                <div class=\"panel panel-default\">\n" +
    "                    <div class=\"panel-heading text-right\">\n" +
    "                        Front/Coronal\n" +
    "                    </div>\n" +
    "\n" +
    "                    <div class=\"panel-body\">\n" +
    "                        <video id=\"coronalVideoCapture\" class=\"demo-analysis\">\n" +
    "                            <source ng-src=\"{{'../../../../demo/aslr/left/coronal.webm'}}\" type=\"video/webm\" />\n" +
    "                            <source src=\"{{'../../../../demo/aslr/left/coronal.mp4'}}\" type=\"video/mp4\" />\n" +
    "                            Sorry, your browser doesn't support HTML5 video.\n" +
    "                        </video>\n" +
    "                    </div>\n" +
    "                </div>\n" +
    "\n" +
    "                <!-- Transverse view -->\n" +
    "                <div class=\"panel panel-default\">\n" +
    "                    <div class=\"panel-heading text-right\">\n" +
    "                        Horizontal/Transverse\n" +
    "                    </div>\n" +
    "\n" +
    "                    <div class=\"panel-body\">\n" +
    "                        <video id=\"transverseVideoCapture\" class=\"demo-analysis\">\n" +
    "                            <source ng-src=\"{{'../../../../demo/aslr/left/transverse.webm'}}\" type=\"video/webm\" />\n" +
    "                            <source src=\"{{'../../../../demo/aslr/left/transverse.mp4'}}\" type=\"video/mp4\" />\n" +
    "                            Sorry, your browser doesn't support HTML5 video.\n" +
    "                        </video>\n" +
    "                    </div>\n" +
    "                </div>\n" +
    "            </div>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "\n" +
    "    <!-- Record controls -->\n" +
    "    <div class=\"row\">\n" +
    "        <div class=\"col-sm-12 text-center\">\n" +
    "            <button\n" +
    "                ng-hide=\"isPlaying\"\n" +
    "                ng-click=\"start()\"\n" +
    "                class=\"btn btn-danger btn-circle btn-lg\">\n" +
    "\n" +
    "                <i class=\"fa fa-circle\"></i>\n" +
    "            </button>\n" +
    "            <button\n" +
    "                ng-show=\"isPlaying\"\n" +
    "                ng-click=\"done()\"\n" +
    "                class=\"btn btn-default btn-circle btn-lg\">\n" +
    "\n" +
    "                <i class=\"fa fa-stop\"></i>\n" +
    "            </button>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("capture/index.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("capture/index.html",
    "<div class=\"page\">\n" +
    "    <header>\n" +
    "        <h1>\n" +
    "            Record <i class=\"fa fa-video-camera\"></i>\n" +
    "            <span></span>\n" +
    "        </h1>\n" +
    "    </header>\n" +
    "\n" +
    "    <h2 style=\"text-align: center; color: #ccc;\">\n" +
    "        In Development\n" +
    "    </h2>\n" +
    "</div>\n" +
    "");
}]);

angular.module("demo/trends/index.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("demo/trends/index.html",
    "<!-- Copyright Heddoko(TM) 2015, all rights reserved. -->\n" +
    "\n" +
    "<div class=\"page\">\n" +
    "    <header>\n" +
    "        <h1>\n" +
    "            Rehabilitation <i class=\"fa fa-medkit\"></i>\n" +
    "            <span></span>\n" +
    "        </h1>\n" +
    "    </header>\n" +
    "\n" +
    "    <!-- Bar chart -->\n" +
    "    <div class=\"row demo-trends-section\">\n" +
    "        <div class=\"col-xs-12 col-md-7 col-md-offset-5 col-lg-9 col-lg-offset-3\">\n" +
    "            <h3 ng-show=\"metric\" class=\"text-center\">\n" +
    "                <span ng-show=\"metric.title == 'Shoulder Range of Motion'\">\n" +
    "                    {{ selectizeRomModel == 1 ? 'Passive ' : 'Active ' }}\n" +
    "                </span>\n" +
    "                {{ metric.title }} / Dec 19 - Feb 6 2016\n" +
    "            </h3>\n" +
    "        </div>\n" +
    "\n" +
    "\n" +
    "        <!-- DEMO: Left Panel -->\n" +
    "        <div class=\"col-xs-12 col-md-5 col-lg-3 demo-trends-info-2\">\n" +
    "\n" +
    "            <!-- Athlete details -->\n" +
    "            <div class=\"row\">\n" +
    "                <div class=\"col-md-4\">\n" +
    "                    <a\n" +
    "                        ng-style=\"{'background-image': 'url('+ profile.avatarSrc +')'}\"\n" +
    "                        style=\"display: block; background: transparent center center no-repeat; background-size: contain;\"\n" +
    "                        href=\"#/profiles/{{ profile.id }}\"\n" +
    "                        class=\"aspect-ratio aspect-square\">\n" +
    "                    </a>\n" +
    "                </div>\n" +
    "\n" +
    "                <div class=\"col-md-8\">\n" +
    "                    <small>Date:</small>\n" +
    "                    <b>Feb 6, 2016</b>\n" +
    "                    <br>\n" +
    "\n" +
    "                    <small>Rehab Start:</small>\n" +
    "                    <b>Dec 19, 2016</b>\n" +
    "                    <br>\n" +
    "\n" +
    "                    <small>Athlete: </small>\n" +
    "                    <b>\n" +
    "                        <a href=\"#/profiles/{{ profile.id }}\">\n" +
    "                            {{ profile.firstName + ' ' + profile.lastName }}\n" +
    "                        </a>\n" +
    "                    </b>\n" +
    "                    <br>\n" +
    "\n" +
    "                    <small>Team: </small>\n" +
    "                    <b>\n" +
    "                        {{ global.getSelectedGroup().name }}\n" +
    "                    </b>\n" +
    "                    <br>\n" +
    "\n" +
    "                    <!-- Metric -->\n" +
    "                    <div ng-show=\"metric\">\n" +
    "                        <selectize\n" +
    "                            ng-model=\"selectizeMetricModel\"\n" +
    "                            class=\"form-control text-center\"\n" +
    "                            config=\"selectizeMetricConfig\"\n" +
    "                            options=\"selectizeMetricOptions\"\n" +
    "                            placeholder=\"Select a metric.\">\n" +
    "                        </selectize>\n" +
    "                    </div>\n" +
    "\n" +
    "                    <!-- Active/Passive selector -->\n" +
    "                    <div ng-show=\"metric.title == 'Shoulder Range of Motion'\">\n" +
    "                        <selectize\n" +
    "                            ng-model=\"selectizeRomModel\"\n" +
    "                            class=\"form-control text-center\"\n" +
    "                            data-config=\"selectizeRomConfig\"\n" +
    "                            data-options=\"selectizeRomOptions\">\n" +
    "                        </selectize>\n" +
    "                    </div>\n" +
    "                </div>\n" +
    "                <br>\n" +
    "            </div>\n" +
    "\n" +
    "            <!-- Recovery percent for Peak Elbow Angular Velocity -->\n" +
    "            <div\n" +
    "                ng-show=\"metric.title == 'Peak Elbow Angular Velocity' && !isFetchingData\"\n" +
    "                class=\"row text-center\"\n" +
    "                style=\"color: #ddd;\">\n" +
    "\n" +
    "                <br>\n" +
    "                <h3>\n" +
    "                    Percent Recovery\n" +
    "                </h3>\n" +
    "\n" +
    "                <div easypiechart options=\"easypie.options\" percent=\"easypie.percent\" class=\"easypiechart\">\n" +
    "                    <span class=\"pie-percent\" ng-bind=\"easypie.percent\"></span>\n" +
    "                </div>\n" +
    "                <br>\n" +
    "            </div>\n" +
    "\n" +
    "            <!-- Threshold input for Peak Elbow Angular Velocity -->\n" +
    "            <div\n" +
    "                ng-show=\"metric.title == 'Peak Elbow Angular Velocity' && !isFetchingData\"\n" +
    "                class=\"row\">\n" +
    "\n" +
    "                <br>\n" +
    "                <h3 class=\"text-center\" style=\"color: #ddd;\">\n" +
    "                    Threshold for Return To Play\n" +
    "                </h3>\n" +
    "                <br>\n" +
    "\n" +
    "                <div class=\"col-md-12 col-lg-8 col-lg-offset-2\">\n" +
    "                    <div class=\"input-group\">\n" +
    "\n" +
    "                        <!-- Decrease -->\n" +
    "                        <span class=\"input-group-btn\">\n" +
    "                            <button ng-click=\"decreaseThreshold()\" class=\"btn btn-default\" type=\"button\">\n" +
    "                                <i class=\"fa fa-minus\"></i>\n" +
    "                            </button>\n" +
    "                        </span>\n" +
    "\n" +
    "                        <!-- Edit -->\n" +
    "                        <input ng-model=\"thresholdValue\" type=\"text\" class=\"form-control text-center\" style=\"font-size: 1.2em\">\n" +
    "\n" +
    "                        <!-- Increase -->\n" +
    "                        <span class=\"input-group-btn\">\n" +
    "                            <button ng-click=\"increaseThreshold()\" class=\"btn btn-default\" type=\"button\">\n" +
    "                                <i class=\"fa fa-plus\"></i>\n" +
    "                            </button>\n" +
    "                        </span>\n" +
    "                    </div>\n" +
    "                </div>\n" +
    "            </div>\n" +
    "\n" +
    "            <!-- ROM percent -->\n" +
    "            <div\n" +
    "                ng-show=\"false && metric.title == 'Shoulder Range of Motion' && !isFetchingData\"\n" +
    "                class=\"row\">\n" +
    "\n" +
    "                <div class=\"col-md-12 text-center\">\n" +
    "                    <span id=\"romPercent\"></span>\n" +
    "                </div>\n" +
    "            </div>\n" +
    "\n" +
    "            <!-- ROM gauge -->\n" +
    "            <div\n" +
    "                ng-show=\"metric.title == 'Shoulder Range of Motion' && !isFetchingData\"\n" +
    "                class=\"row\">\n" +
    "\n" +
    "                <br>\n" +
    "                <div class=\"col-md-8 col-md-offset-2 text-center\">\n" +
    "                    <canvas\n" +
    "                        data-theme-gauge-chart\n" +
    "                        data-gauge-data=\"romGauge.data\"\n" +
    "                        data-live-gauge-value=\"romGaugeValue\"\n" +
    "                        data-gauge-options=\"romGauge.options\"\n" +
    "                        style=\"width: 100%;\">\n" +
    "                    </canvas>\n" +
    "\n" +
    "                    <div style=\"position: absolute; left: 42%; bottom: 15px; font-size: 1.2em\">\n" +
    "                        {{ romGaugeValuePercent }}\n" +
    "                    </div>\n" +
    "                </div>\n" +
    "            </div>\n" +
    "        </div>\n" +
    "\n" +
    "\n" +
    "        <!-- Peak Elbow Angular Velocity -->\n" +
    "        <div\n" +
    "            ng-show=\"metric.title == 'Peak Elbow Angular Velocity' && !isFetchingData\"\n" +
    "            class=\"col-xs-12 col-md-7 col-lg-9\">\n" +
    "\n" +
    "            <div\n" +
    "                data-theme-flot-chart\n" +
    "                data-data=\"flotData\"\n" +
    "                data-options=\"flotOptions\"\n" +
    "                data-plot-hover=\"flotPlotHover\"\n" +
    "                data-threshold=\"thresholdValue\"\n" +
    "                data-threshold-label=\"'Return To Play'\"\n" +
    "                style=\"width: 100%; height: 600px;\">\n" +
    "            </div>\n" +
    "        </div>\n" +
    "\n" +
    "\n" +
    "        <!-- Passive Shoulder Range of Motion -->\n" +
    "        <div\n" +
    "            ng-show=\"metric.title == 'Shoulder Range of Motion' && selectizeRomModel == 1 && !isFetchingData\"\n" +
    "            class=\"col-xs-12 col-md-7 col-lg-9\">\n" +
    "\n" +
    "            <div class=\"row\" style=\"margin-top: 15px;\">\n" +
    "                <div class=\"col-md-2 col-md-offset-3 text-center\">\n" +
    "                    <div class=\"demo-legend-color\" ng-style=\"{'background-color': colors.extRot}\"></div>\n" +
    "                    External Rot. <br>\n" +
    "                    <span class=\"text-muted\" style=\"display: inline-block; margin-top: 5px\">100% = 90&deg;</span>\n" +
    "                </div>\n" +
    "                <div class=\"col-md-2 text-center\">\n" +
    "                    <div class=\"demo-legend-color\" ng-style=\"{'background-color': colors.flexion}\"></div>\n" +
    "                    Flexion <br>\n" +
    "                    <span class=\"text-muted\" style=\"display: inline-block; margin-top: 5px\">100% = 175&deg;</span>\n" +
    "                </div>\n" +
    "                <div class=\"col-md-2 text-center\">\n" +
    "                    <div class=\"demo-legend-color\" ng-style=\"{'background-color': colors.abduction}\"></div>\n" +
    "                    Abduction <br>\n" +
    "                    <span class=\"text-muted\" style=\"display: inline-block; margin-top: 5px\">100% = 180&deg;</span>\n" +
    "                </div>\n" +
    "            </div>\n" +
    "\n" +
    "            <div\n" +
    "                data-theme-flot-chart\n" +
    "                data-data=\"flotRomData\"\n" +
    "                data-options=\"flotRomOptions\"\n" +
    "                data-plot-hover=\"flotRomHover\"\n" +
    "                data-plot-labels=\"flotRomLabels\"\n" +
    "                style=\"width: 100%; height: 450px;\">\n" +
    "            </div>\n" +
    "\n" +
    "            <div class=\"row\" style=\"margin-top: 15px;\">\n" +
    "                <div class=\"col-md-2 col-md-offset-3 text-center\">\n" +
    "                    <div class=\"demo-legend-color\" ng-style=\"{'background-color': colors.intRot}\"></div>\n" +
    "                    Internal Rot. <br>\n" +
    "                    <span class=\"text-muted\" style=\"display: inline-block; margin-top: 5px\">100% = -80&deg;</span>\n" +
    "                </div>\n" +
    "                <div class=\"col-md-2 text-center\">\n" +
    "                    <div class=\"demo-legend-color\" ng-style=\"{'background-color': colors.extension}\"></div>\n" +
    "                    Extension <br>\n" +
    "                    <span class=\"text-muted\" style=\"display: inline-block; margin-top: 5px\">100% = -60&deg;</span>\n" +
    "                </div>\n" +
    "                <div class=\"col-md-2 text-center\">\n" +
    "                    <div class=\"demo-legend-color\" ng-style=\"{'background-color': colors.adduction}\"></div>\n" +
    "                    Adduction <br>\n" +
    "                    <span class=\"text-muted\" style=\"display: inline-block; margin-top: 5px\">100% = -40&deg;</span>\n" +
    "                </div>\n" +
    "            </div>\n" +
    "        </div>\n" +
    "\n" +
    "\n" +
    "        <!-- Active Shoulder Range of Motion -->\n" +
    "        <div\n" +
    "            ng-show=\"metric.title == 'Shoulder Range of Motion' && selectizeRomModel == 2 && !isFetchingData\"\n" +
    "            class=\"col-xs-12 col-md-7 col-lg-9\">\n" +
    "\n" +
    "            <div class=\"row\" style=\"margin-top: 15px;\">\n" +
    "                <div class=\"col-md-2 col-md-offset-3 text-center\">\n" +
    "                    <div class=\"demo-legend-color\" ng-style=\"{'background-color': colors.extRot}\"></div>\n" +
    "                    External Rot. <br>\n" +
    "                    <span class=\"text-muted\" style=\"display: inline-block; margin-top: 5px\">100% = 90&deg;</span>\n" +
    "                </div>\n" +
    "                <div class=\"col-md-2 text-center\">\n" +
    "                    <div class=\"demo-legend-color\" ng-style=\"{'background-color': colors.flexion}\"></div>\n" +
    "                    Flexion <br>\n" +
    "                    <span class=\"text-muted\" style=\"display: inline-block; margin-top: 5px\">100% = 175&deg;</span>\n" +
    "                </div>\n" +
    "                <div class=\"col-md-2 text-center\">\n" +
    "                    <div class=\"demo-legend-color\" ng-style=\"{'background-color': colors.abduction}\"></div>\n" +
    "                    Abduction <br>\n" +
    "                    <span class=\"text-muted\" style=\"display: inline-block; margin-top: 5px\">100% = 180&deg;</span>\n" +
    "                </div>\n" +
    "            </div>\n" +
    "\n" +
    "            <div\n" +
    "                data-theme-flot-chart\n" +
    "                data-data=\"flotActiveRomData\"\n" +
    "                data-options=\"flotActiveRomOptions\"\n" +
    "                data-plot-hover=\"flotRomHover\"\n" +
    "                data-plot-labels=\"flotActiveRomLabels\"\n" +
    "                style=\"width: 100%; height: 450px;\">\n" +
    "            </div>\n" +
    "\n" +
    "            <div class=\"row\" style=\"margin-top: 15px;\">\n" +
    "                <div class=\"col-md-2 col-md-offset-3 text-center\">\n" +
    "                    <div class=\"demo-legend-color\" ng-style=\"{'background-color': colors.intRot}\"></div>\n" +
    "                    Internal Rot. <br>\n" +
    "                    <span class=\"text-muted\" style=\"display: inline-block; margin-top: 5px\">100% = -80&deg;</span>\n" +
    "                </div>\n" +
    "                <div class=\"col-md-2 text-center\">\n" +
    "                    <div class=\"demo-legend-color\" ng-style=\"{'background-color': colors.extension}\"></div>\n" +
    "                    Extension <br>\n" +
    "                    <span class=\"text-muted\" style=\"display: inline-block; margin-top: 5px\">100% = -60&deg;</span>\n" +
    "                </div>\n" +
    "                <div class=\"col-md-2 text-center\">\n" +
    "                    <div class=\"demo-legend-color\" ng-style=\"{'background-color': colors.adduction}\"></div>\n" +
    "                    Adduction <br>\n" +
    "                    <span class=\"text-muted\" style=\"display: inline-block; margin-top: 5px\">100% = -40&deg;</span>\n" +
    "                </div>\n" +
    "            </div>\n" +
    "        </div>\n" +
    "\n" +
    "\n" +
    "        <!-- No metric selected -->\n" +
    "        <div ng-show=\"!metric && !isFetchingData\" class=\"col-xs-12 col-md-7 col-lg-9 text-center\">\n" +
    "            Select a metric to get started.\n" +
    "            <br>\n" +
    "            <br>\n" +
    "\n" +
    "            <div class=\"col-md-4 col-md-offset-4\">\n" +
    "                <selectize\n" +
    "                    ng-model=\"selectizeMetricModel\"\n" +
    "                    class=\"form-control text-center\"\n" +
    "                    config=\"selectizeMetricConfig\"\n" +
    "                    options=\"selectizeMetricOptions\"\n" +
    "                    placeholder=\"Select a metric.\">\n" +
    "                </selectize>\n" +
    "            </div>\n" +
    "        </div>\n" +
    "\n" +
    "\n" +
    "        <!-- Loading data -->\n" +
    "        <div ng-show=\"isFetchingData\" class=\"col-xs-12 col-md-7 col-lg-9 text-center text-muted\">\n" +
    "            <i class=\"fa fa-spinner fa-spin fa-3x\" style=\"display: block; margin: 100px 0 0\"></i>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("fms-bak/demo/analysis.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("fms-bak/demo/analysis.html",
    "<!-- Header -->\n" +
    "<div ng-include src=\"'fms-bak/live/partials/header.html'\"></div>\n" +
    "\n" +
    "<div class=\"page page-dashboard movement-pages\">\n" +
    "	<div>\n" +
    "\n" +
    "        <!-- Breadcrumbs -->\n" +
    "        <div ng-include src=\"'fms-bak/live/partials/breadcrumbs.html'\"></div>\n" +
    "\n" +
    "        <!-- Selected side -->\n" +
    "		<div\n" +
    "            ng-show=\"fms.current.iterations.length > 0\"\n" +
    "            ng-include src=\"'fms-bak/live/partials/iterations.html'\">\n" +
    "        </div>\n" +
    "\n" +
    "        <!-- Demo videos -->\n" +
    "        <div ng-include src=\"'fms-bak/demo/partials/analysis-layout-emphasized.html'\"></div>\n" +
    "\n" +
    "	</div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("fms-bak/demo/index.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("fms-bak/demo/index.html",
    "<!-- Header -->\n" +
    "<div ng-include src=\"'fms-bak/live/partials/header.html'\"></div>\n" +
    "\n" +
    "<div class=\"page page-dashboard movement-pages\">\n" +
    "	<div>\n" +
    "		{{ params }}\n" +
    "	</div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("fms-bak/demo/partials/analysis-coronal.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("fms-bak/demo/partials/analysis-coronal.html",
    "<div class=\"panel-heading text-right\">\n" +
    "    Front/Coronal\n" +
    "</div>\n" +
    "\n" +
    "<div class=\"panel-body\">\n" +
    "    <video class=\"demo-analysis\" style=\"width: 100%;\">\n" +
    "        <source ng-src=\"{{'../../../../demo/aslr/left/coronal.webm'}}\" type=\"video/webm\"/>\n" +
    "        <source src=\"{{'../../../../demo/aslr/left/coronal.mp4'}}\" type=\"video/mp4\"/>\n" +
    "        Sorry, your browser doesn't support HTML5 video.\n" +
    "    </video>\n" +
    "</div>\n" +
    "");
}]);

angular.module("fms-bak/demo/partials/analysis-layout-emphasized.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("fms-bak/demo/partials/analysis-layout-emphasized.html",
    "<div class=\"col-sm-12 panel-group panel-layout-emphasized\">\n" +
    "\n" +
    "    <!-- Emphasized view -->\n" +
    "    <div class=\"col-sm-9 no-gutter panel panel-default panel-emphasized\">\n" +
    "            <div class=\"col-sm-3\">\n" +
    "                <ng-include src=\"'fms-bak/live/partials/analysis-menu.html'\"></ng-include>\n" +
    "            </div>\n" +
    "\n" +
    "            <div class=\"col-sm-9\">\n" +
    "                <ng-include src=\"'fms-bak/demo/partials/analysis-'+ fms.views[0] +'.html'\"></ng-include>\n" +
    "            </div>\n" +
    "    </div>\n" +
    "\n" +
    "    <!-- Secondary views -->\n" +
    "    <div class=\"col-sm-3\">\n" +
    "        <div class=\"col-sm-12 panel panel-default\">\n" +
    "            <ng-include src=\"'fms-bak/demo/partials/analysis-'+ fms.views[1] +'.html'\"></ng-include>\n" +
    "        </div>\n" +
    "        <div class=\"col-sm-12 panel panel-default\">\n" +
    "            <ng-include src=\"'fms-bak/demo/partials/analysis-'+ fms.views[2] +'.html'\"></ng-include>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("fms-bak/demo/partials/analysis-sagittal.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("fms-bak/demo/partials/analysis-sagittal.html",
    "<div class=\"panel-heading text-right\">\n" +
    "    Side/Sagittal\n" +
    "</div>\n" +
    "\n" +
    "<div class=\"panel-body\">\n" +
    "    <video class=\"demo-analysis\" style=\"margin: 0 20%; width: 60%;\">\n" +
    "        <source ng-src=\"../../../../demo/aslr/analysis/sagittal.webm\" type=\"video/webm\"/>\n" +
    "        <source src=\"../../../../demo/aslr/analysis/sagittal.mp4\" type=\"video/mp4\"/>\n" +
    "        Sorry, your browser doesn't support HTML5 video.\n" +
    "    </video>\n" +
    "</div>\n" +
    "");
}]);

angular.module("fms-bak/demo/partials/analysis-transverse.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("fms-bak/demo/partials/analysis-transverse.html",
    "<div class=\"panel-heading text-right\">\n" +
    "    Horizontal/Transverse\n" +
    "</div>\n" +
    "\n" +
    "<div class=\"panel-body\">\n" +
    "    <video class=\"demo-analysis\" style=\"width: 100%;\">\n" +
    "        <source ng-src=\"{{'../../../../demo/aslr/left/transverse.webm'}}\" type=\"video/webm\"/>\n" +
    "        <source src=\"{{'../../../../demo/aslr/left/transverse.mp4'}}\" type=\"video/mp4\"/>\n" +
    "        Sorry, your browser doesn't support HTML5 video.\n" +
    "    </video>\n" +
    "</div>\n" +
    "");
}]);

angular.module("fms-bak/demo/partials/summary-content.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("fms-bak/demo/partials/summary-content.html",
    "<div class=\"panel-body\">\n" +
    "\n" +
    "    <!-- Total score -->\n" +
    "    <div class=\"col-sm-4\"\n" +
    "         style=\"height: 40px; line-height: 40px; background-color: #89c443; color: #333;\">\n" +
    "\n" +
    "        <div class=\"col-sm-9\">Total Score</div>\n" +
    "        <div class=\"col-sm-3\">3</div>\n" +
    "    </div>\n" +
    "\n" +
    "    <br>\n" +
    "    <br>\n" +
    "    <br>\n" +
    "    <br>\n" +
    "\n" +
    "    <!-- Hip results -->\n" +
    "    <table class=\"table table-striped\" ng-show=\"summary.joints.hip\">\n" +
    "\n" +
    "        <!-- Dummy heading -->\n" +
    "        <thead style=\"background-color: #d1d2d4; font-weight: bold; color: #333;\">\n" +
    "            <tr>\n" +
    "                <td>Hip</td>\n" +
    "                <td class=\"text-center\">Left</td>\n" +
    "                <td class=\"text-center\">Right</td>\n" +
    "                <td class=\"text-center\">Difference</td>\n" +
    "            </tr>\n" +
    "        </thead>\n" +
    "\n" +
    "        <!-- Dummy results -->\n" +
    "        <tbody style=\"font-weight: normal; color: #333;\">\n" +
    "            <tr>\n" +
    "                <td>Flexion/Extension</td>\n" +
    "                <td class=\"text-center\">92</td>\n" +
    "                <td class=\"text-center\">110</td>\n" +
    "                <td class=\"text-center\" style=\"background-color: #ed3941;\">18</td>\n" +
    "            </tr>\n" +
    "            <tr>\n" +
    "                <td>Abduction/Adduction</td>\n" +
    "                <td class=\"text-center\">19</td>\n" +
    "                <td class=\"text-center\">20</td>\n" +
    "                <td class=\"text-center\" style=\"background-color: #89c443;\">1</td>\n" +
    "            </tr>\n" +
    "            <tr>\n" +
    "                <td>Internal/External Rot.</td>\n" +
    "                <td class=\"text-center\">11</td>\n" +
    "                <td class=\"text-center\">18</td>\n" +
    "                <td class=\"text-center\" style=\"background-color: #f48b4a;\">7</td>\n" +
    "            </tr>\n" +
    "        </tbody>\n" +
    "    </table>\n" +
    "\n" +
    "    <div\n" +
    "        ng-show=\"summary.joints.hip\"\n" +
    "        class=\"col-sm-6 col-sm-offset-3\"\n" +
    "        style=\"margin-top: 10px; margin-bottom: 60px;\">\n" +
    "\n" +
    "        <div class=\"row\">\n" +
    "            <span class=\"pull-left\">\n" +
    "                <b>Left</b>\n" +
    "            </span>\n" +
    "            <span class=\"pull-right\">\n" +
    "                <b>Right</b>\n" +
    "            </span>\n" +
    "        </div>\n" +
    "        <div class=\"row linear-heat-gradient\">\n" +
    "            <div style=\"\n" +
    "                position: absolute;\n" +
    "                bottom: -5%;\n" +
    "                left: 60%;\n" +
    "                display: block;\n" +
    "                height: 75%;\n" +
    "                width: 2px;\n" +
    "                background-color: #000;\">\n" +
    "            </div>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "\n" +
    "    <!-- Knee results -->\n" +
    "    <table class=\"table table-striped\" ng-show=\"summary.joints.knee\">\n" +
    "\n" +
    "        <!-- Dummy heading -->\n" +
    "        <thead style=\"background-color: #d1d2d4; font-weight: bold; color: #333;\">\n" +
    "            <tr>\n" +
    "                <td>Knee</td>\n" +
    "                <td class=\"text-center\">Left</td>\n" +
    "                <td class=\"text-center\">Right</td>\n" +
    "                <td class=\"text-center\">Difference</td>\n" +
    "            </tr>\n" +
    "        </thead>\n" +
    "\n" +
    "        <!-- Dummy results -->\n" +
    "        <tbody style=\"font-weight: normal; color: #333;\">\n" +
    "            <tr>\n" +
    "                <td>Flexion/Extension</td>\n" +
    "                <td class=\"text-center\">120</td>\n" +
    "                <td class=\"text-center\">109</td>\n" +
    "                <td class=\"text-center\" style=\"background-color: #f48b4a;\">11</td>\n" +
    "            </tr>\n" +
    "            <tr>\n" +
    "                <td>Internal/External Rot.</td>\n" +
    "                <td class=\"text-center\">2</td>\n" +
    "                <td class=\"text-center\">18</td>\n" +
    "                <td class=\"text-center\" style=\"background-color: #ed3941;\">16</td>\n" +
    "            </tr>\n" +
    "        </tbody>\n" +
    "    </table>\n" +
    "\n" +
    "    <div\n" +
    "        ng-show=\"summary.joints.knee\"\n" +
    "        class=\"col-sm-6 col-sm-offset-3\"\n" +
    "        style=\"margin-top: 10px; margin-bottom: 60px;\">\n" +
    "        <div class=\"row\">\n" +
    "            <span class=\"pull-left\">\n" +
    "                <b>Left</b>\n" +
    "            </span>\n" +
    "            <span class=\"pull-right\">\n" +
    "                <b>Right</b>\n" +
    "            </span>\n" +
    "        </div>\n" +
    "        <div class=\"row linear-heat-gradient\">\n" +
    "            <div style=\"\n" +
    "                position: absolute;\n" +
    "                bottom: -5%;\n" +
    "                left: 52%;\n" +
    "                display: block;\n" +
    "                height: 75%;\n" +
    "                width: 2px;\n" +
    "                background-color: #000;\">\n" +
    "            </div>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "\n" +
    "</div>\n" +
    "");
}]);

angular.module("fms-bak/demo/partials/summary-layout.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("fms-bak/demo/partials/summary-layout.html",
    "<div class=\"col-sm-12 panel-group\">\n" +
    "\n" +
    "    <div class=\"col-sm-9 no-gutter panel panel-default\" style=\"padding-left: 0px; height: 700px;\">\n" +
    "            <div class=\"col-sm-3\" style=\"height: 700px;\">\n" +
    "                <ng-include src=\"'fms-bak/demo/partials/summary-menu.html'\"></ng-include>\n" +
    "            </div>\n" +
    "\n" +
    "            <!-- Demo summary content -->\n" +
    "            <div class=\"col-sm-9\" style=\"height: 700px;\">\n" +
    "                <ng-include src=\"'fms-bak/demo/partials/summary-content.html'\"></ng-include>\n" +
    "            </div>\n" +
    "    </div>\n" +
    "\n" +
    "    <div class=\"col-sm-3\">\n" +
    "\n" +
    "        <!-- Movement overview -->\n" +
    "        <div class=\"col-sm-12 panel panel-default\">\n" +
    "            <ng-include src=\"'fms-bak/demo/partials/summary-movement-overview.html'\"></ng-include>\n" +
    "        </div>\n" +
    "\n" +
    "        <!-- Overall score -->\n" +
    "        <div class=\"col-sm-12 panel panel-default\">\n" +
    "            <ng-include src=\"'fms-bak/demo/partials/summary-overall-scores.html'\"></ng-include>\n" +
    "        </div>\n" +
    "\n" +
    "        <!-- Legend -->\n" +
    "        <div class=\"col-sm-12 panel panel-default\">\n" +
    "            <ng-include src=\"'fms-bak/live/partials/summary-legend.html'\"></ng-include>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("fms-bak/demo/partials/summary-menu.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("fms-bak/demo/partials/summary-menu.html",
    "<div class=\"movement-left-column non-test-pages\" style=\"height: 100%\">\n" +
    "\n" +
    "    <!-- Select trial -->\n" +
    "    <div\n" +
    "        ng-hide=\"analysis.playbackRatePane || analysis.planePane\"\n" +
    "        ng-click=\"analysis.trialPane = !analysis.trialPane; analysis.playbackRatePane = false;\"\n" +
    "        class=\"row\"\n" +
    "        style=\"padding:0;\">\n" +
    "        <div class=\"col-sm-8\" style=\"height:56px;\">\n" +
    "            {{ fms.current.trial.name }}\n" +
    "        </div>\n" +
    "        <div class=\"col-sm-4\" style=\"height:56px;\">\n" +
    "            <i ng-show=\"analysis.trialPane\" style=\"line-height:inherit;transform:scale(1, 1.5);\" class=\"fa fa-2x fa-caret-up\" />\n" +
    "            <i ng-hide=\"analysis.trialPane\" style=\"line-height:inherit;transform:scale(1, 1.5);\" class=\"fa fa-2x fa-caret-down\" />\n" +
    "        </div>\n" +
    "    </div>\n" +
    "    <div\n" +
    "        ng-if=\"analysis.trialPane\"\n" +
    "        ng-repeat=\"trial in fms.current.trials\"\n" +
    "        ng-click=\"fms.current.trial = trial; analysis.trialPane = false;\"\n" +
    "        class=\"row hierarchy-2\">\n" +
    "\n" +
    "        <div class=\"col-sm-8\">\n" +
    "            {{trial.name}}\n" +
    "        </div>\n" +
    "        <div class=\"col-sm-4\" ng-click=\"joint.selected = !joint.selected\">\n" +
    "            <i ng-show=\"fms.current.trial == trial\" style=\"line-height:inherit;\" class=\"fa fa-2x fa-check\" />\n" +
    "        </div>\n" +
    "    </div>\n" +
    "\n" +
    "    <!-- Select all joints -->\n" +
    "    <div\n" +
    "        ng-click=\"summary.joints.select('all');\"\n" +
    "        class=\"row\"\n" +
    "        style=\"padding:0;\">\n" +
    "\n" +
    "        <div class=\"col-sm-8\" style=\"height: 56px; color: #333;\">\n" +
    "            Select All\n" +
    "        </div>\n" +
    "        <div class=\"col-sm-4\" style=\"height:56px;\">\n" +
    "            <div ng-hide=\"summary.joints.all\" class=\"checkbox\"></div>\n" +
    "            <div ng-show=\"summary.joints.all\" class=\"checked checkbox\"></div>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "\n" +
    "    <!-- Select Hip -->\n" +
    "    <div\n" +
    "        ng-click=\"summary.joints.select('hip');\"\n" +
    "        class=\"row\"\n" +
    "        style=\"padding:0;\">\n" +
    "\n" +
    "        <div class=\"col-sm-8\" style=\"height: 56px; color: #333;\">\n" +
    "            Hip\n" +
    "        </div>\n" +
    "        <div class=\"col-sm-4\" style=\"height:56px;\">\n" +
    "            <div ng-hide=\"summary.joints.hip\" class=\"checkbox\"></div>\n" +
    "            <div ng-show=\"summary.joints.hip\" class=\"checked checkbox\"></div>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "\n" +
    "    <!-- Select Knee -->\n" +
    "    <div\n" +
    "        ng-click=\"summary.joints.select('knee');\"\n" +
    "        class=\"row\"\n" +
    "        style=\"padding:0;\">\n" +
    "\n" +
    "        <div class=\"col-sm-8\" style=\"height: 56px; color: #333;\">\n" +
    "            Knee\n" +
    "        </div>\n" +
    "        <div class=\"col-sm-4\" style=\"height:56px;\">\n" +
    "            <div ng-hide=\"summary.joints.knee\" class=\"checkbox\"></div>\n" +
    "            <div ng-show=\"summary.joints.knee\" class=\"checked checkbox\"></div>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("fms-bak/demo/partials/summary-movement-overview.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("fms-bak/demo/partials/summary-movement-overview.html",
    "<div class=\"panel-heading text-center\">\n" +
    "    Movement Overview\n" +
    "</div>\n" +
    "\n" +
    "<div class=\"panel-body\">\n" +
    "    <div>\n" +
    "        <div class=\"text-left col-sm-12\">\n" +
    "            <h2 style=\"margin:0\";>\n" +
    "                <b style=\"font-size:40%\">Left</b>\n" +
    "            </h2>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "    <div class=\"panel-body col-sm-12\">\n" +
    "        <img\n" +
    "            ng-src=\"{{ '../../../../demo/aslr/summary/heat_map_left.png' }}\"\n" +
    "            style=\"height:100%;display:block;margin:auto;\">\n" +
    "    </div>\n" +
    "    <div>\n" +
    "        <div class=\"text-left col-sm-12\">\n" +
    "            <h2 style=\"margin:0\">\n" +
    "                <b style=\"font-size:40%\">Right</b>\n" +
    "            </h2>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "    <div class=\"panel-body col-sm-12\">\n" +
    "        <img\n" +
    "            ng-src=\"{{ '../../../../demo/aslr/summary/heat_map_left.png' }}\"\n" +
    "            style=\"height:100%;display:block;margin:auto;\">\n" +
    "    </div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("fms-bak/demo/partials/summary-overall-scores.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("fms-bak/demo/partials/summary-overall-scores.html",
    "<div class=\"panel-heading text-center\">\n" +
    "    Overall Score\n" +
    "</div>\n" +
    "\n" +
    "<div class=\"panel-body text-left\">\n" +
    "\n" +
    "    <!-- Total score -->\n" +
    "    <div class=\"row\">\n" +
    "        <div class=\"col-sm-12\">\n" +
    "            Total Test Score\n" +
    "        </div>\n" +
    "    </div>\n" +
    "\n" +
    "    <br>\n" +
    "\n" +
    "    <div class=\"row \">\n" +
    "        <div class=\"col-sm-12 no-gutter\">\n" +
    "            <div class=\"col-sm-1\" style=\"background-color: #f48b4a\">&nbsp;</div>\n" +
    "            <div class=\"col-sm-1\" style=\"background-color: #ed3941\">&nbsp;</div>\n" +
    "            <div class=\"col-sm-2\" style=\"background-color: #89c443\">&nbsp;</div>\n" +
    "            <div class=\"col-sm-1\" style=\"background-color: #ed3941\">&nbsp;</div>\n" +
    "            <div class=\"col-sm-2\" style=\"background-color: #89c443\">&nbsp;</div>\n" +
    "            <div class=\"col-sm-4 text-center\" style=\"background-color: black; color: white\">15/21</div>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "\n" +
    "    <br>\n" +
    "\n" +
    "    <!-- Movement Efficiency -->\n" +
    "    <div class=\"row\">\n" +
    "        <div class=\"col-sm-12 text-left\">\n" +
    "            Movement Efficiency\n" +
    "        </div>\n" +
    "    </div>\n" +
    "\n" +
    "    <br>\n" +
    "\n" +
    "    <div class=\"row\">\n" +
    "        <div class=\"col-sm-12\">\n" +
    "            <div style=\"position: relative; margin: 0 auto; height: 120px; width: 120px;\">\n" +
    "                <div class=\"c100 p71 green\">\n" +
    "                    <span>71%</span>\n" +
    "                    <div class=\"slice\">\n" +
    "                        <div class=\"bar\"></div>\n" +
    "                        <div class=\"fill\"></div>\n" +
    "                    </div>\n" +
    "                </div>\n" +
    "            </div>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "\n" +
    "</div>\n" +
    "");
}]);

angular.module("fms-bak/demo/partials/test-coronal.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("fms-bak/demo/partials/test-coronal.html",
    "<div class=\"panel-heading text-right\">\n" +
    "    Front/Coronal\n" +
    "</div>\n" +
    "\n" +
    "<div class=\"panel-body\">\n" +
    "    <video class=\"demo-test\" style=\"width: 100%;\">\n" +
    "        <source ng-src=\"{{'../../../../demo/aslr/left/coronal.webm'}}\" type=\"video/webm\"/>\n" +
    "        <source src=\"{{'../../../../demo/aslr/left/coronal.mp4'}}\" type=\"video/mp4\"/>\n" +
    "        Sorry, your browser doesn't support HTML5 video.\n" +
    "    </video>\n" +
    "</div>\n" +
    "");
}]);

angular.module("fms-bak/demo/partials/test-layout-emphasized.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("fms-bak/demo/partials/test-layout-emphasized.html",
    "<div class=\"col-sm-12 panel-group panel-layout-emphasized\">\n" +
    "\n" +
    "    <!-- Emphasized view -->\n" +
    "    <div class=\"col-sm-9 no-gutter panel panel-default panel-emphasized\">\n" +
    "            <div class=\"col-sm-3\">\n" +
    "                <ng-include src=\"'fms-bak/live/partials/test-menu.html'\"></ng-include>\n" +
    "            </div>\n" +
    "\n" +
    "            <div class=\"col-sm-9\">\n" +
    "                <ng-include src=\"'fms-bak/demo/partials/test-'+ fms.views[0] +'.html'\"></ng-include>\n" +
    "            </div>\n" +
    "    </div>\n" +
    "\n" +
    "    <!-- Secondary views -->\n" +
    "    <div class=\"col-sm-3\">\n" +
    "        <div class=\"col-sm-12 panel panel-default\">\n" +
    "            <ng-include src=\"'fms-bak/demo/partials/test-'+ fms.views[1] +'.html'\"></ng-include>\n" +
    "        </div>\n" +
    "        <div class=\"col-sm-12 panel panel-default\">\n" +
    "            <ng-include src=\"'fms-bak/demo/partials/test-'+ fms.views[2] +'.html'\"></ng-include>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("fms-bak/demo/partials/test-sagittal.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("fms-bak/demo/partials/test-sagittal.html",
    "<div class=\"panel-heading text-right\">\n" +
    "    Side/Sagittal\n" +
    "</div>\n" +
    "\n" +
    "<div class=\"panel-body\">\n" +
    "    <video class=\"demo-test\" style=\"margin: 0 20%; width: 60%;\">\n" +
    "        <!-- <source ng-src=\"{{ '../../../../demo/aslr/analysis/sagittal.webm?1' }}\" type=\"video/webm\"/>\n" +
    "        <source src=\"{{ '../../../../demo/aslr/analysis/sagittal.mp4?1' }}\" type=\"video/mp4\"/>\n" +
    "        <source ng-src=\"{{'../../../../demo/aslr/analysis/sagittal.webm'}}\" type=\"video/webm\"/>\n" +
    "        <source src=\"{{'../../../../demo/aslr/analysis/sagittal.mp4'}}\" type=\"video/mp4\"/> -->\n" +
    "        <source ng-src=\"{{'../../../../demo/aslr/left/sagittal.webm'}}\" type=\"video/webm\"/>\n" +
    "        <source src=\"{{'../../../../demo/aslr/left/sagittal.mp4'}}\" type=\"video/mp4\"/>\n" +
    "        Sorry, your browser doesn't support HTML5 video.\n" +
    "    </video>\n" +
    "</div>\n" +
    "");
}]);

angular.module("fms-bak/demo/partials/test-transverse.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("fms-bak/demo/partials/test-transverse.html",
    "<div class=\"panel-heading text-right\">\n" +
    "    Horizontal/Transverse\n" +
    "</div>\n" +
    "\n" +
    "<div class=\"panel-body\">\n" +
    "    <video class=\"demo-test\" style=\"width: 100%;\">\n" +
    "        <source ng-src=\"{{'../../../../demo/aslr/left/transverse.webm'}}\" type=\"video/webm\"/>\n" +
    "        <source src=\"{{'../../../../demo/aslr/left/transverse.mp4'}}\" type=\"video/mp4\"/>\n" +
    "        Sorry, your browser doesn't support HTML5 video.\n" +
    "    </video>\n" +
    "</div>\n" +
    "");
}]);

angular.module("fms-bak/demo/summary.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("fms-bak/demo/summary.html",
    "<!-- Header -->\n" +
    "<div ng-include src=\"'fms-bak/live/partials/header.html'\"></div>\n" +
    "\n" +
    "<div class=\"page page-dashboard movement-pages\">\n" +
    "	<div>\n" +
    "\n" +
    "        <!-- Breadcrumbs -->\n" +
    "        <div ng-include src=\"'fms-bak/live/partials/breadcrumbs.html'\"></div>\n" +
    "\n" +
    "        <!-- Demo summary -->\n" +
    "        <div ng-include src=\"'fms-bak/demo/partials/summary-layout.html'\"></div>\n" +
    "	</div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("fms-bak/demo/test.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("fms-bak/demo/test.html",
    "<!-- Header -->\n" +
    "<div ng-include src=\"'fms-bak/live/partials/header.html'\"></div>\n" +
    "\n" +
    "<div class=\"page page-dashboard movement-pages\">\n" +
    "	<div>\n" +
    "\n" +
    "        <!-- Breadcrumbs -->\n" +
    "        <ng-include src=\"'fms-bak/live/partials/breadcrumbs.html'\"></ng-include>\n" +
    "\n" +
    "        <!-- Selected side -->\n" +
    "		<ng-include\n" +
    "            ng-show=\"fms.current.iterations.length > 0\"\n" +
    "            src=\"'fms-bak/live/partials/iterations.html'\">\n" +
    "        </ng-include>\n" +
    "\n" +
    "        <!-- Test run summary -->\n" +
    "        <div class=\"row\">\n" +
    "            <div\n" +
    "                class=\"col-sm-12 text-center\"\n" +
    "                style=\"margin: 10px auto 20px; font-weight: normal; color: #aaa;\">\n" +
    "                Current test run: <b>{{ fms.current.trial.name }}</b> / <b>{{ fms.current.iteration }}</b>\n" +
    "            </div>\n" +
    "        </div>\n" +
    "\n" +
    "        <!-- Demo videos -->\n" +
    "        <ng-include src=\"'fms-bak/demo/partials/test-layout-emphasized.html'\"></ng-include>\n" +
    "\n" +
    "        <!-- Trials -->\n" +
    "        <ng-include src=\"'fms-bak/live/partials/test-trials.html'\"></ng-include>\n" +
    "	</div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("fms-bak/live/analysis.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("fms-bak/live/analysis.html",
    "<!-- Header -->\n" +
    "<div ng-include src=\"'fms-bak/live/partials/header.html'\"></div>\n" +
    "\n" +
    "<!-- Temporary placeholder -->\n" +
    "<ng-include src=\"'placeholder.html'\"></ng-include>\n" +
    "");
}]);

angular.module("fms-bak/live/index.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("fms-bak/live/index.html",
    "<!-- Header -->\n" +
    "<div ng-include src=\"'fms-bak/live/partials/header.html'\"></div>\n" +
    "\n" +
    "<!-- Temporary placeholder -->\n" +
    "<ng-include src=\"'placeholder.html' + assetVersion\"></ng-include>\n" +
    "");
}]);

angular.module("fms-bak/live/partials/analysis-menu.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("fms-bak/live/partials/analysis-menu.html",
    "<div class=\"movement-left-column non-test-pages\">\n" +
    "\n" +
    "    <!-- Select trial -->\n" +
    "    <div\n" +
    "        ng-hide=\"analysis.playbackRatePane || analysis.planePane\"\n" +
    "        ng-click=\"analysis.trialPane = !analysis.trialPane; analysis.playbackRatePane = false;\"\n" +
    "        class=\"row\"\n" +
    "        style=\"padding:0;\">\n" +
    "        <div class=\"col-sm-8\" style=\"height:56px;\">\n" +
    "            {{ fms.current.trial.name }}\n" +
    "        </div>\n" +
    "        <div class=\"col-sm-4\" style=\"height:56px;\">\n" +
    "            <i ng-show=\"analysis.trialPane\" style=\"line-height:inherit;transform:scale(1, 1.5);\" class=\"fa fa-2x fa-caret-up\" />\n" +
    "            <i ng-hide=\"analysis.trialPane\" style=\"line-height:inherit;transform:scale(1, 1.5);\" class=\"fa fa-2x fa-caret-down\" />\n" +
    "        </div>\n" +
    "    </div>\n" +
    "    <div\n" +
    "        ng-if=\"analysis.trialPane\"\n" +
    "        ng-repeat=\"trial in fms.current.trials\"\n" +
    "        ng-click=\"fms.current.trial = trial; analysis.trialPane = false;\"\n" +
    "        class=\"row hierarchy-2\">\n" +
    "\n" +
    "        <div class=\"col-sm-8\">\n" +
    "            {{trial.name}}\n" +
    "        </div>\n" +
    "        <div class=\"col-sm-4\" ng-click=\"joint.selected = !joint.selected\">\n" +
    "            <i ng-show=\"fms.current.trial == trial\" style=\"line-height:inherit;\" class=\"fa fa-2x fa-check\" />\n" +
    "        </div>\n" +
    "    </div>\n" +
    "\n" +
    "    <!-- Select plane -->\n" +
    "    <div\n" +
    "        ng-hide=\"analysis.playbackRatePane || analysis.trialPane\"\n" +
    "        ng-click=\"Rover.alert('Demo')\"\n" +
    "        class=\"row\"\n" +
    "        style=\"height:56px;margin:0;line-height:56px;\">\n" +
    "\n" +
    "        <div class=\"col-sm-8\">\n" +
    "            Plane\n" +
    "        </div>\n" +
    "        <div class=\"col-sm-4\" style=\"height:56px;\">\n" +
    "            <i style=\"line-height:inherit;transform:scale(1, 1.5);\" class=\"fa fa-2x fa-caret-down\"/>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "\n" +
    "    <!-- Select playback rate -->\n" +
    "    <div\n" +
    "        ng-click=\"analysis.playbackRatePane = !analysis.playbackRatePane; analysis.trialPane = false\"\n" +
    "        ng-hide=\"analysis.trialPane || analysis.planePane\"\n" +
    "        class=\"row\"\n" +
    "        style=\"height:56px;margin:0;line-height:56px;\">\n" +
    "\n" +
    "        <div class=\"col-sm-8\">\n" +
    "            Speed ({{ analysis.playbackRate }}x)\n" +
    "        </div>\n" +
    "        <div class=\"col-sm-4\" style=\"height:56px;\">\n" +
    "            <i ng-hide=\"analysis.playbackRatePane\" style=\"line-height:inherit;transform:scale(1, 1.5);\" class=\"fa fa-2x fa-caret-down\" />\n" +
    "            <i ng-show=\"analysis.playbackRatePane\" style=\"line-height:inherit;transform:scale(1, 1.5);\" class=\"fa fa-2x fa-caret-up\" />\n" +
    "        </div>\n" +
    "    </div>\n" +
    "    <div\n" +
    "        ng-if=\"analysis.playbackRatePane\"\n" +
    "        ng-repeat=\"playbackRate in [0.5, 1, 5, 25]\"\n" +
    "        ng-click=\"analysis.setPlaybackRate(playbackRate); analysis.playbackRatePane = false;\"\n" +
    "        class=\"row hierarchy-2\">\n" +
    "\n" +
    "        <div class=\"col-sm-8\">\n" +
    "            {{ playbackRate }}x\n" +
    "        </div>\n" +
    "\n" +
    "        <div class=\"col-sm-4\">\n" +
    "            <i ng-if=\"analysis.playbackRate == playbackRate\" style=\"line-height:inherit;\" class=\"fa fa-2x fa-check\" />\n" +
    "        </div>\n" +
    "    </div>\n" +
    "\n" +
    "    <!-- Show angles -->\n" +
    "    <div ng-click=\"Rover.alert('Demo')\" class=\"row\" style=\"height:56px;margin:0;line-height:56px;\">\n" +
    "        <div class=\"col-sm-8\">\n" +
    "            Angles\n" +
    "        </div>\n" +
    "        <div class=\"col-sm-4\" style=\"height:56px;\">\n" +
    "            <i style=\"line-height:inherit;\" class=\"fa fa-2x fa-check-square-o\"/>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "\n" +
    "    <!-- Playback controls -->\n" +
    "    <div class=\"video-controls\">\n" +
    "        <i ng-click=\"analysis.play()\" class=\"fa fa-2x fa-play\" style=\"margin-right:5px;\"/>\n" +
    "        <i ng-click=\"Rover.alert('Demo')\" class=\"fa fa-2x fa-forward\" style=\"margin-right:5px;\"/>\n" +
    "        <i ng-click=\"analysis.pause()\" class=\"fa fa-2x fa-pause\" style=\"margin-right:5px;\" />\n" +
    "        <i ng-click=\"analysis.reset()\" class=\"fa fa-2x fa-undo fa-flip-vertical\" />\n" +
    "    </div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("fms-bak/live/partials/breadcrumbs.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("fms-bak/live/partials/breadcrumbs.html",
    "<ol class=\"breadcrumb-movement\">\n" +
    "\n" +
    "    <!-- Movement title -->\n" +
    "    <li>\n" +
    "        <div>\n" +
    "            <a>{{ fms.current.name }}</a>\n" +
    "        </div>\n" +
    "    </li>\n" +
    "\n" +
    "    <!-- Movement tabs -->\n" +
    "    <li\n" +
    "        ng-click=\"global.browseTo.path('/fms/'+ (isDemo ? 'demo' : 'live') +'/'+ fms.current.id +'/test')\"\n" +
    "        ng-class=\"{'active': params.step == 'test'}\">\n" +
    "\n" +
    "        <a>Test</a>\n" +
    "    </li>\n" +
    "    <li\n" +
    "        ng-click=\"global.browseTo.path('/fms/'+ (isDemo ? 'demo' : 'live') +'/'+ fms.current.id +'/analysis')\"\n" +
    "        ng-class=\"{'active': params.step == 'analysis'}\">\n" +
    "        <a>Analysis</a>\n" +
    "    </li>\n" +
    "    <li\n" +
    "        ng-click=\"global.browseTo.path('/fms/'+ (isDemo ? 'demo' : 'live') +'/'+ fms.current.id +'/summary')\"\n" +
    "        ng-class=\"{'active': params.step == 'summary'}\">\n" +
    "\n" +
    "        <a>Summary</a>\n" +
    "    </li>\n" +
    "</ol>\n" +
    "");
}]);

angular.module("fms-bak/live/partials/header.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("fms-bak/live/partials/header.html",
    "<header class=\"page-main-header\">\n" +
    "    <div class=\"row\">\n" +
    "        <div class=\"col-sm-6\">\n" +
    "            <h4>Functional Movement Screening</h4>\n" +
    "            <p>Start your tests</p>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "</header>\n" +
    "");
}]);

angular.module("fms-bak/live/partials/iterations.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("fms-bak/live/partials/iterations.html",
    "<ol class=\"breadcrumb-movement-sm\">\n" +
    "    <li\n" +
    "        ng-click=\"fms.current.iteration = iteration\"\n" +
    "        ng-class=\"{'active': iteration == fms.current.iteration}\"\n" +
    "        ng-repeat=\"iteration in fms.current.iterations\">\n" +
    "        \n" +
    "        <a>\n" +
    "            {{ iteration }}\n" +
    "        </a>\n" +
    "    </li>\n" +
    "</ol>\n" +
    "");
}]);

angular.module("fms-bak/live/partials/summary-legend.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("fms-bak/live/partials/summary-legend.html",
    "<div class=\"panel-heading text-center\">\n" +
    "    Thresholds\n" +
    "    <br>\n" +
    "    <small style=\"font-size: 0.8em; color: #aaa; text-transform: none;\">\n" +
    "        (in degrees)\n" +
    "    </small>\n" +
    "</div>\n" +
    "<div class=\"panel-body\">\n" +
    "\n" +
    "    <div class=\"row no-gutter\" style=\"margin:0; height:32px; color:#89c443;\">\n" +
    "        <div class=\"col-sm-3\">\n" +
    "            <b>0-5&deg;</b>\n" +
    "        </div>\n" +
    "        <div class=\"col-sm-9\">\n" +
    "            <b>Within reasonable movement standards</b>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "\n" +
    "    <div class=\"row no-gutter\" style=\"margin:0;height:32px;color:#f48b4a;\">\n" +
    "        <div class=\"col-sm-3\">\n" +
    "            <b>5-15&deg;</b>\n" +
    "        </div>\n" +
    "        <div class=\"col-sm-9\">\n" +
    "            <b>Area of concern</b>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "    <div class=\"row no-gutter\" style=\"margin:0;height:32px;color:#ed3941;\">\n" +
    "        <div class=\"col-sm-3\">\n" +
    "            <b>15&deg;+</b>\n" +
    "        </div>\n" +
    "        <div class=\"col-sm-9\">\n" +
    "            <b>Area to address</b>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("fms-bak/live/partials/summary-menu.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("fms-bak/live/partials/summary-menu.html",
    "<div class=\"movement-left-column non-test-pages\" style=\"height: 100%\">\n" +
    "\n" +
    "    <!-- Select trial -->\n" +
    "    <div\n" +
    "        ng-hide=\"analysis.playbackRatePane || analysis.planePane\"\n" +
    "        ng-click=\"analysis.trialPane = !analysis.trialPane; analysis.playbackRatePane = false;\"\n" +
    "        class=\"row\"\n" +
    "        style=\"padding:0;\">\n" +
    "        <div class=\"col-sm-8\" style=\"height:56px;\">\n" +
    "            {{ fms.current.trial.name }}\n" +
    "        </div>\n" +
    "        <div class=\"col-sm-4\" style=\"height:56px;\">\n" +
    "            <i ng-show=\"analysis.trialPane\" style=\"line-height:inherit;transform:scale(1, 1.5);\" class=\"fa fa-2x fa-caret-up\" />\n" +
    "            <i ng-hide=\"analysis.trialPane\" style=\"line-height:inherit;transform:scale(1, 1.5);\" class=\"fa fa-2x fa-caret-down\" />\n" +
    "        </div>\n" +
    "    </div>\n" +
    "    <div\n" +
    "        ng-if=\"analysis.trialPane\"\n" +
    "        ng-repeat=\"trial in fms.current.trials\"\n" +
    "        ng-click=\"fms.current.trial = trial; analysis.trialPane = false;\"\n" +
    "        class=\"row hierarchy-2\">\n" +
    "\n" +
    "        <div class=\"col-sm-8\">\n" +
    "            {{trial.name}}\n" +
    "        </div>\n" +
    "        <div class=\"col-sm-4\" ng-click=\"joint.selected = !joint.selected\">\n" +
    "            <i ng-show=\"fms.current.trial == trial\" style=\"line-height:inherit;\" class=\"fa fa-2x fa-check\" />\n" +
    "        </div>\n" +
    "    </div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("fms-bak/live/partials/test-menu.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("fms-bak/live/partials/test-menu.html",
    "<div class=\"movement-left-column test-page\">\n" +
    "    <div\n" +
    "        ng-click=\"run.start()\"\n" +
    "        ng-show=\"isTestLive == false && !run.exists()\"\n" +
    "        style=\"background-color: #89c443; color: #fff;\">\n" +
    "        Start\n" +
    "    </div>\n" +
    "    <div\n" +
    "        ng-click=\"run.end()\"\n" +
    "        ng-show=\"isTestLive == true\"\n" +
    "        style=\"background-color: #ed3941; color: #fff;\">\n" +
    "        Stop\n" +
    "    </div>\n" +
    "    <div\n" +
    "        ng-click=\"run.pain()\"\n" +
    "        ng-show=\"isTestLive == true\"\n" +
    "        style=\"background-color: #f48b4a; color: #fff;\">\n" +
    "        Pain\n" +
    "    </div>\n" +
    "    <div\n" +
    "        ng-click=\"run.fault()\"\n" +
    "        ng-show=\"isTestLive == true\"\n" +
    "        style=\"background-color: #f48b4a; color: #fff;\">\n" +
    "        Fault\n" +
    "    </div>\n" +
    "    <div\n" +
    "        ng-click=\"run.reset()\"\n" +
    "        ng-show=\"isTestLive == false && run.exists()\"\n" +
    "        style=\"color:#f48b4a;\">\n" +
    "        Rerun the test\n" +
    "    </div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("fms-bak/live/partials/test-trials.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("fms-bak/live/partials/test-trials.html",
    "<div class=\"row\">\n" +
    "	<div class=\"col-sm-4\">\n" +
    "		<div class=\"panel panel-default\">\n" +
    "			<div class=\"panel-body\" style=\"padding:0px;\">\n" +
    "				<div class=\"col-sm-12 ng-scope movement-trial-list\" style=\"padding:0px;\">\n" +
    "					<div\n" +
    "                        ng-repeat=\"trial in fms.current.trials\"\n" +
    "                        ng-click=\"fms.current.trial = trial;run.name = null\"\n" +
    "                        ng-class=\"{'active': trial == fms.current.trial}\">\n" +
    "\n" +
    "						<div class=\"trial-label\">\n" +
    "							{{ trial.name }}\n" +
    "                            <span\n" +
    "                                ng-show=\"run.getRunByTrial(trial) && run.getRunByTrial(trial).numFaults > 0\"\n" +
    "                                style=\"font-size:.7em;color:#999;font-weight:normal;margin-left: 5px;position:absolute;\">\n" +
    "\n" +
    "                                Faults: {{ run.getRunByTrial(trial).numFaults }}\n" +
    "                            </span>\n" +
    "						</div>\n" +
    "\n" +
    "						<div class=\"trial-status\">\n" +
    "\n" +
    "                            <!-- Status text -->\n" +
    "							<div class=\"col-sm-8\">\n" +
    "								<div ng-if=\"run.getStatus(trial) == 'saved' && run.getRunByTrial(trial).numFaults == 0\">\n" +
    "								    No Fault\n" +
    "								</div>\n" +
    "								<div ng-if=\"run.getStatus(trial) == 'saved' && run.getRunByTrial(trial).numFaults > 0\">\n" +
    "								    Faults: {{ run.getRunByTrial(trial).numFaults }}\n" +
    "								</div>\n" +
    "								<div ng-if=\"run.getStatus(trial) == 'pain'\">\n" +
    "                                    Pain\n" +
    "                                </div>\n" +
    "							</div>\n" +
    "\n" +
    "							<div class=\"col-sm-4\">\n" +
    "								<img ng-if=\"run.getStatus(trial) == 'live'\" class=\"pull-right\" src=\"../../images/ui/lock_open.png\" alt=\"unsaved\" style=\"height:40px;margin-top:5px;\" />\n" +
    "								<img ng-if=\"run.getStatus(trial) == 'saved' || run.getStatus(trial) =='pain'\" class=\"pull-right\" src=\"../../images/ui/lock_closed.png\" alt=\"saved\" style=\"height:40px;margin-top:5px;\" />\n" +
    "							</div>\n" +
    "						</div>\n" +
    "					</div>\n" +
    "					<div ng-click=\"run.submit()\" ng-hide=\"fms.current.isTestSubmitted\" class=\"movement-submit-button\">\n" +
    "						Submit Results\n" +
    "					</div>\n" +
    "					<div ng-show=\"fms.current.isTestSubmitted\" class=\"movement-submit-button\">\n" +
    "						Results Successfully Submitted\n" +
    "					</div>\n" +
    "				</div>\n" +
    "			</div>\n" +
    "		</div>\n" +
    "	</div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("fms-bak/live/summary.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("fms-bak/live/summary.html",
    "<!-- Header -->\n" +
    "<div ng-include src=\"'fms-bak/live/partials/header.html'\"></div>\n" +
    "\n" +
    "<!-- Temporary placeholder -->\n" +
    "<ng-include src=\"'placeholder.html'\"></ng-include>\n" +
    "");
}]);

angular.module("fms-bak/live/test.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("fms-bak/live/test.html",
    "<!-- Header -->\n" +
    "<div ng-include src=\"'fms-bak/live/partials/header.html'\"></div>\n" +
    "\n" +
    "<!-- Temporary placeholder -->\n" +
    "<ng-include src=\"'placeholder.html'\"></ng-include>\n" +
    "");
}]);

angular.module("group/edit.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("group/edit.html",
    "<div class=\"page page-dashboard\">\n" +
    "\n" +
    "    <!-- Breadcrumbs -->\n" +
    "    <div class=\"row\">\n" +
    "        <div class=\"col-sm-12\">\n" +
    "            <div data-ng-include=\"'partials/breadcrumbs.html'\"></div>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "\n" +
    "    <h1>Edit {{ group.name }}</h1>\n" +
    "    <br>\n" +
    "\n" +
    "    <form ng-submit=\"submitGroupForm()\">\n" +
    "        <div class=\"row\">\n" +
    "\n" +
    "            <!-- Edit form -->\n" +
    "            <div class=\"col-sm-12 col-md-8\">\n" +
    "                <div data-ng-include=\"'group/partials/form.html'\"></div>\n" +
    "            </div>\n" +
    "\n" +
    "            <!-- Team picture -->\n" +
    "        	<div class=\"col-sm-12 col-md-4\">\n" +
    "                <div data-ng-include src=\"'group/partials/upload-photo.html'\"></div>\n" +
    "        	</div>\n" +
    "        </div>\n" +
    "\n" +
    "        <!-- Action buttons -->\n" +
    "        <div class=\"row\">\n" +
    "            <div class=\"col-sm-12 text-center\">\n" +
    "                <button type=\"submit\" class=\"btn btn-default\">Save Changes</button>\n" +
    "            </div>\n" +
    "        </div>\n" +
    "    </form>\n" +
    "\n" +
    "    <!-- Debug -->\n" +
    "    <div ng-show=\"global.isLocal\">\n" +
    "        <br><br>\n" +
    "        <div class=\"panel panel-default\">\n" +
    "            <div class=\"panel-heading\">Debug</div>\n" +
    "            <div class=\"panel-body\">\n" +
    "                Group data: {{ group }} <br><br>\n" +
    "                File data: {{ avatar }}\n" +
    "            </div>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("group/list.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("group/list.html",
    "<!-- Copyright Heddoko(TM) 2015, all rights reserved. -->\n" +
    "\n" +
    "<div class=\"page group-page\">\n" +
    "    <header>\n" +
    "        <h1>\n" +
    "            Teams <i class=\"fa fa-users\"></i>\n" +
    "            <span></span>\n" +
    "        </h1>\n" +
    "    </header>\n" +
    "\n" +
    "    <div ng-if=\"global.data.isFetchingGroups === false\" class=\"row\">\n" +
    "\n" +
    "        <!-- Group list -->\n" +
    "        <ui-filesystem\n" +
    "            data-id=\"groups-explorer\"\n" +
    "            data-files=\"groupList\"\n" +
    "            data-default-layout=\"large-tiles\"\n" +
    "            data-hide-location=\"true\"\n" +
    "            data-config=\"uiFilesystemConfig\"\n" +
    "            data-is-loading=\"isLoading\"\n" +
    "            class=\"col-sm-12\">\n" +
    "        </ui-filesystem>\n" +
    "\n" +
    "        <!-- New group modal form -->\n" +
    "        <div class=\"modal fade\" id=\"createGroupForm\">\n" +
    "            <div class=\"modal-dialog\">\n" +
    "                <div class=\"modal-content\">\n" +
    "                    <div class=\"modal-header\">\n" +
    "                        Add a Team\n" +
    "                    </div>\n" +
    "\n" +
    "                    <div class=\"modal-body\">\n" +
    "\n" +
    "                        <!-- Name -->\n" +
    "                        <div class=\"form-group\">\n" +
    "                            <input\n" +
    "                                type=\"text\"\n" +
    "                                ng-model=\"newGroup.name\"\n" +
    "                                placeholder=\"Name\"\n" +
    "                                class=\"form-control\"\n" +
    "                                required>\n" +
    "                        </div>\n" +
    "\n" +
    "                        <!-- Sport -->\n" +
    "                        <div class=\"form-group\">\n" +
    "                            <input\n" +
    "                                type=\"text\"\n" +
    "                                placeholder=\"Sport\"\n" +
    "                                class=\"form-control\"\n" +
    "                                disabled>\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "\n" +
    "                    <div class=\"modal-footer text-center\">\n" +
    "                        <button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\">\n" +
    "                            Cancel\n" +
    "                        </button>\n" +
    "                        <button ng-click=\"createGroup()\" type=\"button\" class=\"btn btn-primary\" data-dismiss=\"modal\">\n" +
    "                            Create\n" +
    "                        </button>\n" +
    "                    </div>\n" +
    "                </div>\n" +
    "            </div>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "\n" +
    "    <div ng-if=\"global.data.isFetchingGroups === true\">\n" +
    "        <ng-include src=\"'partials/loading.html'\"></ng-include>\n" +
    "    </div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("group/partials/form.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("group/partials/form.html",
    "<div class=\"panel panel-default\">\n" +
    "    <div class=\"panel-heading\">\n" +
    "        Team Details\n" +
    "    </div>\n" +
    "\n" +
    "    <div class=\"panel-body\">\n" +
    "\n" +
    "        <!-- Name -->\n" +
    "        <div class=\"form-group\">\n" +
    "            <input\n" +
    "                type=\"text\"\n" +
    "                ng-model=\"group.name\"\n" +
    "                placeholder=\"Name\"\n" +
    "                class=\"form-control\"\n" +
    "                required>\n" +
    "        </div>\n" +
    "\n" +
    "        <!-- Sport -->\n" +
    "        <!-- <div class=\"row form-group\">\n" +
    "            <div class=\"col-sm-4 col-md-2\">\n" +
    "                Sport\n" +
    "            </div>\n" +
    "            <div class=\"col-sm-8\">\n" +
    "                <select ng-model=\"group.sport_id\" class=\"form-control\" required>\n" +
    "                    <option\n" +
    "                        ng-repeat=\"sport in sports\"\n" +
    "                        ng-selected=\"sport.id === group.sport_id\"\n" +
    "                        value=\"{{ sport.id }}\">\n" +
    "                        {{ sport.name }}\n" +
    "                    </option>\n" +
    "                </select>\n" +
    "            </div>\n" +
    "        </div> -->\n" +
    "    </div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("group/partials/upload-photo.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("group/partials/upload-photo.html",
    "<div class=\"panel panel-default\">\n" +
    "    <div class=\"panel-heading\">\n" +
    "        Avatar\n" +
    "    </div>\n" +
    "\n" +
    "    <div class=\"panel-body\">\n" +
    "\n" +
    "        <!-- Avatar preview -->\n" +
    "        <div class=\"row\">\n" +
    "            <div class=\"col-sm-12\">\n" +
    "                <img\n" +
    "                    ng-show=\"avatar || group.photo_src\"\n" +
    "                    ngf-src=\"avatar || group.photo_src\"\n" +
    "                    style=\"width: 100%\">\n" +
    "            </div>\n" +
    "        </div>\n" +
    "        <br>\n" +
    "\n" +
    "        <!-- Action buttons -->\n" +
    "        <div class=\"row\">\n" +
    "            <div class=\"col-sm-12 text-center\">\n" +
    "                <span ng-show=\"group.id < 1\">\n" +
    "                    Please create the team <br>\n" +
    "                    before uploading a photo. <br><br>\n" +
    "                </span>\n" +
    "\n" +
    "                <span ng-show=\"group.id >= 1\">\n" +
    "                    Temporarily disabled. <br><br>\n" +
    "                </span>\n" +
    "\n" +
    "                <button\n" +
    "                    ngf-select=\"uploadPhoto($file)\"\n" +
    "                    ngf-drop\n" +
    "                    ng-model=\"avatar\"\n" +
    "                    accept=\"image/*\"\n" +
    "                    ngf-pattern=\"'image/*'\"\n" +
    "                    ngf-capture=\"'camera'\"\n" +
    "                    ngf-min-height=\"100\"\n" +
    "                    ngf-max-size=\"2MB\"\n" +
    "                    ng-class=\"{'btn btn-default': true, 'disabled': group.id < 1 || true}\">\n" +
    "\n" +
    "                    Select or drag &amp; drop a photo\n" +
    "                </button>\n" +
    "            </div>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("group/view.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("group/view.html",
    "<!-- Copyright Heddoko(TM) 2015, all rights reserved. -->\n" +
    "\n" +
    "<div ng-if=\"global.data.isFetchingGroups === false && group.id\" class=\"page group-page\">\n" +
    "    <header>\n" +
    "        <h1>\n" +
    "            {{ group.name }} <i class=\"fa fa-users\"></i>\n" +
    "            <span></span>\n" +
    "        </h1>\n" +
    "    </header>\n" +
    "\n" +
    "    <section>\n" +
    "    	<div class=\"row\">\n" +
    "\n" +
    "            <!-- Avatar -->\n" +
    "    		<div class=\"col-xs-12 col-md-5 col-lg-3\">\n" +
    "                <div class=\"text-center\">\n" +
    "                    <ui-avatar\n" +
    "                        data-upload-endpoint=\"uploadAvatarEndpoint\"\n" +
    "                        data-success-callback=\"uploadAvatarCallback\"\n" +
    "                        data-src=\"group.avatarSrc\">\n" +
    "                    </ui-avatar>\n" +
    "                </div>\n" +
    "\n" +
    "                <div class=\"text-right profile-shortcuts\">\n" +
    "\n" +
    "                    <!-- Delete group button -->\n" +
    "                    <a\n" +
    "                        ng-show=\"group.id > 0\"\n" +
    "                        data-toggle=\"modal\"\n" +
    "                        data-target=\"#deleteGroupConfirmation\"\n" +
    "                        href=\"javascript:;\">\n" +
    "\n" +
    "                        <button class=\"btn btn-danger btn-circle btn-lg\">\n" +
    "                            <i class=\"fa fa-trash\"></i>\n" +
    "                        </button>\n" +
    "                    </a>\n" +
    "\n" +
    "                    <!-- Delete confirmation -->\n" +
    "                    <div class=\"modal fade\" id=\"deleteGroupConfirmation\">\n" +
    "                        <div class=\"modal-dialog\">\n" +
    "                            <div class=\"modal-content\">\n" +
    "                                <div class=\"modal-body\">\n" +
    "                                    <p ng-show=\"global.state.profile.filtered.length > 0\">\n" +
    "                                        Are you sure you want to delete <b>{{ group.name }}</b>\n" +
    "                                        and the {{ global.state.profile.filtered.length }}\n" +
    "                                        associated athletes?\n" +
    "                                    </p>\n" +
    "                                    <p ng-show=\"global.state.profile.filtered.length === 0\">\n" +
    "                                        Are you sure you want to delete <b>{{ group.name }}</b>?\n" +
    "                                    </p>\n" +
    "                                </div>\n" +
    "                                <div class=\"modal-footer text-center\">\n" +
    "                                    <button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\">\n" +
    "                                        Cancel\n" +
    "                                    </button>\n" +
    "                                    <button ng-click=\"deleteGroup()\" type=\"button\" class=\"btn btn-danger\" data-dismiss=\"modal\">\n" +
    "                                        Yes, delete {{ group.name }}\n" +
    "                                    </button>\n" +
    "                                </div>\n" +
    "                            </div>\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "                </div>\n" +
    "    		</div>\n" +
    "\n" +
    "            <!-- Group details -->\n" +
    "            <div class=\"col-xs-12 col-md-7 col-lg-5\">\n" +
    "                <ui-editable-fields\n" +
    "                    data-id=\"groupDetails\"\n" +
    "                    data-model=\"group\"\n" +
    "                    data-save=\"saveGroupDetails\"\n" +
    "                    data-save-callback=\"saveGroupDetailsCallback\"\n" +
    "                    data-heading=\"Team Details\">\n" +
    "\n" +
    "                    <div class=\"row\">\n" +
    "\n" +
    "                        <!-- Group name -->\n" +
    "                        <div class=\"col-xs-6\">\n" +
    "                            <ui-editable-field\n" +
    "                                data-label=\"Name\"\n" +
    "                                data-key=\"name\"\n" +
    "                                data-required=\"true\">\n" +
    "                            </ui-editable-field>\n" +
    "                        </div>\n" +
    "\n" +
    "                        <!-- Tags -->\n" +
    "                        <div class=\"col-xs-6\">\n" +
    "                            <ui-editable-field\n" +
    "                                data-label=\"Sport\"\n" +
    "                                data-key=\"mainTag\"\n" +
    "                                data-type=\"tag\"\n" +
    "                                data-empty=\"Select a Sport\">\n" +
    "                            </ui-editable-field>\n" +
    "                        </div>\n" +
    "\n" +
    "                        <!-- Placeholder for Tags (NFL TechCrunch demo) -->\n" +
    "                        <div ng-if=\"false\" class=\"col-xs-6\">\n" +
    "                            <ui-editable-field\n" +
    "                                data-label=\"Sport\"\n" +
    "                                data-type=\"placeholder\"\n" +
    "                                data-display=\"Football\">\n" +
    "                            </ui-editable-field>\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "\n" +
    "                    <div class=\"row\">\n" +
    "\n" +
    "                        <!-- Managers -->\n" +
    "                        <div class=\"col-xs-6\">\n" +
    "                            <ui-editable-field\n" +
    "                                data-label=\"Head Coach\"\n" +
    "                                data-type=\"placeholder\"\n" +
    "                                data-display=\"Rick Springfield\">\n" +
    "                            </ui-editable-field>\n" +
    "                        </div>\n" +
    "\n" +
    "                        <!-- Profiles count -->\n" +
    "                        <div class=\"col-xs-6\">\n" +
    "                            <ui-editable-field\n" +
    "                                data-label=\"Number of athletes\"\n" +
    "                                data-type=\"placeholder\"\n" +
    "                                data-display=\"{{ global.data.filteredProfiles.length }}\">\n" +
    "                            </ui-editable-field>\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "\n" +
    "                    <div class=\"row\">\n" +
    "\n" +
    "                        <!-- Date created -->\n" +
    "                        <div class=\"col-xs-6\">\n" +
    "                            <ui-editable-field\n" +
    "                                data-label=\"Date Created\"\n" +
    "                                data-key=\"createdAt\"\n" +
    "                                data-type=\"date\"\n" +
    "                                data-required=\"true\">\n" +
    "                            </ui-editable-field>\n" +
    "                        </div>\n" +
    "\n" +
    "                        <!-- Date modified -->\n" +
    "                        <div class=\"col-xs-6\">\n" +
    "                            <ui-editable-field\n" +
    "                                data-label=\"Date Modified\"\n" +
    "                                data-key=\"updatedAt\"\n" +
    "                                data-type=\"date\"\n" +
    "                                data-required=\"true\">\n" +
    "                            </ui-editable-field>\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "                </ui-editable-fields>\n" +
    "            </div>\n" +
    "\n" +
    "            <!-- Notes -->\n" +
    "            <div class=\"col-xs-12 col-lg-4\">\n" +
    "                <ui-editable-standalone-field\n" +
    "                    data-heading=\"Notes\"\n" +
    "                    data-model=\"group\"\n" +
    "                    data-key=\"notes\"\n" +
    "                    data-empty=\"...\"\n" +
    "                    data-disabled=\"true\"\n" +
    "                    data-save=\"saveGroupDetails\"\n" +
    "                    data-save-callback=\"saveGroupDetailsCallback\">\n" +
    "                </ui-editable-standalone-field>\n" +
    "            </div>\n" +
    "\n" +
    "            <!-- Placeholder for Notes (NFL TechCrunch demo) -->\n" +
    "            <div ng-if=\"false\" class=\"col-xs-12 col-lg-4\">\n" +
    "                <div class=\"ui-editable-list-container\">\n" +
    "                    <h3 class=\"title\">\n" +
    "                        Notes\n" +
    "                        <span></span>\n" +
    "                    </h3>\n" +
    "                    <br>\n" +
    "\n" +
    "                    Wins: 12 / Losses: 4 / Ties: 0\n" +
    "                </div>\n" +
    "            </div>\n" +
    "        </div>\n" +
    "    </section>\n" +
    "\n" +
    "    <!-- Profile list -->\n" +
    "    <div class=\"row\">\n" +
    "        <div class=\"col-sm-12\">\n" +
    "            <h3 class=\"title\">\n" +
    "                Team Members\n" +
    "                <span></span>\n" +
    "            </h3>\n" +
    "            <br>\n" +
    "\n" +
    "            <!-- New profile button -->\n" +
    "            <!-- TODO: figure out why the right margin is needed -->\n" +
    "            <a\n" +
    "                href=\"#/profiles/create\"\n" +
    "                class=\"btn btn-info btn-metro\"\n" +
    "                style=\"margin-right: 0\">\n" +
    "\n" +
    "                <i class=\"fa fa-plus\"></i>\n" +
    "            </a>\n" +
    "\n" +
    "            <a\n" +
    "                ng-repeat=\"profile in global.data.filteredProfiles | limitTo:global.data.filteredProfiles.length track by profile.id\"\n" +
    "                href=\"#/profiles/{{ profile.id }}\"\n" +
    "                class=\"btn btn-info btn-metro\"\n" +
    "                style=\"background-image: url({{ ::profile.avatarSrc || '' }});\">\n" +
    "\n" +
    "                <span style=\"background-color: rgba(0, 0, 0, 0.4)\">{{ ::profile.lastName.toUpperCase() }}</span>\n" +
    "            </a>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "</div>\n" +
    "\n" +
    "<!-- Fetching group data -->\n" +
    "<div ng-if=\"global.data.isFetchingGroups === true\" class=\"page\">\n" +
    "    <ng-include src=\"'partials/loading.html'\"></ng-include>\n" +
    "</div>\n" +
    "\n" +
    "<!-- No group selected -->\n" +
    "<div ng-if=\"global.data.isFetchingGroups === false && group.id === 0\" class=\"page\">\n" +
    "    <h1 class=\"text-center\">No group selected</h1>\n" +
    "    <br>\n" +
    "\n" +
    "    <div class=\"text-center\">\n" +
    "        Please select a group to view its details.\n" +
    "        <br>\n" +
    "\n" +
    "        You can also take this opportunity to\n" +
    "        <a href=\"#/groups/create\">create one</a>.\n" +
    "    </div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("import/index.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("import/index.html",
    "<!-- Copyright Heddoko(TM) 2015, all rights reserved. -->\n" +
    "\n" +
    "<div class=\"page import-page\">\n" +
    "    <header>\n" +
    "        <h1>\n" +
    "            Upload <i class=\"fa fa-cloud-upload\"></i>\n" +
    "            <span></span>\n" +
    "        </h1>\n" +
    "\n" +
    "        <!-- Append profile name to page title -->\n" +
    "        <div ng-if=\"global.getSelectedProfile().id > 0\" class=\"h1-append\">\n" +
    "            for\n" +
    "            <a href=\"#/profiles/{{ global.getSelectedProfile().id }}\">\n" +
    "                {{ ::global.getSelectedProfile().firstName }}\n" +
    "                {{ ::global.getSelectedProfile().lastName }}\n" +
    "            </a>\n" +
    "\n" +
    "            <small>\n" +
    "                <a ng-click=\"global.store.profileId = 0\" href=\"javascript:;\">\n" +
    "                    (change)\n" +
    "                </a>\n" +
    "            </small>\n" +
    "        </div>\n" +
    "    </header>\n" +
    "\n" +
    "    <!-- Import page -->\n" +
    "    <div ng-if=\"global.getSelectedProfile().id > 0\">\n" +
    "\n" +
    "        <!-- Import new movement -->\n" +
    "        <div class=\"movement-import row\">\n" +
    "            <div class=\"col-md-2 col-md-offset-2 text-center\">\n" +
    "                <a\n" +
    "                    ngf-select=\"startImport($files)\"\n" +
    "                    ngf-drop=\"startImport($files)\"\n" +
    "                    accept=\".csv,.txt\"\n" +
    "                    ngf-max-size=\"1GB\"\n" +
    "                    multiple\n" +
    "                    class=\"btn btn-info btn-metro\"\n" +
    "                    style=\"margin-right: 0\">\n" +
    "\n" +
    "                    <i class=\"fa fa-plus\"></i>\n" +
    "                </a>\n" +
    "            </div>\n" +
    "\n" +
    "            <div ng-if=\"global.data.importedMovements.length === 0 && global.data.isImporting === false\" class=\"col-md-6\">\n" +
    "                <h3>\n" +
    "                    Upload your movement files.\n" +
    "                </h3>\n" +
    "\n" +
    "                Once you upload or capture a new movement, it will show up here.\n" +
    "                <br>\n" +
    "                <br>\n" +
    "\n" +
    "                <b>\n" +
    "                    Go ahead and use the <i class=\"fa fa-plus fa-lg fa-fw\"></i> button to import\n" +
    "                    your data files.\n" +
    "                </b>\n" +
    "            </div>\n" +
    "\n" +
    "            <!-- Uploading movement file -->\n" +
    "            <div ng-if=\"global.data.isImporting === true\" class=\"col-md-6\">\n" +
    "\n" +
    "                <!-- Import status -->\n" +
    "                <h3 class=\"text-center\">\n" +
    "                    {{ global.data.import.status }}\n" +
    "\n" +
    "                    <br>\n" +
    "                    <small ng-show=\"global.data.import.queue.length > 1\">\n" +
    "                        {{ global.data.import.queue.length }} movement files left.\n" +
    "                    </small>\n" +
    "                    <small ng-show=\"global.data.import.queue.length <= 1\">\n" +
    "                        Almost done...\n" +
    "                    </small>\n" +
    "                </h3>\n" +
    "\n" +
    "                <!-- Import progress -->\n" +
    "                <div class=\"col-xs-10 col-xs-offset-1 col-md-6 col-md-offset-3 col-lg-4 col-lg-offset-4\">\n" +
    "                    <div class=\"progress\">\n" +
    "                        <div\n" +
    "                            class=\"progress-bar progress-bar-info progress-bar-striped active\"\n" +
    "                            role=\"progressbar\"\n" +
    "                            aria-valuenow=\"{{ global.data.import.progress }}\"\n" +
    "                            aria-valuemin=\"0\"\n" +
    "                            aria-valuemax=\"100\"\n" +
    "                            style=\"min-width: 2em;\"\n" +
    "                            ng-style=\"{width: global.data.import.progress + '%'}\">\n" +
    "\n" +
    "                            {{ global.data.import.progress }}%\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "                </div>\n" +
    "\n" +
    "                <!-- Import actions -->\n" +
    "                <br>\n" +
    "                <div class=\"col-xs-12 text-center\">\n" +
    "                    <div class=\"btn-group\">\n" +
    "                        <button\n" +
    "                            ng-disabled=\"global.data.importedMovements.length === 0\"\n" +
    "                            ng-click=\"global.data.importedMovements = []\"\n" +
    "                            type=\"button\"\n" +
    "                            class=\"btn btn-default\">\n" +
    "\n" +
    "                            Clear List\n" +
    "                        </button>\n" +
    "\n" +
    "                        <button\n" +
    "                            ng-disabled=\"global.data.import.queue.length <= 1\"\n" +
    "                            ng-click=\"global.data.import.queue = []\"\n" +
    "                            type=\"button\"\n" +
    "                            class=\"btn btn-danger\">\n" +
    "\n" +
    "                            Stop Import\n" +
    "                        </button>\n" +
    "                    </div>\n" +
    "                </div>\n" +
    "            </div>\n" +
    "        </div>\n" +
    "\n" +
    "        <div ng-show=\"global.data.importedMovements.length > 0\">\n" +
    "\n" +
    "            <!-- List of uploaded movements -->\n" +
    "            <div ng-repeat=\"file in global.data.importedMovements\" ng-if=\"file && file.id\" class=\"movement-import row\">\n" +
    "\n" +
    "                <div ng-show=\"!file.isUpdating\">\n" +
    "\n" +
    "                    <!-- Movement preview -->\n" +
    "                    <div class=\"col-md-2 col-md-offset-2\">\n" +
    "                        <div class=\"row\">\n" +
    "                            <div class=\"col-xs-12\">\n" +
    "                                <ui-movement-preview data-aspect-ratio=\"4:3\"></ui-movement-preview>\n" +
    "                            </div>\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "\n" +
    "                    <div class=\"col-md-6\">\n" +
    "\n" +
    "                        <!-- Movement title -->\n" +
    "                        <div class=\"row\">\n" +
    "                            <div class=\"col-xs-12\">\n" +
    "                                <ui-editable-standalone-field\n" +
    "                                    data-model=\"file\"\n" +
    "                                    data-key=\"title\"\n" +
    "                                    data-empty=\"Movement Title\"\n" +
    "                                    data-input-type=\"text\"\n" +
    "                                    data-save=\"saveMovementFile\"\n" +
    "                                    data-save-callback=\"saveMovementFileCallback\">\n" +
    "                                </ui-editable-standalone-field>\n" +
    "                            </div>\n" +
    "                        </div>\n" +
    "\n" +
    "                        <!-- Tags -->\n" +
    "                        <div class=\"row\">\n" +
    "                            <div class=\"col-xs-12\">\n" +
    "                                <ui-taggable-input\n" +
    "                                    data-model=\"file\"\n" +
    "                                    data-key=\"tags\"\n" +
    "                                    data-max-tags=\"20\">\n" +
    "                                </ui-taggable-input>\n" +
    "                            </div>\n" +
    "                        </div>\n" +
    "\n" +
    "                        <!-- Action buttons -->\n" +
    "                        <br>\n" +
    "                        <div class=\"row\">\n" +
    "                            <div class=\"col-xs-12\">\n" +
    "\n" +
    "                                <!-- Thumbnail link -->\n" +
    "                                <a\n" +
    "                                    ng-click=\"selectThumbnail()\"\n" +
    "                                    href=\"javascript:;\"\n" +
    "                                    class=\"btn btn-lg btn-default\">\n" +
    "\n" +
    "                                    <i class=\"fa fa-picture-o fa-fw\"></i>\n" +
    "                                </a>\n" +
    "\n" +
    "                                <!-- Edit -->\n" +
    "                                <a\n" +
    "                                    ng-click=\"editMovement()\"\n" +
    "                                    href=\"javascript:;\"\n" +
    "                                    class=\"btn btn-lg btn-default\">\n" +
    "\n" +
    "                                    <i class=\"fa fa-pencil fa-fw\"></i>\n" +
    "                                </a>\n" +
    "\n" +
    "                                <!-- Delete -->\n" +
    "                                <a\n" +
    "                                    ng-click=\"deleteMovement(file)\"\n" +
    "                                    href=\"javascript:;\"\n" +
    "                                    class=\"btn btn-lg btn-danger\">\n" +
    "\n" +
    "                                    <i class=\"fa fa-trash-o fa-fw\"></i>\n" +
    "                                </a>\n" +
    "                            </div>\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "                </div>\n" +
    "\n" +
    "                <!-- Updating movement details -->\n" +
    "                <div ng-show=\"file.isUpdating\" class=\"text-center\" style=\"margin: 50px auto;\">\n" +
    "                    <i class=\"fa fa-spinner fa-spin fa-2x text-muted\"></i>\n" +
    "                </div>\n" +
    "            </div>\n" +
    "\n" +
    "            <!-- Clear button -->\n" +
    "            <div class=\"movement-import row\">\n" +
    "                <div class=\"col-sm-12 text-center\">\n" +
    "                    <button\n" +
    "                        ng-click=\"global.data.importedMovements = []\"\n" +
    "                        type=\"button\"\n" +
    "                        class=\"btn btn-info\">\n" +
    "\n" +
    "                        Clear List\n" +
    "                    </button>\n" +
    "                </div>\n" +
    "            </div>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "\n" +
    "    <!-- Loading profiles -->\n" +
    "    <div ng-if=\"global.data.isFetchingProfiles\">\n" +
    "        <div ng-include=\"'partials/loading.html'\"></div>\n" +
    "    </div>\n" +
    "\n" +
    "    <!-- No profile selected -->\n" +
    "    <div ng-if=\"!global.data.isFetchingProfiles && global.getSelectedProfile().id === 0\">\n" +
    "        <div ng-include=\"'partials/select-profile.html'\"></div>\n" +
    "    </div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("movements/analysis-demo/index.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("movements/analysis-demo/index.html",
    "<div class=\"page demo-analysis-page\">\n" +
    "    <header>\n" +
    "        <h1>\n" +
    "            Analyze <i class=\"fa fa-line-chart\"></i>\n" +
    "            <span></span>\n" +
    "        </h1>\n" +
    "    </header>\n" +
    "\n" +
    "    <div class=\"row\">\n" +
    "\n" +
    "        <div class=\"col-sm-10 col-sm-offset-1\">\n" +
    "\n" +
    "            <!-- Emphasized view -->\n" +
    "            <div class=\"col-sm-8 panel panel-default\" id=\"emphasizedViewContainer\">\n" +
    "                <div class=\"panel-heading text-right\">\n" +
    "                    Side/Sagittal\n" +
    "                </div>\n" +
    "\n" +
    "                <div class=\"panel-body\">\n" +
    "                    <video id=\"sagittalVideo\" class=\"demo-analysis\">\n" +
    "                        <source ng-src=\"../../../../demo/aslr/analysis/sagittal.webm\" type=\"video/webm\" />\n" +
    "                        <source src=\"../../../../demo/aslr/analysis/sagittal.mp4\" type=\"video/mp4\" />\n" +
    "                        Sorry, your browser doesn't support HTML5 video.\n" +
    "                    </video>\n" +
    "                </div>\n" +
    "\n" +
    "                <div class=\"demo-controls\">\n" +
    "                    <a ng-hide=\"isPlaying\" ng-click=\"play()\" href=\"javascript:;\">\n" +
    "                        <i class=\"fa fa-lg fa-play\"></i>\n" +
    "                    </a>\n" +
    "                    <a ng-show=\"isPlaying\" ng-click=\"pause()\" href=\"javascript:;\">\n" +
    "                        <i class=\"fa fa-lg fa-pause\"></i>\n" +
    "                    </a>\n" +
    "                    <a ng-click=\"backward()\" href=\"javascript:;\">\n" +
    "                        <i class=\"fa fa-lg fa-backward\"></i>\n" +
    "                    </a>\n" +
    "                    <a ng-click=\"forward()\" href=\"javascript:;\">\n" +
    "                        <i class=\"fa fa-lg fa-forward\"></i>\n" +
    "                    </a>\n" +
    "                    <a ng-click=\"reset()\" href=\"javascript:;\">\n" +
    "                        <i class=\"fa fa-lg fa-refresh\"></i>\n" +
    "                    </a>\n" +
    "                </div>\n" +
    "            </div>\n" +
    "\n" +
    "            <!-- Secondary views -->\n" +
    "            <div class=\"col-sm-4\" id=\"secondaryViewsContainer\">\n" +
    "\n" +
    "                <!-- Coronal view -->\n" +
    "                <div class=\"col-sm-12 panel panel-default\">\n" +
    "                    <div class=\"panel-heading text-right\">\n" +
    "                        Front/Coronal\n" +
    "                    </div>\n" +
    "\n" +
    "                    <div class=\"panel-body\">\n" +
    "                        <video id=\"coronalVideo\" class=\"demo-analysis\">\n" +
    "                            <source ng-src=\"{{'../../../../demo/aslr/left/coronal.webm'}}\" type=\"video/webm\" />\n" +
    "                            <source src=\"{{'../../../../demo/aslr/left/coronal.mp4'}}\" type=\"video/mp4\" />\n" +
    "                            Sorry, your browser doesn't support HTML5 video.\n" +
    "                        </video>\n" +
    "                    </div>\n" +
    "                </div>\n" +
    "\n" +
    "                <!-- Transverse view -->\n" +
    "                <div class=\"col-sm-12 panel panel-default\">\n" +
    "                    <div class=\"panel-heading text-right\">\n" +
    "                        Horizontal/Transverse\n" +
    "                    </div>\n" +
    "\n" +
    "                    <div class=\"panel-body\">\n" +
    "                        <video id=\"transverseVideo\" class=\"demo-analysis\">\n" +
    "                            <source ng-src=\"{{'../../../../demo/aslr/left/transverse.webm'}}\" type=\"video/webm\" />\n" +
    "                            <source src=\"{{'../../../../demo/aslr/left/transverse.mp4'}}\" type=\"video/mp4\" />\n" +
    "                            Sorry, your browser doesn't support HTML5 video.\n" +
    "                        </video>\n" +
    "                    </div>\n" +
    "                </div>\n" +
    "            </div>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("movements/analysis/index.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("movements/analysis/index.html",
    "<!-- Copyright Heddoko(TM) 2015, all rights reserved. -->\n" +
    "\n" +
    "<div class=\"page analysis-page\">\n" +
    "    <header>\n" +
    "        <h1>\n" +
    "            Analyze <i class=\"fa fa-line-chart\"></i>\n" +
    "            <span></span>\n" +
    "        </h1>\n" +
    "    </header>\n" +
    "\n" +
    "    <!-- Analysis container -->\n" +
    "    <div ng-if=\"!global.data.isFetchingAnalysisMovement && movement\" class=\"page-padded\">\n" +
    "\n" +
    "        <!-- Title -->\n" +
    "        <div class=\"row\">\n" +
    "            <div class=\"col-lg-6\">\n" +
    "                <h3 class=\"title\">\n" +
    "                    <ui-editable-standalone-field\n" +
    "                        data-model=\"movement\"\n" +
    "                        data-key=\"title\"\n" +
    "                        data-empty=\"Movement Title\"\n" +
    "                        data-input-type=\"text\"\n" +
    "                        data-save=\"saveMovementDetails\"\n" +
    "                        data-save-callback=\"saveMovementDetailsCallback\">\n" +
    "                    </ui-editable-standalone-field>\n" +
    "\n" +
    "                    <span></span>\n" +
    "                </h3>\n" +
    "            </div>\n" +
    "        </div>\n" +
    "\n" +
    "        <!-- Layout -->\n" +
    "        <ng-include src=\"'movements/analysis/layouts/' + layout + '.html'\"></ng-include>\n" +
    "\n" +
    "        <!-- Movement details -->\n" +
    "        <div class=\"row no-gutter\">\n" +
    "            <div class=\"col-sm-12\">\n" +
    "\n" +
    "            </div>\n" +
    "        </div>\n" +
    "\n" +
    "    </div>\n" +
    "\n" +
    "    <!-- Retrieving movement data -->\n" +
    "    <div ng-if=\"global.data.isFetchingAnalysisMovement\">\n" +
    "        <ng-include src=\"'partials/loading.html'\"></ng-include>\n" +
    "    </div>\n" +
    "\n" +
    "    <!-- Movement not found -->\n" +
    "    <div ng-if=\"!global.data.isFetchingAnalysisMovement && !movement\">\n" +
    "        <h2 class=\"text-center\">\n" +
    "            An Error Occurred :(\n" +
    "        </h2>\n" +
    "    </div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("movements/analysis/layouts/emphasis.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("movements/analysis/layouts/emphasis.html",
    "<!-- Copyright Heddoko(TM) 2016, all rights reserved. -->\n" +
    "\n" +
    "<div class=\"emphasis-layout\">\n" +
    "\n" +
    "    <!-- Main container -->\n" +
    "    <div class=\"main-container\">\n" +
    "\n" +
    "        <!-- Secondary views -->\n" +
    "        <div class=\"secondary-views\">\n" +
    "            <div class=\"movement-playback-container\">\n" +
    "                <ui-movement-playback\n" +
    "                    data-title=\"'Side / Sagittal'\"\n" +
    "                    data-height=\"'300px'\"\n" +
    "                    data-previous-movement=\"false\"\n" +
    "                    data-next-movement=\"false\">\n" +
    "                </ui-movement-playback>\n" +
    "            </div>\n" +
    "\n" +
    "            <div class=\"movement-playback-container\">\n" +
    "                <ui-movement-playback\n" +
    "                    data-title=\"'Horizontal / Transverse'\"\n" +
    "                    data-height=\"'300px'\"\n" +
    "                    data-previous-movement=\"false\"\n" +
    "                    data-next-movement=\"false\">\n" +
    "                </ui-movement-playback>\n" +
    "            </div>\n" +
    "        </div>\n" +
    "\n" +
    "        <!-- Primary view -->\n" +
    "        <div class=\"primary-view\">\n" +
    "            <div class=\"movement-playback-container\">\n" +
    "                <ui-movement-playback\n" +
    "                    data-title=\"'Front / Coronal'\"\n" +
    "                    data-height=\"'605px'\"\n" +
    "                    data-previous-movement=\"false\"\n" +
    "                    data-next-movement=\"false\">\n" +
    "                </ui-movement-playback>\n" +
    "            </div>\n" +
    "        </div>\n" +
    "\n" +
    "        <!-- Menu -->\n" +
    "        <div class=\"menu-container\">\n" +
    "            <a href=\"javascript:;\" class=\"btn btn-flat\">\n" +
    "                <i class=\"fa fa-desktop\"></i>\n" +
    "                <span>\n" +
    "                    Screen <br>\n" +
    "                    Recording\n" +
    "                </span>\n" +
    "            </a>\n" +
    "\n" +
    "            <a href=\"javascript:;\" class=\"btn btn-flat\">\n" +
    "                <i class=\"fa fa-camera\"></i>\n" +
    "                <span>Screenshot</span>\n" +
    "            </a>\n" +
    "\n" +
    "            <a href=\"javascript:;\" class=\"btn btn-flat\">\n" +
    "                <i class=\"fa fa-microphone\"></i>\n" +
    "                <span>Voice</span>\n" +
    "            </a>\n" +
    "\n" +
    "            <a href=\"javascript:;\" class=\"btn btn-flat\">\n" +
    "                <i class=\"fa fa-comment\"></i>\n" +
    "                <span>Comment</span>\n" +
    "            </a>\n" +
    "\n" +
    "            <a href=\"javascript:;\" class=\"btn btn-flat\">\n" +
    "                <i class=\"fa fa-search-plus\"></i>\n" +
    "                <span>Zoom In</span>\n" +
    "            </a>\n" +
    "\n" +
    "            <a href=\"javascript:;\" class=\"btn btn-flat\">\n" +
    "                <i class=\"fa fa-search-minus\"></i>\n" +
    "                <span>Zoom Out</span>\n" +
    "            </a>\n" +
    "\n" +
    "            <a href=\"javascript:;\" class=\"btn btn-flat\">\n" +
    "                <i class=\"fa fa-hand-paper-o\"></i>\n" +
    "                <span>Rotate</span>\n" +
    "            </a>\n" +
    "\n" +
    "            <a href=\"javascript:;\" class=\"btn btn-flat\">\n" +
    "                <i class=\"fa fa-pencil\"></i>\n" +
    "                <span>Edit</span>\n" +
    "            </a>\n" +
    "\n" +
    "            <a href=\"javascript:;\" class=\"btn btn-flat\">\n" +
    "                <i class=\"fa fa-random\"></i>\n" +
    "                <span>Compare</span>\n" +
    "            </a>\n" +
    "\n" +
    "            <a href=\"javascript:;\" class=\"btn btn-flat\">\n" +
    "                <i class=\"fa fa-share-alt\"></i>\n" +
    "                <span>Share</span>\n" +
    "            </a>\n" +
    "\n" +
    "            <a href=\"javascript:;\" class=\"btn btn-flat\">\n" +
    "                <i class=\"fa fa-trash\"></i>\n" +
    "                <span>Delete</span>\n" +
    "            </a>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "\n" +
    "    <!-- Movement details -->\n" +
    "    <div class=\"row movement-meta-row\">\n" +
    "\n" +
    "        <!-- Profile details -->\n" +
    "        <div class=\"col-lg-4\">\n" +
    "            <div class=\"row no-gutter\">\n" +
    "                <div class=\"col-md-5\">\n" +
    "                    <a\n" +
    "                        href=\"#/profiles/{{ profile.id }}\"\n" +
    "                        class=\"aspect-ratio aspect-4-3 card profile-avatar\"\n" +
    "                        style=\"background-image: url({{ profile.avatarSrc || '' }})\">\n" +
    "                    </a>\n" +
    "                </div>\n" +
    "\n" +
    "                <div class=\"col-md-7\">\n" +
    "                    <div class=\"profile-details\">\n" +
    "                        <div>\n" +
    "                            <a href=\"#/profiles/{{ profile.id }}\">\n" +
    "                                {{ profile.lastName.toUpperCase() }}\n" +
    "                            </a>\n" +
    "                        </div>\n" +
    "\n" +
    "                        <!-- Main group -->\n" +
    "                        <div ng-if=\"profile.groups.length > 0\">\n" +
    "                            <a href=\"#/groups/{{ profile.groups[0].id }}\">\n" +
    "                                {{ profile.groups[0].name }}\n" +
    "                            </a>\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "                </div>\n" +
    "            </div>\n" +
    "        </div>\n" +
    "\n" +
    "        <div class=\"col-lg-8 movement-details\">\n" +
    "            <div class=\"row\">\n" +
    "\n" +
    "                <div class=\"col-sm-3\">\n" +
    "\n" +
    "                    <!-- Date -->\n" +
    "                    <span style=\"font-size: 0.8em\">\n" +
    "                        {{ movement.createdAt | mysqlDate:'MMM d yyyy, h:mm a' }}\n" +
    "                    </span>\n" +
    "\n" +
    "                    <!-- Tags -->\n" +
    "                    <ui-editable-standalone-field\n" +
    "                        data-model=\"movement\"\n" +
    "                        data-key=\"tags\"\n" +
    "                        data-empty=\"Tags:\"\n" +
    "                        data-input-type=\"tag-title\"\n" +
    "                        data-max-tags=\"20\"\n" +
    "                        data-save=\"saveMovementDetails\"\n" +
    "                        data-save-callback=\"saveMovementDetailsCallback\">\n" +
    "                    </ui-editable-standalone-field>\n" +
    "\n" +
    "                </div>\n" +
    "\n" +
    "                <!-- Notes -->\n" +
    "                <div class=\"col-sm-6\">\n" +
    "                    <ui-editable-standalone-field\n" +
    "                        data-model=\"movement.meta\"\n" +
    "                        data-key=\"notes\"\n" +
    "                        data-empty=\"Notes:\"\n" +
    "                        data-input-type=\"textarea\"\n" +
    "                        data-save=\"saveMovementDetails\"\n" +
    "                        data-save-callback=\"saveMovementDetailsCallback\">\n" +
    "                    </ui-editable-standalone-field>\n" +
    "                </div>\n" +
    "\n" +
    "                <!-- Buttons -->\n" +
    "                <div class=\"col-sm-3\">\n" +
    "\n" +
    "                </div>\n" +
    "            </div>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("movements/comparison/index.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("movements/comparison/index.html",
    "<div class=\"page\">\n" +
    "    <header>\n" +
    "        <h1>\n" +
    "            Compare\n" +
    "            <span></span>\n" +
    "        </h1>\n" +
    "    </header>\n" +
    "\n" +
    "    <h2 style=\"text-align: center; color: #ccc;\">\n" +
    "        In Development\n" +
    "    </h2>\n" +
    "</div>\n" +
    "");
}]);

angular.module("movements/explorer/index.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("movements/explorer/index.html",
    "<!-- Copyright Heddoko(TM) 2015, all rights reserved. -->\n" +
    "\n" +
    "<div class=\"page movements-page\">\n" +
    "    <header>\n" +
    "        <h1>\n" +
    "            Organize\n" +
    "            <i class=\"fa fa-th\"></i>\n" +
    "            <span></span>\n" +
    "        </h1>\n" +
    "    </header>\n" +
    "\n" +
    "    <!-- Main container -->\n" +
    "    <div\n" +
    "        ng-if=\"global.getDataLength('profile') > 0 || global.data.isFetchingProfiles\"\n" +
    "        class=\"row file-explorer-container\">\n" +
    "\n" +
    "        <ui-filesystem\n" +
    "            data-id=\"movements-explorer\"\n" +
    "            data-files=\"files\"\n" +
    "            data-folders=\"folders\"\n" +
    "            data-parent-folder=\"parentFolder\"\n" +
    "            data-path=\"path\"\n" +
    "            data-config=\"uiFilesystemConfig\"\n" +
    "            data-default-layout=\"small-tiles\"\n" +
    "            data-is-loading=\"global.data.isFetchingMovementData || global.data.isFetchingProfiles\"\n" +
    "            class=\"col-sm-12\">\n" +
    "        </ui-filesystem>\n" +
    "\n" +
    "        <!-- New folder modal form -->\n" +
    "        <div class=\"modal fade\" id=\"createFolderForm\">\n" +
    "            <div class=\"modal-dialog\">\n" +
    "                <div class=\"modal-content\">\n" +
    "                    <div class=\"modal-header\">\n" +
    "                        Create a Folder\n" +
    "                    </div>\n" +
    "\n" +
    "                    <div class=\"modal-body\">\n" +
    "\n" +
    "                        <!-- Name -->\n" +
    "                        <div class=\"form-group\">\n" +
    "                            <input\n" +
    "                                type=\"text\"\n" +
    "                                ng-model=\"newFolder.name\"\n" +
    "                                placeholder=\"Folder Name\"\n" +
    "                                class=\"form-control\"\n" +
    "                                required>\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "\n" +
    "                    <div class=\"modal-footer text-center\">\n" +
    "                        <button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\">\n" +
    "                            Cancel\n" +
    "                        </button>\n" +
    "                        <button\n" +
    "                            ng-click=\"createFolder(newFolder.name)\"\n" +
    "                            type=\"button\"\n" +
    "                            class=\"btn btn-primary\"\n" +
    "                            data-dismiss=\"modal\">\n" +
    "\n" +
    "                            Create\n" +
    "                        </button>\n" +
    "                    </div>\n" +
    "                </div>\n" +
    "            </div>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "\n" +
    "    <!-- No profile exists -->\n" +
    "    <div\n" +
    "        ng-show=\"!global.data.isFetchingProfiles && global.getDataLength('profile') === 0\"\n" +
    "        class=\"text-center\">\n" +
    "\n" +
    "        <h3 class=\"text-center\">You do not have any profiles yet.</h3>\n" +
    "        <br>\n" +
    "\n" +
    "        Once you create a profile, you'll be able to upload and organize your movements here.\n" +
    "        <br>\n" +
    "        <br>\n" +
    "\n" +
    "        Take this opportunity to <a href=\"#/profiles/create\">create one</a>.\n" +
    "    </div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("overlay/movement-editor/footer.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("overlay/movement-editor/footer.html",
    "<button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\">Cancel</button>\n" +
    "<button type=\"button\" class=\"btn btn-primary\" data-dismiss=\"modal\">Save changes</button>\n" +
    "");
}]);

angular.module("overlay/movement-editor/index.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("overlay/movement-editor/index.html",
    "<h3 style=\"color: #ccc; text-align: center; margin: 100px auto\">\n" +
    "    In Development.\n" +
    "</h3>\n" +
    "");
}]);

angular.module("overlay/thumbnail-selector/footer.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("overlay/thumbnail-selector/footer.html",
    "<button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\">Cancel</button>\n" +
    "<button type=\"button\" class=\"btn btn-primary\" data-dismiss=\"modal\">Use as thumbnail</button>\n" +
    "");
}]);

angular.module("overlay/thumbnail-selector/index.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("overlay/thumbnail-selector/index.html",
    "<h3 style=\"color: #ccc; text-align: center; margin: 100px auto\">\n" +
    "    In Development.\n" +
    "</h3>\n" +
    "");
}]);

angular.module("partials/breadcrumbs.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("partials/breadcrumbs.html",
    "<!-- Copyright Heddoko(TM) 2015, all rights reserved. -->\n" +
    "\n" +
    "<ol class=\"breadcrumb-alt\">\n" +
    "    <li>\n" +
    "        <a class=\"active\" href=\"#/dashboard\">\n" +
    "            Dashboard\n" +
    "        </a>\n" +
    "    </li>\n" +
    "    <li ng-show=\"global.store.groupId > 0\">\n" +
    "        <a class=\"active\" href=\"#/groups/list\">\n" +
    "            Teams\n" +
    "        </a>\n" +
    "    </li>\n" +
    "    <li ng-show=\"isProfilePage && global.store.groupId > 0\">\n" +
    "        <a class=\"active\" href=\"javascript:;\" ng-click=\"global.browseTo.group()\">\n" +
    "            {{ global.state.group.list[global.store.groupId].name }}\n" +
    "        </a>\n" +
    "    </li>\n" +
    "    <li ng-show=\"currentPath == '/profiles/view' && global.store.profileId > 0\">\n" +
    "        <a class=\"active\" href=\"javascript:;\" ng-click=\"global.browseTo.profile()\">\n" +
    "            {{ global.state.profile.list[global.store.profileId].firstName }}\n" +
    "        </a>\n" +
    "    </li>\n" +
    "    <li ng-show=\"currentPath == '/groups/create'\">\n" +
    "        <a class=\"active\" href=\"javascript:;\" ng-click=\"\">\n" +
    "            Create a Team\n" +
    "        </a>\n" +
    "    </li>\n" +
    "    <li ng-show=\"currentPath == '/profiles/create'\">\n" +
    "        <a class=\"active\" href=\"javascript:;\" ng-click=\"\">\n" +
    "            Add an athlete\n" +
    "        </a>\n" +
    "    </li>\n" +
    "</ol>\n" +
    "");
}]);

angular.module("partials/directives/ui-avatar.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("partials/directives/ui-avatar.html",
    "<!-- Copyright Heddoko(TM) 2015, all rights reserved. -->\n" +
    "\n" +
    "<div class=\"ui-avatar-container\" ng-style=\"{width: containerWidth}\">\n" +
    "\n" +
    "    <!-- Avatar container -->\n" +
    "    <a\n" +
    "        href=\"javascript:;\"\n" +
    "        ngf-select=\"upload($file)\"\n" +
    "        ngf-drop\n" +
    "        accept=\"image/*\"\n" +
    "        ngf-pattern=\"'image/*'\"\n" +
    "        ngf-capture=\"'camera'\"\n" +
    "        ngf-min-height=\"100\"\n" +
    "        ngf-max-size=\"2MB\"\n" +
    "        class=\"aspect-ratio aspect-square ui-avatar-button\"\n" +
    "        ng-style=\"{'background-image': 'url(' + avatarSrc + ')'}\">\n" +
    "        <div>\n" +
    "\n" +
    "            <!-- Loading animation -->\n" +
    "            <div ng-show=\"status == 'uploading'\" class=\"ui-avatar-placeholder uploading\">\n" +
    "                <i class=\"fa fa-spinner fa-spin\"></i>\n" +
    "            </div>\n" +
    "\n" +
    "            <!-- Avatar placeholder -->\n" +
    "            <div ng-show=\"status == 'none'\" class=\"ui-avatar-placeholder\">\n" +
    "                <i class=\"fa fa-user fa-2x\"></i>\n" +
    "            </div>\n" +
    "\n" +
    "            <!-- Avatar actions -->\n" +
    "            <div class=\"ui-avatar-actions\">\n" +
    "                <i class=\"fa fa-camera fa-fw ui-avatar-icon\"></i>\n" +
    "                <span class=\"ui-avatar-text\">Update Avatar</span>\n" +
    "            </div>\n" +
    "\n" +
    "        </div>\n" +
    "    </a>\n" +
    "</div>\n" +
    "");
}]);

angular.module("partials/directives/ui-editable-fields/btn-group.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("partials/directives/ui-editable-fields/btn-group.html",
    "<!-- Copyright Heddoko(TM) 2015, all rights reserved. -->\n" +
    "\n" +
    "<span class=\"input-group-btn\">\n" +
    "    <button ng-click=\"cancel()\" title=\"Cancel\" class=\"btn btn-danger\" type=\"button\">\n" +
    "        <i class=\"fa fa-times\"></i>\n" +
    "    </button>\n" +
    "    <button ng-click=\"save()\" title=\"Save\" class=\"btn btn-default\" type=\"button\">\n" +
    "        <i class=\"fa fa-floppy-o\"></i>\n" +
    "    </button>\n" +
    "</span>\n" +
    "");
}]);

angular.module("partials/directives/ui-editable-fields/field-horizontal.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("partials/directives/ui-editable-fields/field-horizontal.html",
    "<!-- Copyright Heddoko(TM) 2015, all rights reserved. -->\n" +
    "\n" +
    "<div class=\"row ui-editable-field\">\n" +
    "\n" +
    "    <!-- Field label -->\n" +
    "    <div class=\"col-sm-4 col-lg-3 text-right\">\n" +
    "        {{ label }} <span ng-show=\"isRequired && state == 'editing'\">*</span>\n" +
    "    </div>\n" +
    "\n" +
    "    <div class=\"col-sm-8 col-lg-9\" ng-switch=\"inputType\">\n" +
    "\n" +
    "        <!-- Date -->\n" +
    "        <div ng-switch-when=\"date\">\n" +
    "            <ng-include src=\"'partials/directives/ui-editable-fields/field-value-timestamp.html'\"></ng-include>\n" +
    "\n" +
    "            <input\n" +
    "                ng-show=\"state == 'editing'\"\n" +
    "                ng-disabled=\"isDisabled\"\n" +
    "                ng-required=\"isRequired\"\n" +
    "                type=\"datetime\"\n" +
    "                class=\"form-control\">\n" +
    "        </div>\n" +
    "\n" +
    "        <!-- Date & time -->\n" +
    "        <div ng-switch-when=\"datetime\">\n" +
    "            <ng-include src=\"'partials/directives/ui-editable-fields/field-value-timestamp.html'\"></ng-include>\n" +
    "\n" +
    "            <input\n" +
    "                ng-show=\"state == 'editing'\"\n" +
    "                ng-disabled=\"isDisabled\"\n" +
    "                ng-required=\"isRequired\"\n" +
    "                type=\"datetime\"\n" +
    "                class=\"form-control\">\n" +
    "        </div>\n" +
    "\n" +
    "        <!-- Length -->\n" +
    "        <div ng-switch-when=\"length\">\n" +
    "            <ng-include src=\"'partials/directives/ui-editable-fields/field-value-with-units.html'\"></ng-include>\n" +
    "\n" +
    "            <!-- Feet & inches -->\n" +
    "            <div class=\"row\" ng-show=\"state == 'editing' && config.unitForLength == 'ft/in'\">\n" +
    "                <div class=\"col-sm-6\">\n" +
    "                    <div class=\"input-group\">\n" +
    "                        <input\n" +
    "                            ng-model=\"data.lengthFeet\"\n" +
    "                            ng-disabled=\"isDisabled\"\n" +
    "                            ng-required=\"isRequired\"\n" +
    "                            ng-blur=\"updateModel()\"\n" +
    "                            ng-change=\"updateModel()\"\n" +
    "                            type=\"number\" min=\"1\" max=\"10\"\n" +
    "                            class=\"form-control\">\n" +
    "                        <span class=\"input-group-addon\">&apos;</span>\n" +
    "                    </div>\n" +
    "                </div>\n" +
    "\n" +
    "                <div class=\"col-sm-6\">\n" +
    "                    <div class=\"input-group\">\n" +
    "                        <input\n" +
    "                            ng-model=\"data.lengthInches\"\n" +
    "                            ng-disabled=\"isDisabled\"\n" +
    "                            ng-required=\"isRequired\"\n" +
    "                            ng-blur=\"updateModel()\"\n" +
    "                            ng-change=\"updateModel()\"\n" +
    "                            type=\"number\" min=\"0\" max=\"11\"\n" +
    "                            class=\"form-control\">\n" +
    "                        <span class=\"input-group-addon\">&quot;</span>\n" +
    "                    </div>\n" +
    "                </div>\n" +
    "            </div>\n" +
    "\n" +
    "            <!-- All other supported units -->\n" +
    "            <div class=\"input-group\" ng-show=\"state == 'editing' && config.unitForLength != 'ft/in'\">\n" +
    "                <span class=\"input-group-addon\">{{ config.unitForLength }}</span>\n" +
    "                <input\n" +
    "                    ng-model=\"data.lengthVal\"\n" +
    "                    ng-disabled=\"isDisabled\"\n" +
    "                    ng-required=\"isRequired\"\n" +
    "                    ng-blur=\"updateModel()\"\n" +
    "                    type=\"number\" step=\"0.01\"\n" +
    "                    class=\"form-control\">\n" +
    "            </div>\n" +
    "        </div>\n" +
    "\n" +
    "        <!-- Gender -->\n" +
    "        <div ng-switch-when=\"gender\">\n" +
    "            <ng-include src=\"'partials/directives/ui-editable-fields/field-value-generic.html'\"></ng-include>\n" +
    "\n" +
    "            <select\n" +
    "                ng-show=\"state == 'editing'\"\n" +
    "                ng-model=\"model[key]\"\n" +
    "                ng-selected=\"model[key]\"\n" +
    "                ng-disabled=\"isDisabled\"\n" +
    "                ng-required=\"isRequired\"\n" +
    "                class=\"form-control\">\n" +
    "\n" +
    "                <option value=\"female\">Female</option>\n" +
    "                <option value=\"male\">Male</option>\n" +
    "            </select>\n" +
    "        </div>\n" +
    "\n" +
    "        <!-- Mass -->\n" +
    "        <div ng-switch-when=\"mass\">\n" +
    "            <ng-include src=\"'partials/directives/ui-editable-fields/field-value-with-units.html'\"></ng-include>\n" +
    "\n" +
    "            <!-- Edit field -->\n" +
    "            <div class=\"input-group\" ng-show=\"state == 'editing'\">\n" +
    "                <span class=\"input-group-addon\">{{ config.unitForMass }}</span>\n" +
    "                <input\n" +
    "                    ng-model=\"data.massVal\"\n" +
    "                    ng-disabled=\"isDisabled\"\n" +
    "                    ng-required=\"isRequired\"\n" +
    "                    ng-blur=\"updateModel()\"\n" +
    "                    type=\"number\" step=\"0.01\"\n" +
    "                    class=\"form-control\">\n" +
    "            </div>\n" +
    "        </div>\n" +
    "\n" +
    "        <!-- Tag -->\n" +
    "        <div ng-switch-when=\"tag\">\n" +
    "            <ng-include src=\"'partials/directives/ui-editable-fields/field-value-generic.html'\"></ng-include>\n" +
    "\n" +
    "            <div ng-show=\"state == 'editing'\">\n" +
    "                <selectize\n" +
    "                    ng-model=\"data\"\n" +
    "                    class=\"form-control text-left\"\n" +
    "                    config=\"config\"\n" +
    "                    options=\"options\">\n" +
    "                </selectize>\n" +
    "            </div>\n" +
    "        </div>\n" +
    "\n" +
    "        <!-- Placeholder -->\n" +
    "        <div ng-switch-when=\"placeholder\">\n" +
    "            <ng-include src=\"'partials/directives/ui-editable-fields/field-value-generic.html'\"></ng-include>\n" +
    "\n" +
    "            <input\n" +
    "                ng-show=\"state == 'editing'\"\n" +
    "                ng-model=\"display\"\n" +
    "                ng-disabled=\"isDisabled\"\n" +
    "                ng-required=\"isRequired\"\n" +
    "                type=\"text\"\n" +
    "                class=\"form-control\">\n" +
    "        </div>\n" +
    "\n" +
    "        <!-- Default -->\n" +
    "        <div ng-switch-default>\n" +
    "            <ng-include src=\"'partials/directives/ui-editable-fields/field-value-generic.html'\"></ng-include>\n" +
    "\n" +
    "            <input\n" +
    "                ng-show=\"state == 'editing'\"\n" +
    "                ng-model=\"model[key]\"\n" +
    "                ng-disabled=\"isDisabled\"\n" +
    "                ng-required=\"isRequired\"\n" +
    "                type=\"text\"\n" +
    "                class=\"form-control\">\n" +
    "        </div>\n" +
    "    </div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("partials/directives/ui-editable-fields/field-value-generic.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("partials/directives/ui-editable-fields/field-value-generic.html",
    "<!-- Copyright Heddoko(TM) 2015, all rights reserved. -->\n" +
    "\n" +
    "<div ng-show=\"state == 'idle'\" class=\"ui-editable-field-value\">\n" +
    "\n" +
    "    <!-- Displayed value -->\n" +
    "    <a ng-if=\"!isDisabled\" ng-click=\"edit()\" href=\"javascript:;\" class=\"edit-text\">\n" +
    "        {{ display || model[key] || empty }}\n" +
    "    </a>\n" +
    "    <span ng-if=\"isDisabled\">\n" +
    "        {{ display || model[key] || empty }}\n" +
    "    </span>\n" +
    "\n" +
    "    <!-- Edit icon -->\n" +
    "    <a ng-if=\"!isDisabled\" ng-click=\"edit()\" href=\"javascript:;\" class=\"edit-icon fa fa-pencil\"></a>\n" +
    "    <i ng-if=\"isDisabled\" class=\"edit-icon fa fa-ban\"></i>\n" +
    "\n" +
    "</div>\n" +
    "\n" +
    "<div ng-show=\"state == 'saving'\" class=\"ui-editable-field-value ui-editable-fields-muted\">\n" +
    "    {{ display || model[key] || empty }}\n" +
    "    <i class=\"fa fa-spinner fa-spin fa-fw\"></i>\n" +
    "</div>\n" +
    "");
}]);

angular.module("partials/directives/ui-editable-fields/field-value-timestamp.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("partials/directives/ui-editable-fields/field-value-timestamp.html",
    "<!-- Copyright Heddoko(TM) 2015, all rights reserved. -->\n" +
    "\n" +
    "<div ng-show=\"state == 'idle'\" class=\"ui-editable-field-value\">\n" +
    "\n" +
    "    <!-- Displayed value -->\n" +
    "    <a ng-if=\"!isDisabled\" ng-click=\"edit()\" href=\"javascript:;\" class=\"edit-text\">\n" +
    "        {{ timestamp | date:format }}\n" +
    "    </a>\n" +
    "    <span ng-if=\"isDisabled\">\n" +
    "        {{ timestamp | date:format }}\n" +
    "    </span>\n" +
    "\n" +
    "    <!-- Edit icon -->\n" +
    "    <a ng-if=\"!isDisabled\" ng-click=\"edit()\" href=\"javascript:;\" class=\"edit-icon fa fa-pencil\"></a>\n" +
    "    <i ng-if=\"isDisabled\" class=\"edit-icon fa fa-ban\"></i>\n" +
    "\n" +
    "</div>\n" +
    "\n" +
    "<div ng-show=\"state == 'saving'\" class=\"ui-editable-field-value ui-editable-fields-muted\">\n" +
    "    {{ timestamp | date:format }}\n" +
    "    <i class=\"fa fa-spinner fa-spin fa-fw\"></i>\n" +
    "</div>\n" +
    "");
}]);

angular.module("partials/directives/ui-editable-fields/field-value-with-units.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("partials/directives/ui-editable-fields/field-value-with-units.html",
    "<!-- Copyright Heddoko(TM) 2015, all rights reserved. -->\n" +
    "\n" +
    "<div ng-show=\"state == 'idle'\" class=\"row no-gutter ui-editable-field-value\">\n" +
    "\n" +
    "    <!-- Unit selector -->\n" +
    "    <div class=\"col-xs-2 text-center\">\n" +
    "        <div class=\"dropdown\">\n" +
    "            <a href=\"javascript:;\" class=\"dropdown-toggle\" data-toggle=\"dropdown\" aria-haspopup=\"true\">\n" +
    "                <i class=\"fa fa-cog\" style=\"color: #ccc;\"></i>\n" +
    "            </a>\n" +
    "            <ul class=\"dropdown-menu\">\n" +
    "                <li ng-repeat=\"unit in units\">\n" +
    "                    <a ng-click=\"updateUnit(unit)\" href=\"javascript:;\">\n" +
    "                        {{ unit }}\n" +
    "                    </a>\n" +
    "                </li>\n" +
    "            </ul>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "\n" +
    "    <div class=\"col-xs-10\">\n" +
    "\n" +
    "        <!-- Displayed data -->\n" +
    "        <a ng-click=\"edit()\" href=\"javascript:;\" class=\"edit-text\">\n" +
    "            {{ data.displayStr }}\n" +
    "        </a>\n" +
    "\n" +
    "        <!-- Edit icons -->\n" +
    "        <a ng-if=\"!isDisabled\" ng-click=\"edit()\" href=\"javascript:;\" class=\"edit-icon fa fa-pencil\"></a>\n" +
    "        <i ng-if=\"isDisabled\" class=\"edit-icon fa fa-ban\"></i>\n" +
    "    </div>\n" +
    "</div>\n" +
    "\n" +
    "<div ng-show=\"state == 'saving'\" class=\"ui-editable-field-value ui-editable-fields-muted\">\n" +
    "    {{ data.displayStr }}\n" +
    "    <i class=\"fa fa-spinner fa-spin fa-fw\"></i>\n" +
    "</div>\n" +
    "");
}]);

angular.module("partials/directives/ui-editable-fields/field-vertical.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("partials/directives/ui-editable-fields/field-vertical.html",
    "<!-- Copyright Heddoko(TM) 2015, all rights reserved. -->\n" +
    "\n" +
    "<div class=\"row ui-editable-field ui-editable-field-vertical\">\n" +
    "\n" +
    "    <div ng-switch=\"inputType\" class=\"col-xs-12\">\n" +
    "\n" +
    "        <!-- Date -->\n" +
    "        <div ng-switch-when=\"date\">\n" +
    "            <ng-include src=\"'partials/directives/ui-editable-fields/field-value-timestamp.html'\"></ng-include>\n" +
    "\n" +
    "            <!-- Edit field -->\n" +
    "            <div\n" +
    "                ng-show=\"state == 'editing'\"\n" +
    "                class=\"input-group\">\n" +
    "\n" +
    "                <span class=\"input-group-btn\">\n" +
    "                    <button ng-click=\"cancel()\" title=\"Cancel\" class=\"btn btn-danger\" type=\"button\">\n" +
    "                        <i class=\"fa fa-times\"></i>\n" +
    "                    </button>\n" +
    "                    <button ng-click=\"save()\" title=\"Save\" class=\"btn btn-default\" type=\"button\">\n" +
    "                        <i class=\"fa fa-floppy-o\"></i>\n" +
    "                    </button>\n" +
    "                </span>\n" +
    "\n" +
    "                <input\n" +
    "                    ng-show=\"state == 'editing'\"\n" +
    "                    ng-disabled=\"isDisabled\"\n" +
    "                    ng-required=\"isRequired\"\n" +
    "                    type=\"datetime\"\n" +
    "                    class=\"form-control\">\n" +
    "            </div>\n" +
    "        </div>\n" +
    "\n" +
    "        <!-- Date & time -->\n" +
    "        <div ng-switch-when=\"datetime\">\n" +
    "            <ng-include src=\"'partials/directives/ui-editable-fields/field-value-timestamp.html'\"></ng-include>\n" +
    "\n" +
    "            <!-- Edit field -->\n" +
    "            <div\n" +
    "                ng-show=\"state == 'editing'\"\n" +
    "                class=\"input-group\">\n" +
    "\n" +
    "                <span class=\"input-group-btn\">\n" +
    "                    <button ng-click=\"cancel()\" title=\"Cancel\" class=\"btn btn-danger\" type=\"button\">\n" +
    "                        <i class=\"fa fa-times\"></i>\n" +
    "                    </button>\n" +
    "                    <button ng-click=\"save()\" title=\"Save\" class=\"btn btn-default\" type=\"button\">\n" +
    "                        <i class=\"fa fa-floppy-o\"></i>\n" +
    "                    </button>\n" +
    "                </span>\n" +
    "\n" +
    "                <input\n" +
    "                    ng-disabled=\"isDisabled\"\n" +
    "                    ng-required=\"isRequired\"\n" +
    "                    type=\"datetime\"\n" +
    "                    class=\"form-control\">\n" +
    "            </div>\n" +
    "        </div>\n" +
    "\n" +
    "        <!-- Length -->\n" +
    "        <div ng-switch-when=\"length\">\n" +
    "            <ng-include src=\"'partials/directives/ui-editable-fields/field-value-with-units.html'\"></ng-include>\n" +
    "\n" +
    "            <!-- Feet & inches -->\n" +
    "            <div class=\"row\" ng-show=\"state == 'editing' && config.unitForLength == 'ft/in'\">\n" +
    "                <div class=\"col-sm-6\">\n" +
    "                    <div class=\"input-group\">\n" +
    "                        <input\n" +
    "                            ng-model=\"data.lengthFeet\"\n" +
    "                            ng-disabled=\"isDisabled\"\n" +
    "                            ng-required=\"isRequired\"\n" +
    "                            ng-blur=\"updateModel()\"\n" +
    "                            ng-change=\"updateModel()\"\n" +
    "                            type=\"number\" min=\"1\" max=\"10\"\n" +
    "                            class=\"form-control\">\n" +
    "                        <span class=\"input-group-addon\">&apos;</span>\n" +
    "                    </div>\n" +
    "                </div>\n" +
    "\n" +
    "                <div class=\"col-sm-6\">\n" +
    "                    <div class=\"input-group\">\n" +
    "                        <input\n" +
    "                            ng-model=\"data.lengthInches\"\n" +
    "                            ng-disabled=\"isDisabled\"\n" +
    "                            ng-required=\"isRequired\"\n" +
    "                            ng-blur=\"updateModel()\"\n" +
    "                            ng-change=\"updateModel()\"\n" +
    "                            type=\"number\" min=\"0\" max=\"11\"\n" +
    "                            class=\"form-control\">\n" +
    "                        <span class=\"input-group-addon\">&quot;</span>\n" +
    "                    </div>\n" +
    "                </div>\n" +
    "            </div>\n" +
    "\n" +
    "            <!-- All other supported units -->\n" +
    "            <div class=\"input-group\" ng-show=\"state == 'editing' && config.unitForLength != 'ft/in'\">\n" +
    "                <span class=\"input-group-addon\">{{ config.unitForLength }}</span>\n" +
    "                <input\n" +
    "                    ng-model=\"data.lengthVal\"\n" +
    "                    ng-disabled=\"isDisabled\"\n" +
    "                    ng-required=\"isRequired\"\n" +
    "                    ng-blur=\"updateModel()\"\n" +
    "                    type=\"number\" step=\"0.01\"\n" +
    "                    class=\"form-control\">\n" +
    "            </div>\n" +
    "        </div>\n" +
    "\n" +
    "        <!-- Gender -->\n" +
    "        <div ng-switch-when=\"gender\">\n" +
    "            <ng-include src=\"'partials/directives/ui-editable-fields/field-value-generic.html'\"></ng-include>\n" +
    "\n" +
    "            <!-- Edit field -->\n" +
    "            <div\n" +
    "                ng-show=\"state == 'editing'\"\n" +
    "                class=\"input-group\">\n" +
    "\n" +
    "                <span class=\"input-group-btn\">\n" +
    "                    <button ng-click=\"cancel()\" title=\"Cancel\" class=\"btn btn-danger\" type=\"button\">\n" +
    "                        <i class=\"fa fa-times\"></i>\n" +
    "                    </button>\n" +
    "                    <button ng-click=\"save()\" title=\"Save\" class=\"btn btn-default\" type=\"button\">\n" +
    "                        <i class=\"fa fa-floppy-o\"></i>\n" +
    "                    </button>\n" +
    "                </span>\n" +
    "\n" +
    "                <select\n" +
    "                    ng-model=\"model[key]\"\n" +
    "                    ng-selected=\"model[key]\"\n" +
    "                    ng-disabled=\"isDisabled\"\n" +
    "                    ng-required=\"isRequired\"\n" +
    "                    class=\"form-control\">\n" +
    "\n" +
    "                    <option value=\"female\">Female</option>\n" +
    "                    <option value=\"male\">Male</option>\n" +
    "                </select>\n" +
    "            </div>\n" +
    "        </div>\n" +
    "\n" +
    "        <!-- Mass -->\n" +
    "        <div ng-switch-when=\"mass\">\n" +
    "            <ng-include src=\"'partials/directives/ui-editable-fields/field-value-with-units.html'\"></ng-include>\n" +
    "\n" +
    "            <!-- Edit field -->\n" +
    "            <div\n" +
    "                ng-show=\"state == 'editing'\"\n" +
    "                class=\"input-group\">\n" +
    "\n" +
    "                <!-- Buttons -->\n" +
    "                <span class=\"input-group-btn\">\n" +
    "                    <button ng-click=\"cancel()\" title=\"Cancel\" class=\"btn btn-danger\" type=\"button\">\n" +
    "                        <i class=\"fa fa-times\"></i>\n" +
    "                    </button>\n" +
    "                    <button ng-click=\"save()\" title=\"Save\" class=\"btn btn-default\" type=\"button\">\n" +
    "                        <i class=\"fa fa-floppy-o\"></i>\n" +
    "                    </button>\n" +
    "                </span>\n" +
    "\n" +
    "                <!-- Unit -->\n" +
    "                <span class=\"input-group-addon\">{{ config.unitForMass }}</span>\n" +
    "\n" +
    "                <input\n" +
    "                    ng-model=\"data.massVal\"\n" +
    "                    ng-disabled=\"isDisabled\"\n" +
    "                    ng-required=\"isRequired\"\n" +
    "                    ng-blur=\"updateModel()\"\n" +
    "                    type=\"number\" step=\"0.01\"\n" +
    "                    class=\"form-control\">\n" +
    "            </div>\n" +
    "        </div>\n" +
    "\n" +
    "        <!-- Tag -->\n" +
    "        <div ng-switch-when=\"tag\">\n" +
    "            <ng-include src=\"'partials/directives/ui-editable-fields/field-value-generic.html'\"></ng-include>\n" +
    "\n" +
    "            <div\n" +
    "                ng-show=\"state == 'editing'\"\n" +
    "                class=\"input-group\">\n" +
    "\n" +
    "                <span class=\"input-group-btn\">\n" +
    "                    <button ng-click=\"cancel()\" title=\"Cancel\" class=\"btn btn-danger\" type=\"button\">\n" +
    "                        <i class=\"fa fa-times\"></i>\n" +
    "                    </button>\n" +
    "                    <button ng-click=\"save()\" title=\"Save\" class=\"btn btn-default\" type=\"button\">\n" +
    "                        <i class=\"fa fa-floppy-o\"></i>\n" +
    "                    </button>\n" +
    "                </span>\n" +
    "\n" +
    "                <selectize\n" +
    "                    ng-model=\"data\"\n" +
    "                    class=\"form-control text-left\"\n" +
    "                    config=\"config\"\n" +
    "                    options=\"options\">\n" +
    "                </selectize>\n" +
    "            </div>\n" +
    "        </div>\n" +
    "\n" +
    "        <!-- Placeholder -->\n" +
    "        <div ng-switch-when=\"placeholder\">\n" +
    "            <ng-include src=\"'partials/directives/ui-editable-fields/field-value-generic.html'\"></ng-include>\n" +
    "\n" +
    "            <div\n" +
    "                ng-show=\"state == 'editing'\"\n" +
    "                class=\"input-group\">\n" +
    "\n" +
    "                <span class=\"input-group-btn\">\n" +
    "                    <button ng-click=\"cancel()\" title=\"Cancel\" class=\"btn btn-danger\" type=\"button\">\n" +
    "                        <i class=\"fa fa-times\"></i>\n" +
    "                    </button>\n" +
    "                    <button ng-click=\"save()\" title=\"Save\" class=\"btn btn-default\" type=\"button\">\n" +
    "                        <i class=\"fa fa-floppy-o\"></i>\n" +
    "                    </button>\n" +
    "                </span>\n" +
    "\n" +
    "                <input\n" +
    "                    ng-model=\"display\"\n" +
    "                    ng-disabled=\"isDisabled\"\n" +
    "                    ng-required=\"isRequired\"\n" +
    "                    type=\"text\"\n" +
    "                    class=\"form-control\">\n" +
    "            </div>\n" +
    "        </div>\n" +
    "\n" +
    "        <!-- Default -->\n" +
    "        <div ng-switch-default>\n" +
    "            <ng-include src=\"'partials/directives/ui-editable-fields/field-value-generic.html'\"></ng-include>\n" +
    "\n" +
    "            <!-- Edit field -->\n" +
    "            <div\n" +
    "                ng-show=\"state == 'editing'\"\n" +
    "                class=\"input-group\">\n" +
    "\n" +
    "                <span class=\"input-group-btn\">\n" +
    "                    <button ng-click=\"cancel()\" title=\"Cancel\" class=\"btn btn-danger\" type=\"button\">\n" +
    "                        <i class=\"fa fa-times\"></i>\n" +
    "                    </button>\n" +
    "                    <button ng-click=\"save()\" title=\"Save\" class=\"btn btn-default\" type=\"button\">\n" +
    "                        <i class=\"fa fa-floppy-o\"></i>\n" +
    "                    </button>\n" +
    "                </span>\n" +
    "\n" +
    "                <input\n" +
    "                    ng-model=\"model[key]\"\n" +
    "                    ng-disabled=\"isDisabled\"\n" +
    "                    ng-required=\"isRequired\"\n" +
    "                    type=\"text\"\n" +
    "                    class=\"form-control\">\n" +
    "            </div>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "\n" +
    "    <!-- Field label -->\n" +
    "    <div class=\"col-xs-12 ui-editable-field-label\">\n" +
    "        {{ label }} <span ng-show=\"isRequired && state == 'editing'\">*</span>\n" +
    "    </div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("partials/directives/ui-editable-fields/fields.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("partials/directives/ui-editable-fields/fields.html",
    "<!-- Copyright Heddoko(TM) 2015, all rights reserved. -->\n" +
    "\n" +
    "<div class=\"ui-editable-fields\" ng-attr-id=\"{{ id }}\">\n" +
    "\n" +
    "    <h3 class=\"title\">\n" +
    "        {{ heading }}\n" +
    "        <span></span>\n" +
    "    </h3>\n" +
    "\n" +
    "    <!-- Action buttons -->\n" +
    "    <div class=\"ui-editable-fields-actions\">\n" +
    "        <span ng-click=\"save()\" ng-show=\"state == 'editing'\">\n" +
    "            Save &nbsp; <i class=\"fa fa-toggle-on\"></i>\n" +
    "        </span>\n" +
    "        <span ng-click=\"edit()\" ng-show=\"state == 'idle'\">\n" +
    "            Edit All Fields &nbsp; <i class=\"fa fa-toggle-off\"></i>\n" +
    "        </span>\n" +
    "        <span ng-show=\"state == 'saving'\" style=\"color: #ccc;\">\n" +
    "            <i class=\"fa fa-toggle-off\"></i>\n" +
    "        </span>\n" +
    "    </div>\n" +
    "\n" +
    "    <div ng-switch=\"state\">\n" +
    "\n" +
    "        <!-- Working animation -->\n" +
    "        <div ng-switch-when=\"saving\" class=\"text-center\" style=\"padding: 20px 0;\">\n" +
    "            <i class=\"fa fa-spin fa-4x fa-spinner\"></i>\n" +
    "        </div>\n" +
    "\n" +
    "        <!-- Model fields -->\n" +
    "        <div ng-switch-default ng-transclude>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("partials/directives/ui-editable-fields/standalone-field.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("partials/directives/ui-editable-fields/standalone-field.html",
    "<!-- Copyright Heddoko(TM) 2015, all rights reserved. -->\n" +
    "\n" +
    "<div class=\"ui-editable-fields\">\n" +
    "\n" +
    "    <h3 ng-if=\"heading\" class=\"title\">\n" +
    "        {{ heading }}\n" +
    "        <span></span>\n" +
    "    </h3>\n" +
    "\n" +
    "    <div class=\"row\" ng-switch=\"state\">\n" +
    "\n" +
    "        <!-- Editable field -->\n" +
    "        <div ng-switch-when=\"editing\" class=\"col-sm-12\">\n" +
    "\n" +
    "            <!-- Type: tag -->\n" +
    "            <div ng-if=\"inputType == 'tag-title'\" class=\"input-type-tag\">\n" +
    "\n" +
    "                <div class=\"buttons\">\n" +
    "                    <a ng-click=\"cancel()\" title=\"Cancel\" href=\"javascript:;\">\n" +
    "                        <i class=\"fa fa-times\"></i>\n" +
    "                    </a>\n" +
    "                    <a ng-click=\"save()\" title=\"Save\" href=\"javascript:;\">\n" +
    "                        <i class=\"fa fa-floppy-o\"></i>\n" +
    "                    </a>\n" +
    "                </div>\n" +
    "\n" +
    "                <selectize\n" +
    "                    ng-model=\"model[key]\"\n" +
    "                    class=\"form-control text-left\"\n" +
    "                    config=\"selectizeConfig\"\n" +
    "                    options=\"options\">\n" +
    "                </selectize>\n" +
    "            </div>\n" +
    "\n" +
    "            <!-- Type: text -->\n" +
    "            <div ng-if=\"inputType == 'text'\" class=\"input-group\">\n" +
    "                <span class=\"input-group-btn\">\n" +
    "                    <button ng-click=\"cancel()\" title=\"Cancel\" class=\"btn btn-danger\" type=\"button\">\n" +
    "                        <i class=\"fa fa-times\"></i>\n" +
    "                    </button>\n" +
    "                    <button ng-click=\"save()\" title=\"Save\" class=\"btn btn-default\" type=\"button\">\n" +
    "                        <i class=\"fa fa-floppy-o\"></i>\n" +
    "                    </button>\n" +
    "                </span>\n" +
    "\n" +
    "                <input\n" +
    "                    ng-model=\"model[key]\"\n" +
    "                    type=\"text\"\n" +
    "                    class=\"form-control\">\n" +
    "            </div>\n" +
    "\n" +
    "            <!-- Type: textarea -->\n" +
    "            <div ng-if=\"inputType == 'textarea'\" class=\"input-type-textarea\">\n" +
    "\n" +
    "                <textarea ng-model=\"model[key]\" class=\"form-control\"></textarea>\n" +
    "\n" +
    "                <div class=\"buttons\">\n" +
    "                    <a ng-click=\"cancel()\" title=\"Cancel\" href=\"javascript:;\">\n" +
    "                        <i class=\"fa fa-times\"></i>\n" +
    "                    </a>\n" +
    "                    <a ng-click=\"save()\" title=\"Save\" href=\"javascript:;\">\n" +
    "                        <i class=\"fa fa-floppy-o\"></i>\n" +
    "                    </a>\n" +
    "                </div>\n" +
    "            </div>\n" +
    "        </div>\n" +
    "\n" +
    "        <!-- Saving notice -->\n" +
    "        <div ng-switch-when=\"saving\" class=\"col-sm-12\">\n" +
    "            <span class=\"input-type-{{ inputType }}\">\n" +
    "                {{ display || model[key] || empty }}\n" +
    "                <i class=\"fa fa-spinner fa-spin fa-fw\"></i>\n" +
    "            </span>\n" +
    "        </div>\n" +
    "\n" +
    "        <!-- Displayed field -->\n" +
    "        <div ng-switch-default class=\"col-sm-12 ui-editable-field-value\">\n" +
    "            <a ng-click=\"edit()\" href=\"javascript:;\" class=\"input-type-{{ inputType }}\">\n" +
    "                <span class=\"edit-text\">\n" +
    "                    {{ display || model[key] || empty }}\n" +
    "                </span>\n" +
    "                <i class=\"edit-icon fa fa-pencil\"></i>\n" +
    "            </a>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("partials/directives/ui-filesystem/container.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("partials/directives/ui-filesystem/container.html",
    "<!-- Copyright Heddoko(TM) 2016, all rights reserved. -->\n" +
    "\n" +
    "<div class=\"ui-filesystem-container\" ng-attr-id=\"{{ ::id }}\">\n" +
    "\n" +
    "    <!-- Toolbar -->\n" +
    "    <div ng-if=\"!hideToolbar\" class=\"row ui-filesystem-toolbar\" role=\"toolbar\">\n" +
    "        <div class=\"col-xs-12\">\n" +
    "\n" +
    "            <!-- Layout selector -->\n" +
    "            <div ng-if=\"layout.list.length > 1\" class=\"ui-filesystem-toolbar-group\">\n" +
    "                <a\n" +
    "                    ng-repeat=\"btn in layout.list\"\n" +
    "                    ng-click=\"setLayout(btn.name)\"\n" +
    "                    ng-class=\"{'ui-filesystem-active': btn.name == layout.name}\"\n" +
    "                    class=\"ui-filesystem-toolbar-btn\">\n" +
    "\n" +
    "                    <i class=\"fa fa-fw fa-lg fa-{{ btn.icon }}\"></i>\n" +
    "                </a>\n" +
    "            </div>\n" +
    "\n" +
    "            <!-- Create button -->\n" +
    "            <div ng-if=\"config.toolbar.createModal\" class=\"ui-filesystem-toolbar-group\">\n" +
    "                <a\n" +
    "                    data-toggle=\"modal\"\n" +
    "                    data-target=\"#{{ config.toolbar.createModal }}\"\n" +
    "                    href=\"javascript:;\"\n" +
    "                    class=\"ui-filesystem-toolbar-btn\">\n" +
    "\n" +
    "                    <i class=\"fa fa-fw fa-lg fa-{{ config.toolbar.createModalIcon }}\"></i>\n" +
    "                </a>\n" +
    "            </div>\n" +
    "\n" +
    "            <!-- Other actions -->\n" +
    "            <div ng-if=\"config.onDelete\" class=\"ui-filesystem-toolbar-group\">\n" +
    "\n" +
    "                <!-- Edit resource -->\n" +
    "                <a\n" +
    "                    ng-click=\"config.onEdit()\"\n" +
    "                    ng-class=\"{disabled: (selectedFiles.length + selectedFolders.length !== 1)}\"\n" +
    "                    href=\"javascript:;\"\n" +
    "                    class=\"ui-filesystem-toolbar-btn\">\n" +
    "\n" +
    "                    <i class=\"fa fa-fw fa-lg fa-pencil\"></i>\n" +
    "                </a>\n" +
    "\n" +
    "                <!-- Delete resources -->\n" +
    "                <a\n" +
    "                    ng-click=\"config.onDelete()\"\n" +
    "                    ng-class=\"{disabled: (selectedFiles.length + selectedFolders.length === 0)}\"\n" +
    "                    href=\"javascript:;\"\n" +
    "                    class=\"ui-filesystem-toolbar-btn\">\n" +
    "\n" +
    "                    <i class=\"fa fa-fw fa-lg fa-trash\"></i>\n" +
    "                </a>\n" +
    "            </div>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "\n" +
    "    <!-- Contents -->\n" +
    "    <div ng-if=\"!isLoading\" class=\"ui-filesystem-contents ui-filesystem-{{ layout.name }}\">\n" +
    "        <ng-include\n" +
    "            src=\"'partials/directives/ui-filesystem/layouts/' + layout.name + '.html'\">\n" +
    "        </ng-include>\n" +
    "    </div>\n" +
    "\n" +
    "    <!-- Loading -->\n" +
    "    <div ng-if=\"isLoading\" class=\"ui-filesystem-contents\">\n" +
    "        <div class=\"text-center\" style=\"margin: 60px auto\">\n" +
    "            <i class=\"fa fa-spinner fa-spin fa-2x text-muted\"></i>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "\n" +
    "    <!-- Location -->\n" +
    "    <div ng-if=\"!hideLocation && path\" class=\"row ui-filesystem-location\">\n" +
    "        <div class=\"col-xs-12\">\n" +
    "            <i class=\"fa fa-compass fa-fw\"></i>\n" +
    "            <span>\n" +
    "                {{ path }}\n" +
    "            </span>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "\n" +
    "</div>\n" +
    "");
}]);

angular.module("partials/directives/ui-filesystem/layouts/details.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("partials/directives/ui-filesystem/layouts/details.html",
    "<!-- Copyright Heddoko(TM) 2016, all rights reserved. -->\n" +
    "\n" +
    "<table class=\"table table-striped\">\n" +
    "\n" +
    "    <!-- Heading -->\n" +
    "    <thead>\n" +
    "        <tr>\n" +
    "\n" +
    "            <!-- Checkbox -->\n" +
    "            <td ng-if=\"config.onSelect\" class=\"text-right\">\n" +
    "                <a ng-click=\"toggleAll()\" href=\"javascript:;\">\n" +
    "                    <i class=\"fa fa-square-o fa-fw\"></i>\n" +
    "                </a>\n" +
    "            </td>\n" +
    "\n" +
    "            <td ng-repeat=\"title in config.detailsLayoutTitles track by title.key\" class=\"text-center\">\n" +
    "                {{ ::title.title }}\n" +
    "            </td>\n" +
    "        </tr>\n" +
    "    </thead>\n" +
    "\n" +
    "    <tbody>\n" +
    "\n" +
    "        <!-- Parent folder -->\n" +
    "        <tr ng-if=\"parentFolder\">\n" +
    "\n" +
    "            <!-- Checkbox -->\n" +
    "            <td ng-if=\"config.onSelect\">&nbsp;</td>\n" +
    "\n" +
    "            <td colspan=\"{{ ::config.detailsLayoutTitles.length }}\">\n" +
    "                <a href=\"{{ ::parentFolder.href }}\">\n" +
    "                    <i class=\"fa fa-fw fa-level-up\"></i> {{ ::parentFolder.title || 'Back' }}\n" +
    "                </a>\n" +
    "            </td>\n" +
    "\n" +
    "        </tr>\n" +
    "\n" +
    "        <!-- Folders -->\n" +
    "        <tr ng-repeat=\"folder in folders track by $index\">\n" +
    "\n" +
    "            <!-- Checkbox -->\n" +
    "            <td ng-if=\"config.onSelect\" class=\"text-right\">\n" +
    "                <a ng-click=\"toggleFolder(folder)\" href=\"javascript:;\">\n" +
    "                    <i\n" +
    "                        ng-class=\"{'fa-square-o': !folder.selected, 'fa-check-square-o': folder.selected}\"\n" +
    "                        class=\"fa fa-fw\">\n" +
    "                    </i>\n" +
    "                </a>\n" +
    "            </td>\n" +
    "\n" +
    "            <td\n" +
    "                ng-repeat=\"title in config.detailsLayoutTitles track by title.key\"\n" +
    "                ng-class=\"{'text-center': !config.onSelect || title.key !== 'title'}\">\n" +
    "\n" +
    "                <a ng-if=\"title.key == 'title'\" href=\"{{ ::folder.href }}\">\n" +
    "                    <i class=\"fa fa-folder-open fa-fw\"></i> {{ ::folder.title }}\n" +
    "                </a>\n" +
    "\n" +
    "                <span ng-if=\"title.key != 'title'\">{{ ::folder[title.key] }}</span>\n" +
    "            </td>\n" +
    "\n" +
    "        </tr>\n" +
    "\n" +
    "        <!-- Files -->\n" +
    "        <tr ng-repeat=\"file in files track by $index\">\n" +
    "\n" +
    "            <!-- Checkbox -->\n" +
    "            <td ng-if=\"config.onSelect\" class=\"text-right\">\n" +
    "                <a ng-click=\"toggleFile(file)\" href=\"javascript:;\">\n" +
    "                    <i\n" +
    "                        ng-class=\"{'fa-square-o': !file.selected, 'fa-check-square-o': file.selected}\"\n" +
    "                        class=\"fa fa-fw\">\n" +
    "                    </i>\n" +
    "                </a>\n" +
    "            </td>\n" +
    "\n" +
    "            <td\n" +
    "                ng-repeat=\"title in config.detailsLayoutTitles track by title.key\"\n" +
    "                ng-class=\"{'text-center': !config.onSelect || title.key !== 'title'}\">\n" +
    "\n" +
    "                <a ng-if=\"title.key == 'title'\" href=\"{{ ::file.href }}\">\n" +
    "                    {{ ::file.title }}\n" +
    "                </a>\n" +
    "\n" +
    "                <span ng-if=\"title.key != 'title'\">{{ ::file[title.key] }}</span>\n" +
    "            </td>\n" +
    "        </tr>\n" +
    "    </tbody>\n" +
    "</table>\n" +
    "");
}]);

angular.module("partials/directives/ui-filesystem/layouts/large-tiles.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("partials/directives/ui-filesystem/layouts/large-tiles.html",
    "<!-- Copyright Heddoko(TM) 2015, all rights reserved. -->\n" +
    "\n" +
    "<div class=\"row\">\n" +
    "\n" +
    "    <!-- Parent folder -->\n" +
    "    <div ng-if=\"parentFolder\" class=\"col-sm-12 col-md-6 col-lg-4\">\n" +
    "        <a href=\"{{ ::parentFolder.href }}\" class=\"aspect-ratio aspect-8-5 card parent-folder\">\n" +
    "            <div>\n" +
    "                <i class=\"fa fa-hand-o-left fa-5x\"></i>\n" +
    "                <span class=\"title\">\n" +
    "                    {{ ::parentFolder.title || 'Back' }}\n" +
    "                </span>\n" +
    "            </div>\n" +
    "        </a>\n" +
    "    </div>\n" +
    "\n" +
    "    <!-- Other folders -->\n" +
    "    <div ng-repeat=\"folder in folders track by $index\" class=\"col-sm-12 col-md-6 col-lg-4\">\n" +
    "        <a\n" +
    "            href=\"{{ ::folder.href }}\"\n" +
    "            title=\"{{ ::folder.title }}\"\n" +
    "            class=\"aspect-ratio aspect-8-5 card folder\">\n" +
    "            <div>\n" +
    "\n" +
    "                <i class=\"fa fa-folder-open-o folder-icon\"></i>\n" +
    "\n" +
    "                <!-- Folder actions -->\n" +
    "                <div class=\"data\">\n" +
    "                    <section>\n" +
    "\n" +
    "                        <!-- Favourite (demo) -->\n" +
    "                        <i class=\"fa fa-star-o fa-2x pull-right\"></i>\n" +
    "\n" +
    "                        <!-- Title -->\n" +
    "                        <span class=\"title\">\n" +
    "                            {{ ::folder.title | characters:25 }}\n" +
    "                        </span>\n" +
    "\n" +
    "                        <!-- Sub title -->\n" +
    "                        <span class=\"sub-title\" ng-if=\"folder.updatedAt\">\n" +
    "                            Last updated on {{ ::folder.updatedAt | mysqlDate:'MMM d, yyyy' }}\n" +
    "                        </span>\n" +
    "                    </section>\n" +
    "\n" +
    "                    <section>\n" +
    "\n" +
    "                        <!-- Analyze -->\n" +
    "                        <button\n" +
    "                            ng-if=\"config.onAnalyzeFolder\"\n" +
    "                            ng-click=\"call($event, config.onAnalyzeFolder, [folder])\"\n" +
    "                            type=\"button\"\n" +
    "                            class=\"data-btn\">\n" +
    "\n" +
    "                            <i class=\"fa fa-line-chart fa-fw fa-2x\"></i>\n" +
    "                            <span>\n" +
    "                                Analyze\n" +
    "                            </span>\n" +
    "                        </button>\n" +
    "\n" +
    "                        <!-- Compare -->\n" +
    "                        <button\n" +
    "                            ng-if=\"config.onCompareFolder\"\n" +
    "                            ng-click=\"call($event, config.onCompareFolder, [folder])\"\n" +
    "                            type=\"button\"\n" +
    "                            class=\"data-btn\">\n" +
    "\n" +
    "                            <i class=\"fa fa-random fa-fw fa-2x\"></i>\n" +
    "                            <span>\n" +
    "                                Compare\n" +
    "                            </span>\n" +
    "                        </button>\n" +
    "\n" +
    "                        <!-- Share -->\n" +
    "                        <button\n" +
    "                            ng-if=\"config.onShareFolder\"\n" +
    "                            ng-click=\"call($event, config.onShareFolder, [folder])\"\n" +
    "                            type=\"button\"\n" +
    "                            class=\"data-btn\">\n" +
    "\n" +
    "                            <i class=\"fa fa-share-alt fa-fw fa-2x\"></i>\n" +
    "                            <span>\n" +
    "                                Share\n" +
    "                            </span>\n" +
    "                        </button>\n" +
    "\n" +
    "                        <!-- Edit -->\n" +
    "                        <button\n" +
    "                            ng-if=\"config.onEditFolder\"\n" +
    "                            ng-click=\"call($event, config.onEditFolder, [folder])\"\n" +
    "                            type=\"button\"\n" +
    "                            class=\"data-btn\">\n" +
    "\n" +
    "                            <i class=\"fa fa-pencil fa-fw fa-2x\"></i>\n" +
    "                            <span>\n" +
    "                                Edit\n" +
    "                            </span>\n" +
    "                        </button>\n" +
    "\n" +
    "                        <!-- Delete -->\n" +
    "                        <button\n" +
    "                            ng-if=\"config.onDeleteFolder\"\n" +
    "                            ng-click=\"call($event, config.onDeleteFolder, [folder.id])\"\n" +
    "                            type=\"button\"\n" +
    "                            href=\"javascript:;\"\n" +
    "                            class=\"data-btn\">\n" +
    "\n" +
    "                            <i class=\"fa fa-trash fa-fw fa-2x\"></i>\n" +
    "                            <span>\n" +
    "                                Delete\n" +
    "                            </span>\n" +
    "                        </button>\n" +
    "                    </section>\n" +
    "                </div>\n" +
    "            </div>\n" +
    "        </a>\n" +
    "    </div>\n" +
    "\n" +
    "    <!-- Files -->\n" +
    "    <div ng-repeat=\"file in files track by $index\" class=\"col-sm-12 col-md-6 col-lg-4\">\n" +
    "        <a\n" +
    "            href=\"{{ ::file.href }}\"\n" +
    "            title=\"{{ ::file.title }}\"\n" +
    "            class=\"aspect-ratio aspect-8-5 card file\">\n" +
    "            <div style=\"background-image: url({{ ::file.image || '' }})\">\n" +
    "\n" +
    "                <!-- File actions -->\n" +
    "                <div class=\"data\">\n" +
    "                    <section>\n" +
    "\n" +
    "                        <!-- Favourite (demo) -->\n" +
    "                        <i class=\"fa fa-star-o fa-2x pull-right\"></i>\n" +
    "\n" +
    "                        <!-- Title -->\n" +
    "                        <span class=\"title\">\n" +
    "                            {{ ::file.title | characters:25 }}\n" +
    "                        </span>\n" +
    "\n" +
    "                        <!-- Sub title -->\n" +
    "                        <span class=\"sub-title\" ng-if=\"file.subTitle\">\n" +
    "                            {{ ::file.subTitle | characters:25 }}\n" +
    "                        </span>\n" +
    "                    </section>\n" +
    "\n" +
    "                    <section>\n" +
    "\n" +
    "                        <!-- Analyze -->\n" +
    "                        <button\n" +
    "                            ng-if=\"config.onAnalyzeFile\"\n" +
    "                            ng-click=\"call($event, config.onAnalyzeFile, [file])\"\n" +
    "                            type=\"button\"\n" +
    "                            class=\"data-btn\">\n" +
    "\n" +
    "                            <i class=\"fa fa-line-chart fa-fw fa-2x\"></i>\n" +
    "                            <span>\n" +
    "                                Analyze\n" +
    "                            </span>\n" +
    "                        </button>\n" +
    "\n" +
    "                        <!-- Compare -->\n" +
    "                        <button\n" +
    "                            ng-if=\"config.onCompareFile\"\n" +
    "                            ng-click=\"call($event, config.onCompareFile, [file])\"\n" +
    "                            type=\"button\"\n" +
    "                            class=\"data-btn\">\n" +
    "\n" +
    "                            <i class=\"fa fa-random fa-fw fa-2x\"></i>\n" +
    "                            <span>\n" +
    "                                Compare\n" +
    "                            </span>\n" +
    "                        </button>\n" +
    "\n" +
    "                        <!-- Share -->\n" +
    "                        <button\n" +
    "                            ng-if=\"config.onShareFile\"\n" +
    "                            ng-click=\"call($event, config.onShareFile, [file])\"\n" +
    "                            type=\"button\"\n" +
    "                            class=\"data-btn\">\n" +
    "\n" +
    "                            <i class=\"fa fa-share-alt fa-fw fa-2x\"></i>\n" +
    "                            <span>\n" +
    "                                Share\n" +
    "                            </span>\n" +
    "                        </button>\n" +
    "\n" +
    "                        <!-- Edit -->\n" +
    "                        <button\n" +
    "                            ng-if=\"config.onEditFile\"\n" +
    "                            ng-click=\"call($event, config.onEditFile, [file])\"\n" +
    "                            type=\"button\"\n" +
    "                            class=\"data-btn\">\n" +
    "\n" +
    "                            <i class=\"fa fa-pencil fa-fw fa-2x\"></i>\n" +
    "                            <span>\n" +
    "                                Edit\n" +
    "                            </span>\n" +
    "                        </button>\n" +
    "\n" +
    "                        <!-- Delete -->\n" +
    "                        <button\n" +
    "                            ng-if=\"config.onDeleteFile\"\n" +
    "                            ng-click=\"call($event, config.onDeleteFile, [file.id])\"\n" +
    "                            type=\"button\"\n" +
    "                            href=\"javascript:;\"\n" +
    "                            class=\"data-btn\">\n" +
    "\n" +
    "                            <i class=\"fa fa-trash fa-fw fa-2x\"></i>\n" +
    "                            <span>\n" +
    "                                Delete\n" +
    "                            </span>\n" +
    "                        </button>\n" +
    "                    </section>\n" +
    "                </div>\n" +
    "            </div>\n" +
    "        </a>\n" +
    "    </div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("partials/directives/ui-filesystem/layouts/small-tiles.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("partials/directives/ui-filesystem/layouts/small-tiles.html",
    "<!-- Copyright Heddoko(TM) 2015, all rights reserved. -->\n" +
    "\n" +
    "<div class=\"row\">\n" +
    "\n" +
    "    <!-- Parent folder -->\n" +
    "    <div ng-if=\"parentFolder\" class=\"col-sm-6 col-md-4 col-lg-2\">\n" +
    "        <a href=\"{{ ::parentFolder.href }}\" class=\"aspect-ratio aspect-square card parent-folder\">\n" +
    "            <div>\n" +
    "                <i class=\"fa fa-hand-o-left fa-2x\"></i>\n" +
    "                <span class=\"title\">\n" +
    "                    {{ ::parentFolder.title || 'Back' }}\n" +
    "                </span>\n" +
    "            </div>\n" +
    "        </a>\n" +
    "    </div>\n" +
    "\n" +
    "    <!-- Other folders -->\n" +
    "    <div ng-repeat=\"folder in folders track by $index\" class=\"col-sm-6 col-md-4 col-lg-2\">\n" +
    "        <a\n" +
    "            href=\"{{ ::folder.href }}\"\n" +
    "            title=\"{{ ::folder.title }}\"\n" +
    "            class=\"aspect-ratio aspect-square card folder\">\n" +
    "            <div>\n" +
    "\n" +
    "                <i class=\"fa fa-folder-open-o folder-icon\"></i>\n" +
    "\n" +
    "                <!-- Folder actions -->\n" +
    "                <div class=\"data\">\n" +
    "                    <section>\n" +
    "\n" +
    "                        <!-- Favourite (demo) -->\n" +
    "                        <i class=\"fa fa-star-o pull-right\"></i>\n" +
    "\n" +
    "                        <!-- Title -->\n" +
    "                        <span class=\"title\">\n" +
    "                            {{ ::folder.title | characters:12 }}\n" +
    "                        </span>\n" +
    "\n" +
    "                        <!-- Sub title -->\n" +
    "                        <span class=\"sub-title\" ng-if=\"folder.updatedAt\">\n" +
    "                            Last updated {{ ::folder.updatedAt | mysqlDate:'MMM d, yyyy' }}\n" +
    "                        </span>\n" +
    "                    </section>\n" +
    "\n" +
    "                    <section>\n" +
    "\n" +
    "                        <!-- Analyze -->\n" +
    "                        <button\n" +
    "                            ng-if=\"config.onAnalyzeFolder\"\n" +
    "                            ng-click=\"call($event, config.onAnalyzeFolder, [folder])\"\n" +
    "                            type=\"button\"\n" +
    "                            class=\"data-btn\">\n" +
    "\n" +
    "                            <i class=\"fa fa-line-chart fa-fw\"></i>\n" +
    "                            <span>\n" +
    "                                Analyze\n" +
    "                            </span>\n" +
    "                        </button>\n" +
    "\n" +
    "                        <!-- Compare -->\n" +
    "                        <button\n" +
    "                            ng-if=\"config.onCompareFolder\"\n" +
    "                            ng-click=\"call($event, config.onCompareFolder, [folder])\"\n" +
    "                            type=\"button\"\n" +
    "                            class=\"data-btn\">\n" +
    "\n" +
    "                            <i class=\"fa fa-random fa-fw\"></i>\n" +
    "                            <span>\n" +
    "                                Compare\n" +
    "                            </span>\n" +
    "                        </button>\n" +
    "\n" +
    "                        <!-- Share -->\n" +
    "                        <button\n" +
    "                            ng-if=\"config.onShareFolder\"\n" +
    "                            ng-click=\"call($event, config.onShareFolder, [folder])\"\n" +
    "                            type=\"button\"\n" +
    "                            class=\"data-btn\">\n" +
    "\n" +
    "                            <i class=\"fa fa-share-alt fa-fw\"></i>\n" +
    "                            <span>\n" +
    "                                Share\n" +
    "                            </span>\n" +
    "                        </button>\n" +
    "\n" +
    "                        <!-- Edit -->\n" +
    "                        <button\n" +
    "                            ng-if=\"config.onEditFolder\"\n" +
    "                            ng-click=\"call($event, config.onEditFolder, [folder])\"\n" +
    "                            type=\"button\"\n" +
    "                            class=\"data-btn\">\n" +
    "\n" +
    "                            <i class=\"fa fa-pencil fa-fw\"></i>\n" +
    "                            <span>\n" +
    "                                Edit\n" +
    "                            </span>\n" +
    "                        </button>\n" +
    "\n" +
    "                        <!-- Delete -->\n" +
    "                        <button\n" +
    "                            ng-if=\"config.onDeleteFolder\"\n" +
    "                            ng-click=\"call($event, config.onDeleteFolder, [folder.id])\"\n" +
    "                            type=\"button\"\n" +
    "                            href=\"javascript:;\"\n" +
    "                            class=\"data-btn\">\n" +
    "\n" +
    "                            <i class=\"fa fa-trash fa-fw\"></i>\n" +
    "                            <span>\n" +
    "                                Delete\n" +
    "                            </span>\n" +
    "                        </button>\n" +
    "                    </section>\n" +
    "                </div>\n" +
    "            </div>\n" +
    "        </a>\n" +
    "    </div>\n" +
    "\n" +
    "    <!-- Files -->\n" +
    "    <div ng-repeat=\"file in files track by $index\" class=\"col-sm-6 col-md-4 col-lg-2\">\n" +
    "        <a\n" +
    "            href=\"{{ ::file.href }}\"\n" +
    "            title=\"{{ ::file.title }}\"\n" +
    "            class=\"aspect-ratio aspect-square card file\">\n" +
    "            <div style=\"background-image: url({{ ::file.image || '' }})\">\n" +
    "\n" +
    "                <!-- File actions -->\n" +
    "                <div class=\"data\">\n" +
    "                    <section>\n" +
    "\n" +
    "                        <!-- Favourite (demo) -->\n" +
    "                        <i class=\"fa fa-star-o pull-right\"></i>\n" +
    "\n" +
    "                        <!-- Title -->\n" +
    "                        <span class=\"title\">\n" +
    "                            {{ ::file.title | characters:12 }}\n" +
    "                        </span>\n" +
    "\n" +
    "                        <!-- Sub title -->\n" +
    "                        <span class=\"sub-title\" ng-if=\"file.subTitle\">\n" +
    "                            {{ ::file.subTitle | characters:25 }}\n" +
    "                        </span>\n" +
    "                    </section>\n" +
    "\n" +
    "                    <section>\n" +
    "\n" +
    "                        <!-- Analyze -->\n" +
    "                        <button\n" +
    "                            ng-if=\"config.onAnalyzeFile\"\n" +
    "                            ng-click=\"call($event, config.onAnalyzeFile, [file])\"\n" +
    "                            type=\"button\"\n" +
    "                            class=\"data-btn\">\n" +
    "\n" +
    "                            <i class=\"fa fa-line-chart fa-fw\"></i>\n" +
    "                            <span>\n" +
    "                                Analyze\n" +
    "                            </span>\n" +
    "                        </button>\n" +
    "\n" +
    "                        <!-- Compare -->\n" +
    "                        <button\n" +
    "                            ng-if=\"config.onCompareFile\"\n" +
    "                            ng-click=\"call($event, config.onCompareFile, [file])\"\n" +
    "                            type=\"button\"\n" +
    "                            class=\"data-btn\">\n" +
    "\n" +
    "                            <i class=\"fa fa-random fa-fw\"></i>\n" +
    "                            <span>\n" +
    "                                Compare\n" +
    "                            </span>\n" +
    "                        </button>\n" +
    "\n" +
    "                        <!-- Share -->\n" +
    "                        <button\n" +
    "                            ng-if=\"config.onShareFile\"\n" +
    "                            ng-click=\"call($event, config.onShareFile, [file])\"\n" +
    "                            type=\"button\"\n" +
    "                            class=\"data-btn\">\n" +
    "\n" +
    "                            <i class=\"fa fa-share-alt fa-fw\"></i>\n" +
    "                            <span>\n" +
    "                                Share\n" +
    "                            </span>\n" +
    "                        </button>\n" +
    "\n" +
    "                        <!-- Edit -->\n" +
    "                        <button\n" +
    "                            ng-if=\"config.onEditFile\"\n" +
    "                            ng-click=\"call($event, config.onEditFile, [file])\"\n" +
    "                            type=\"button\"\n" +
    "                            class=\"data-btn\">\n" +
    "\n" +
    "                            <i class=\"fa fa-pencil fa-fw\"></i>\n" +
    "                            <span>\n" +
    "                                Edit\n" +
    "                            </span>\n" +
    "                        </button>\n" +
    "\n" +
    "                        <!-- Delete -->\n" +
    "                        <button\n" +
    "                            ng-if=\"config.onDeleteFile\"\n" +
    "                            ng-click=\"call($event, config.onDeleteFile, [file.id])\"\n" +
    "                            type=\"button\"\n" +
    "                            class=\"data-btn\">\n" +
    "\n" +
    "                            <i class=\"fa fa-trash fa-fw\"></i>\n" +
    "                            <span>\n" +
    "                                Delete\n" +
    "                            </span>\n" +
    "                        </button>\n" +
    "                    </section>\n" +
    "                </div>\n" +
    "            </div>\n" +
    "        </a>\n" +
    "    </div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("partials/directives/ui-movement/placeholder.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("partials/directives/ui-movement/placeholder.html",
    "<!-- Copyright Heddoko(TM) 2015, all rights reserved. -->\n" +
    "\n" +
    "<div class=\"ui-movement-card four-three-aspect-ratio\">\n" +
    "    <div class=\"ui-movement-placeholder\">\n" +
    "        Movement Placeholder\n" +
    "    </div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("partials/directives/ui-movement/playback.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("partials/directives/ui-movement/playback.html",
    "<!-- Copyright Heddoko(TM) 2016, all rights reserved. -->\n" +
    "\n" +
    "<div class=\"ui-movement-container\" ng-style=\"{height: height}\">\n" +
    "\n" +
    "    <h1 ng-style=\"{'text-align': titleAlign}\">\n" +
    "        <a ng-if=\"previousMovement\" ng-click=\"previousMovement()\" href=\"javascript:;\">\n" +
    "            <i class=\"fa fa-arrow-left fa-fw\"></i>\n" +
    "        </a>\n" +
    "\n" +
    "        {{ title }}\n" +
    "\n" +
    "        <a ng-if=\"nextMovement\" ng-click=\"nextMovement()\" href=\"javascript:;\">\n" +
    "            <i class=\"fa fa-arrow-right fa-fw\"></i>\n" +
    "        </a>\n" +
    "    </h1>\n" +
    "\n" +
    "    <div style=\"display: block; margin: 50px 0; text-align: center\" class=\"text-muted\">\n" +
    "        <i class=\"fa fa-cubes fa-fw\"></i> In Development...\n" +
    "    </div>\n" +
    "\n" +
    "    <div class=\"controls\">\n" +
    "\n" +
    "        <a href=\"javascript:;\" class=\"fa fa-arrow-up controls-up\"></a>\n" +
    "        <a href=\"javascript:;\" class=\"fa fa-arrow-right controls-right\"></a>\n" +
    "        <a href=\"javascript:;\" class=\"fa fa-arrow-down controls-down\"></a>\n" +
    "        <a href=\"javascript:;\" class=\"fa fa-arrow-left controls-left\"></a>\n" +
    "\n" +
    "    </div>\n" +
    "\n" +
    "</div>\n" +
    "");
}]);

angular.module("partials/directives/ui-movement/preview.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("partials/directives/ui-movement/preview.html",
    "<!-- Copyright Heddoko(TM) 2015, all rights reserved. -->\n" +
    "\n" +
    "<div class=\"ui-movement-card aspect-ratio aspect-4-3\">\n" +
    "    <div class=\"ui-movement-placeholder\">\n" +
    "        Movement Preview <br>\n" +
    "        <span style=\"color: #ccc\">\n" +
    "            In Development...\n" +
    "        </span>\n" +
    "    </div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("partials/directives/ui-page-title.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("partials/directives/ui-page-title.html",
    "<!-- Title -->\n" +
    "<span class=\"ui-page-title-background\">\n" +
    "    <span class=\"ui-page-title-skew\">\n" +
    "        <span class=\"ui-page-title\">\n" +
    "            {{ title }} <i ng-show=\"icon\" ng-class=\"'fa fa-' + icon\"></i>\n" +
    "        </span>\n" +
    "    </span>\n" +
    "</span>\n" +
    "\n" +
    "<div class=\"clearfix\"></div>\n" +
    "");
}]);

angular.module("partials/directives/ui-taggable-input.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("partials/directives/ui-taggable-input.html",
    "<div class=\"ui-taggable-input\">\n" +
    "    <selectize\n" +
    "        ng-model=\"data\"\n" +
    "        class=\"form-control text-left\"\n" +
    "        config=\"config\"\n" +
    "        options=\"options\"\n" +
    "        placeholder=\"Start typing to add tags\">\n" +
    "    </selectize>\n" +
    "</div>\n" +
    "");
}]);

angular.module("partials/header.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("partials/header.html",
    "<header class=\"clearfix\">\n" +
    "	<div class=\"top-nav\">\n" +
    "		<ul class=\"nav-left list-unstyled\">\n" +
    "\n" +
    "            <!-- Menu button (shows up in mobile views) -->\n" +
    "			<li>\n" +
    "				<div class=\"menu-button\" toggle-off-canvas>\n" +
    "					<span class=\"icon-bar\"></span>\n" +
    "					<span class=\"icon-bar\"></span>\n" +
    "					<span class=\"icon-bar\"></span>\n" +
    "				</div>\n" +
    "			</li>\n" +
    "\n" +
    "            <!-- Menu button (shows up on desktop views) -->\n" +
    "			<li>\n" +
    "				<a href=\"#/\" data-toggle-min-nav class=\"toggle-min\">\n" +
    "                    <i class=\"fa fa-bars\"></i>\n" +
    "                </a>\n" +
    "			</li>\n" +
    "\n" +
    "            <!-- Account -->\n" +
    "            <!-- <li ng-show=\"global.isLocal\" class=\"dropdown text-normal nav-profile\">\n" +
    "                <a\n" +
    "                    href=\"javascript:;\"\n" +
    "                    class=\"dropdown-toggle\"\n" +
    "                    id=\"menuAccountDropdown\"\n" +
    "                    data-toggle=\"dropdown\"\n" +
    "                    aria-haspopup=\"true\"\n" +
    "                    aria-expanded=\"true\">\n" +
    "\n" +
    "                    <i class=\"fa fa-user\"></i>\n" +
    "                </a>\n" +
    "\n" +
    "                <div\n" +
    "                    class=\"dropdown-menu with-arrow pull-left panel panel-default\"\n" +
    "                    aria-labelledby=\"menuAccountDropdown\">\n" +
    "\n" +
    "                    <ul class=\"list-group\">\n" +
    "                        <li class=\"list-group-item\">\n" +
    "                            <a href=\"#/account\">\n" +
    "                                <i class=\"fa fa-user color-info\"></i>\n" +
    "                                <span>My Account</span>\n" +
    "                            </a>\n" +
    "                        </li>\n" +
    "                        <li class=\"list-group-item\">\n" +
    "                            <a ng-click=\"global.endSession()\" href=\"javascript:;\">\n" +
    "                                <i class=\"fa fa-sign-out fa-fw\"></i>\n" +
    "                                <span>Log out</span>\n" +
    "                            </a>\n" +
    "                        </li>\n" +
    "                    </ul>\n" +
    "                </div>\n" +
    "            </li> -->\n" +
    "		</ul>\n" +
    "\n" +
    "        <ul class=\"nav-right list-unstyled\">\n" +
    "\n" +
    "            <!-- Search input -->\n" +
    "            <li>\n" +
    "                <div ng-controller=\"SearchController\" class=\"search-input\">\n" +
    "                    <selectize\n" +
    "                        ng-model=\"model\"\n" +
    "                        class=\"form-control\"\n" +
    "                        config=\"config\"\n" +
    "                        options=\"options\">\n" +
    "                    </selectize>\n" +
    "                </div>\n" +
    "            </li>\n" +
    "\n" +
    "            <!-- Notifications -->\n" +
    "            <!-- <li ng-show=\"global.isLocal\" class=\"dropdown\">\n" +
    "                <a\n" +
    "                    href=\"javascript:;\"\n" +
    "                    class=\"dropdown-toggle onboarding-general\"\n" +
    "                    id=\"menuAccountDropdown\"\n" +
    "                    data-toggle=\"dropdown\"\n" +
    "                    aria-haspopup=\"true\"\n" +
    "                    aria-expanded=\"true\"\n" +
    "                    data-intro=\"Whenever you do something in the app, it will show up as a notification here\">\n" +
    "\n" +
    "                    <i class=\"fa fa-bell fa-fw\"></i>\n" +
    "                    <span>Notifications</span>\n" +
    "                </a>\n" +
    "\n" +
    "                <div\n" +
    "                    class=\"dropdown-menu with-arrow pull-right panel panel-default\"\n" +
    "                    aria-labelledby=\"menuNotificationsDropdown\">\n" +
    "\n" +
    "                    <div class=\"panel-heading\">\n" +
    "                        You have 2 notifications.\n" +
    "                    </div>\n" +
    "\n" +
    "                    <ul class=\"list-group\">\n" +
    "                        <li class=\"list-group-item\">\n" +
    "                            <a href=\"javascript:;\" class=\"media\">\n" +
    "                                <span class=\"pull-left media-icon\">\n" +
    "                                    <span class=\"circle-icon sm bg-success\">\n" +
    "                                        <i class=\"fa fa-bell-o\"></i>\n" +
    "                                    </span>\n" +
    "                                </span>\n" +
    "\n" +
    "                                <div class=\"media-body\">\n" +
    "                                    <span class=\"block\">New tasks needs to be done</span>\n" +
    "                                    <span class=\"text-muted block\">2min ago</span>\n" +
    "                                </div>\n" +
    "                            </a>\n" +
    "                        </li>\n" +
    "                        <li class=\"list-group-item\">\n" +
    "                            <a href=\"javascript:;\" class=\"media\">\n" +
    "                                <span class=\"pull-left media-icon\">\n" +
    "                                    <span class=\"circle-icon sm bg-info\">\n" +
    "                                        <i class=\"fa fa-bell-o\"></i>\n" +
    "                                    </span>\n" +
    "                                </span>\n" +
    "\n" +
    "                                <div class=\"media-body\">\n" +
    "                                    <span class=\"block\">Change your password</span>\n" +
    "                                    <span class=\"text-muted\">3 hours ago</span>\n" +
    "                                </div>\n" +
    "                            </a>\n" +
    "                        </li>\n" +
    "                        <li class=\"list-group-item\">\n" +
    "                            <a href=\"javascript:;\" class=\"media\">\n" +
    "                                <span class=\"pull-left media-icon\">\n" +
    "                                    <span class=\"circle-icon sm bg-danger\">\n" +
    "                                        <i class=\"fa fa-bell-o\"></i>\n" +
    "                                    </span>\n" +
    "                                </span>\n" +
    "\n" +
    "                                <div class=\"media-body\">\n" +
    "                                    <span class=\"block\">New feature added</span>\n" +
    "                                    <span class=\"text-muted\">9 hours ago</span>\n" +
    "                                </div>\n" +
    "                            </a>\n" +
    "                        </li>\n" +
    "                    </ul>\n" +
    "\n" +
    "                    <div class=\"panel-footer\">\n" +
    "                        <a href=\"javascript:;\">\n" +
    "                            Show all notifications.\n" +
    "                        </a>\n" +
    "                    </div>\n" +
    "                </div>\n" +
    "            </li> -->\n" +
    "\n" +
    "            <!-- App settings -->\n" +
    "            <li>\n" +
    "                <a\n" +
    "                    href=\"#/config\"\n" +
    "                    class=\"onboarding-general\"\n" +
    "                    data-step=\"4\"\n" +
    "                    data-intro=\"Click this button to modify your app settings\">\n" +
    "\n" +
    "                    <i class=\"fa fa-cogs fa-fw\"></i>\n" +
    "                    <span>Settings</span>\n" +
    "                </a>\n" +
    "            </li>\n" +
    "\n" +
    "            <li ng-if=\"global.isAdmin\">\n" +
    "                <a\n" +
    "                    href=\"/admin\"\n" +
    "                    class=\"onboarding-general\">\n" +
    "                    <i class=\"fa fa-users fa-fw\"></i>\n" +
    "                    <span>Admin</span>\n" +
    "                </a>\n" +
    "            </li>\n" +
    "\n" +
    "            <!-- Onboarding -->\n" +
    "            <li>\n" +
    "                <a\n" +
    "                    ng-click=\"global.onboarding.general()\"\n" +
    "                    href=\"javascript:;\"\n" +
    "                    class=\"onboarding-general\"\n" +
    "                    data-step=\"6\"\n" +
    "                    data-intro=\"Click here whenever you want to see this again\">\n" +
    "\n" +
    "                    <i class=\"fa fa-question fa-fw\"></i>\n" +
    "                    <span>Help</span>\n" +
    "                </a>\n" +
    "            </li>\n" +
    "\n" +
    "            <!-- Sign out -->\n" +
    "            <li>\n" +
    "                <a\n" +
    "                    ng-click=\"global.endSession()\"\n" +
    "                    href=\"javascript:;\"\n" +
    "                    class=\"onboarding-general\"\n" +
    "                    data-step=\"5\"\n" +
    "                    data-intro=\"Use this button when you're ready to log out\">\n" +
    "\n" +
    "                    <i class=\"fa fa-sign-out fa-fw\"></i>\n" +
    "                    <span>Log out</span>\n" +
    "                </a>\n" +
    "            </li>\n" +
    "        </ul>\n" +
    "\n" +
    "	</div>\n" +
    "</header>\n" +
    "");
}]);

angular.module("partials/loading.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("partials/loading.html",
    "<!-- Copyright Heddoko(TM) 2016, all rights reserved. -->\n" +
    "\n" +
    "<div class=\"row\">\n" +
    "    <div class=\"col-sm-12 loading-notice\">\n" +
    "        <h3>\n" +
    "            Please bear with us\n" +
    "            <br>\n" +
    "\n" +
    "            while we retrieve your data.\n" +
    "        </h3>\n" +
    "        <i class=\"fa fa-spinner fa-spin fa-2x\"></i>\n" +
    "    </div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("partials/navigation.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("partials/navigation.html",
    "<!-- Copyright Heddoko(TM) 2015, all rights reserved. -->\n" +
    "\n" +
    "<div class=\"logo\">\n" +
    "    <div class=\"logo-container\">\n" +
    "        <img src=\"../../images/logos/heddoko_logo_1.svg\" alt=\"Heddoko\" class=\"logo1\">\n" +
    "        <img src=\"../../images/logos/heddoko_logo_2.svg\" alt=\"Heddoko\" class=\"logo2\">\n" +
    "    </div>\n" +
    "</div>\n" +
    "\n" +
    "<div id=\"nav-wrapper\">\n" +
    "    <ul id=\"nav\" data-slim-scroll data-collapse-nav data-highlight-active>\n" +
    "\n" +
    "        <!-- Landing page -->\n" +
    "        <li\n" +
    "            class=\"onboarding-general\"\n" +
    "            data-step=\"1\"\n" +
    "            data-intro=\"This is a shortcut to your dashboard.\"\n" +
    "            data-position=\"right\">\n" +
    "\n" +
    "            <a href=\"#/dashboard\">\n" +
    "                <i class=\"fa fa-dashboard\"></i>\n" +
    "                <span>Dashboard</span>\n" +
    "            </a>\n" +
    "        </li>\n" +
    "\n" +
    "        <!-- Groups -->\n" +
    "		<li>\n" +
    "            <!-- List of groups -->\n" +
    "            <a ng-if=\"global.data.group && global.data.group.length > 0\" href=\"#/group\">\n" +
    "                <i class=\"fa fa-users\"></i>\n" +
    "                <span>Teams</span>\n" +
    "            </a>\n" +
    "\n" +
    "            <ul ng-if=\"global.data.group && global.data.group.length > 0\" style=\"display: block\">\n" +
    "                <li>\n" +
    "                    <a href=\"#/groups/{{ global.getSelectedGroup().id }}\">\n" +
    "                        <i class=\"fa fa-angle-right\"></i>\n" +
    "                        <span>{{ global.getSelectedGroup().name }}</span>\n" +
    "                    </a>\n" +
    "                </li>\n" +
    "                <li>\n" +
    "                    <a href=\"#/group\">\n" +
    "                        <i class=\"fa fa-angle-right\"></i>\n" +
    "                        <span>View All Teams</span>\n" +
    "                    </a>\n" +
    "                </li>\n" +
    "            </ul>\n" +
    "\n" +
    "            <!-- Fetching groups -->\n" +
    "            <a ng-if=\"global.data.isFetchingGroups === true\" href=\"javascript:;\">\n" +
    "                <i class=\"fa fa-spinner fa-spin\"></i>\n" +
    "                <span>Teams</span>\n" +
    "            </a>\n" +
    "\n" +
    "            <!-- No groups available -->\n" +
    "            <a\n" +
    "                ng-if=\"global.data.isFetchingGroups === false &&\n" +
    "                    global.data.group.length === 0\"\n" +
    "                href=\"javascript:;\">\n" +
    "\n" +
    "                <i class=\"fa fa-users\"></i>\n" +
    "                <span>Create a Team</span>\n" +
    "            </a>\n" +
    "        </li>\n" +
    "\n" +
    "        <!-- Profiles -->\n" +
    "		<li>\n" +
    "            <!-- List of profiles -->\n" +
    "            <a ng-if=\"global.data.profile && global.data.profile.length > 0\" href=\"#/profiles\">\n" +
    "                <i class=\"fa fa-user\"></i>\n" +
    "                <span>Athletes</span>\n" +
    "                <span class=\"pull-right\">\n" +
    "                    <small>\n" +
    "                        ( {{ global.data.profile.length }} )\n" +
    "                    </small>\n" +
    "                </span>\n" +
    "            </a>\n" +
    "\n" +
    "            <!-- Fetching profiles -->\n" +
    "            <a ng-if=\"global.data.isFetchingProfiles === true\" href=\"javascript:;\">\n" +
    "                <i class=\"fa fa-spinner fa-spin\"></i>\n" +
    "                <span>Athletes</span>\n" +
    "            </a>\n" +
    "\n" +
    "            <!-- No profiles available -->\n" +
    "            <a\n" +
    "                ng-if=\"global.data.isFetchingProfiles === false && global.data.profile.length === 0\"\n" +
    "                href=\"#/profiles/create\">\n" +
    "\n" +
    "                <i class=\"fa fa-user\"></i>\n" +
    "                <span>Create a Profile</span>\n" +
    "            </a>\n" +
    "        </li>\n" +
    "\n" +
    "        <!-- Movements -->\n" +
    "		<li>\n" +
    "            <a href=\"#/movements\">\n" +
    "                <i class=\"fa fa-th\"></i>\n" +
    "                <span>Movements</span>\n" +
    "            </a>\n" +
    "\n" +
    "            <ul style=\"display: block\">\n" +
    "                <li>\n" +
    "                    <a href=\"#/movements\">\n" +
    "                        <i class=\"fa fa-angle-right\"></i>\n" +
    "                        <span>Organize</span>\n" +
    "                    </a>\n" +
    "                </li>\n" +
    "                <li>\n" +
    "                    <a href=\"#/capture/demo\">\n" +
    "                        <i class=\"fa fa-angle-right\"></i>\n" +
    "                        <span>Record</span>\n" +
    "                    </a>\n" +
    "                </li>\n" +
    "                <!-- <li>\n" +
    "                    <a href=\"#/capture\">\n" +
    "                        <i class=\"fa fa-angle-right\"></i>\n" +
    "                        <span>Record</span>\n" +
    "                    </a>\n" +
    "                </li> -->\n" +
    "                <li>\n" +
    "                    <a href=\"#/movements/upload\">\n" +
    "                        <i class=\"fa fa-angle-right\"></i>\n" +
    "                        <span>Upload</span>\n" +
    "                    </a>\n" +
    "                </li>\n" +
    "                <li>\n" +
    "                    <a href=\"#/movements/analyze/demo\">\n" +
    "                        <i class=\"fa fa-angle-right\"></i>\n" +
    "                        <span>Analyze (demo)</span>\n" +
    "                    </a>\n" +
    "                </li>\n" +
    "                <li>\n" +
    "                    <a href=\"#/movements/compare\">\n" +
    "                        <i class=\"fa fa-angle-right\"></i>\n" +
    "                        <span>Compare</span>\n" +
    "                    </a>\n" +
    "                </li>\n" +
    "            </ul>\n" +
    "        </li>\n" +
    "\n" +
    "        <!-- Screenings -->\n" +
    "		<li>\n" +
    "            <a href=\"#/screenings\">\n" +
    "                <i class=\"fa fa-pencil-square-o\"></i>\n" +
    "                <span>Movement Tests</span>\n" +
    "            </a>\n" +
    "\n" +
    "            <ul style=\"display: block\">\n" +
    "                <li>\n" +
    "                    <a href=\"#/screenings/live\">\n" +
    "                        <i class=\"fa fa-angle-right\"></i>\n" +
    "                        <span ng-if=\"global.store.liveScreeningId\">Live Test</span>\n" +
    "                        <span ng-if=\"!global.store.liveScreeningId\">New Test</span>\n" +
    "                    </a>\n" +
    "                </li>\n" +
    "                <li>\n" +
    "                    <a href=\"#/screenings\">\n" +
    "                        <i class=\"fa fa-angle-right\"></i>\n" +
    "                        <span>View Results</span>\n" +
    "                    </a>\n" +
    "                </li>\n" +
    "            </ul>\n" +
    "        </li>\n" +
    "\n" +
    "        <!-- Live FMS pages. -->\n" +
    "        <li ng-show=\"false\">\n" +
    "            <!-- TODO: Call this Live FMS? -->\n" +
    "            <a href=\"#/fms/live\"> <i class=\"fa fa-pencil-square-o\"></i> Movement Screen</a>\n" +
    "\n" +
    "            <!-- TODO: Use ng-repeat when ready -->\n" +
    "            <ul style=\"display: block\">\n" +
    "                <li>\n" +
    "                    <a href=\"#/fms/live/ds\"><span>Deep Squat</span></a>\n" +
    "                </li>\n" +
    "                <li>\n" +
    "                    <a href=\"#/fms/live/hs\"><span>Hurdle Step</span></a>\n" +
    "                </li>\n" +
    "                <li>\n" +
    "                    <a href=\"#/fms/live/il\"><span>Inline Lunge</span></a>\n" +
    "                </li>\n" +
    "                <li>\n" +
    "                    <a href=\"#/fms/live/sm\"><span>Shoulder Mobility</span></a>\n" +
    "                </li>\n" +
    "                <li>\n" +
    "                    <a href=\"#/fms/live/sm\"><span>Impingement (C. Test)</span></a>\n" +
    "                </li>\n" +
    "                <li>\n" +
    "                    <a href=\"#/fms/demo/aslr\"><span>Active Straight-Leg Raise</span></a>\n" +
    "                </li>\n" +
    "                <li>\n" +
    "                    <a href=\"#/fms/live/tspu\"><span>Trunk Stability Push-Up</span></a>\n" +
    "                </li>\n" +
    "                <li>\n" +
    "                    <a href=\"#/fms/live/pu\"><span>Press-Up (C. Test)</span></a>\n" +
    "                </li>\n" +
    "                <li>\n" +
    "                    <a href=\"#/fms/live/rs\"><span>Rotary Stability</span></a>\n" +
    "                </li>\n" +
    "                <li>\n" +
    "                    <a href=\"#/fms/live/ps\"><span>Posterior Rocking (C. Test)</span></a>\n" +
    "                </li>\n" +
    "            </ul>\n" +
    "        </li>\n" +
    "\n" +
    "        <!-- Account page -->\n" +
    "		<li>\n" +
    "            <a href=\"#/account\">\n" +
    "                <i class=\"fa fa-user\"></i>\n" +
    "                <span>My Account</span>\n" +
    "            </a>\n" +
    "        </li>\n" +
    "\n" +
    "        <li class=\"app-version\">\n" +
    "            {{ global.appVersion }}\n" +
    "		</li>\n" +
    "    </ul>\n" +
    "</div>\n" +
    "");
}]);

angular.module("partials/overlay.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("partials/overlay.html",
    "<!-- Copyright Heddoko(TM) 2015, all rights reserved. -->\n" +
    "\n" +
    "<div class=\"modal fade\" id=\"overlay-screen\" tabindex=\"-1\">\n" +
    "    <div class=\"modal-dialog modal-responsive\">\n" +
    "        <div class=\"modal-content\">\n" +
    "\n" +
    "            <!-- Overlay title -->\n" +
    "            <header class=\"modal-header\">\n" +
    "                <button\n" +
    "                    type=\"button\"\n" +
    "                    class=\"close\"\n" +
    "                    data-dismiss=\"modal\"\n" +
    "                    aria-label=\"Close\">\n" +
    "\n" +
    "                    <span aria-hidden=\"true\">&times;</span>\n" +
    "                </button>\n" +
    "                <h2 class=\"modal-title text-center\" id=\"overlay-screen-title\">\n" +
    "                    {{ global.data.overlay.title }}\n" +
    "                </h2>\n" +
    "            </header>\n" +
    "\n" +
    "            <!-- Overlay content -->\n" +
    "            <div\n" +
    "                data-ng-if=\"global.data.overlay.bodyTemplate.length > 0\"\n" +
    "                data-ng-include=\"global.data.overlay.bodyTemplate\"\n" +
    "                class=\"modal-body\">\n" +
    "            </div>\n" +
    "\n" +
    "            <!-- Overlay footer -->\n" +
    "            <footer\n" +
    "                data-ng-if=\"global.data.overlay.footerTemplate.length > 0\"\n" +
    "                data-ng-include=\"global.data.overlay.footerTemplate\"\n" +
    "                class=\"modal-footer\">\n" +
    "            </footer>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("partials/select-profile.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("partials/select-profile.html",
    "<!-- Copyright Heddoko(TM) 2015, all rights reserved. -->\n" +
    "\n" +
    "<h3 class=\"text-center\">Select a profile to get started</h3>\n" +
    "<br>\n" +
    "\n" +
    "<div class=\"col-md-4 col-md-offset-4 text-center\">\n" +
    "    <ui-profile-lookup\n" +
    "        profiles=\"global.data.profile.list\"\n" +
    "        select-profile=\"global.selectProfile(profile, true)\"\n" +
    "        placeholder=\"Lookup a profile\">\n" +
    "    </ui-profile-lookup>\n" +
    "    <br>\n" +
    "\n" +
    "    You can also take this opportunity to\n" +
    "    <a href=\"#/profiles/create\">create one</a>.\n" +
    "</div>\n" +
    "");
}]);

angular.module("profile/create.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("profile/create.html",
    "<!-- Copyright Heddoko(TM) 2016, all rights reserved. -->\n" +
    "\n" +
    "<div class=\"page page-dashboard\">\n" +
    "    <header>\n" +
    "        <h1>\n" +
    "            Add an Athlete <i class=\"fa fa-users\"></i>\n" +
    "            <span></span>\n" +
    "        </h1>\n" +
    "    </header>\n" +
    "\n" +
    "    <form ng-submit=\"createProfile()\">\n" +
    "        <div class=\"row\">\n" +
    "\n" +
    "            <!-- New profile form -->\n" +
    "            <div class=\"col-sm-12\">\n" +
    "                <div class=\"panel panel-default\">\n" +
    "                    <div class=\"panel-heading\">\n" +
    "                        Fields marked * are required\n" +
    "                    </div>\n" +
    "\n" +
    "                    <div class=\"panel-body\">\n" +
    "\n" +
    "                        <!-- First & last name -->\n" +
    "                        <div class=\"row\">\n" +
    "                            <div class=\"col-sm-4 col-md-3 text-right\">\n" +
    "                                First &amp; Last Name *\n" +
    "                            </div>\n" +
    "                            <div class=\"col-sm-4 col-md-4\">\n" +
    "                                <div class=\"form-group\">\n" +
    "                                    <input\n" +
    "                                        type=\"text\"\n" +
    "                                        ng-model=\"newProfile.firstName\"\n" +
    "                                        placeholder=\"First Name\"\n" +
    "                                        class=\"form-control\"\n" +
    "                                        required>\n" +
    "                                </div>\n" +
    "                            </div>\n" +
    "                            <div class=\"col-sm-4 col-md-5\">\n" +
    "                                <div class=\"form-group\">\n" +
    "                                    <input\n" +
    "                                        type=\"text\"\n" +
    "                                        ng-model=\"newProfile.lastName\"\n" +
    "                                        placeholder=\"Last Name\"\n" +
    "                                        class=\"form-control\"\n" +
    "                                        required>\n" +
    "                                </div>\n" +
    "                            </div>\n" +
    "                        </div>\n" +
    "\n" +
    "                        <!-- Height -->\n" +
    "                        <div class=\"row form-group\">\n" +
    "                            <div class=\"col-sm-4 col-md-3 text-right\">\n" +
    "                                Height (ft / in) *\n" +
    "                            </div>\n" +
    "                            <div class=\"col-sm-4 col-md-2\">\n" +
    "                                <input\n" +
    "                                    type=\"number\"\n" +
    "                                    min=\"1\" max=\"10\"\n" +
    "                                    ng-model=\"newProfile.feet\"\n" +
    "                                    placeholder=\"Feet\"\n" +
    "                                    class=\"form-control\"\n" +
    "                                    required>\n" +
    "                            </div>\n" +
    "                            <div class=\"col-sm-4 col-md-2\">\n" +
    "                                <input\n" +
    "                                    type=\"number\"\n" +
    "                                    min=\"0\" max=\"11\"\n" +
    "                                    ng-model=\"newProfile.inches\"\n" +
    "                                    placeholder=\"Inches\"\n" +
    "                                    class=\"form-control\">\n" +
    "                            </div>\n" +
    "                        </div>\n" +
    "\n" +
    "                        <!-- Weight -->\n" +
    "                        <div class=\"row form-group\">\n" +
    "                            <div class=\"col-sm-4 col-md-3 text-right\">\n" +
    "                                Weight (lbs) *\n" +
    "                            </div>\n" +
    "                            <div class=\"col-sm-8 col-md-2\">\n" +
    "                                <input\n" +
    "                                    class=\"form-control\"\n" +
    "                                    ng-model=\"newProfile.weightInPounds\"\n" +
    "                                    placeholder=\"Weight\"\n" +
    "                                    type=\"number\"\n" +
    "                                    min=\"1\" max=\"1000\" step=\"0.01\"\n" +
    "                                    required>\n" +
    "                            </div>\n" +
    "                        </div>\n" +
    "\n" +
    "                        <!-- Date of birth -->\n" +
    "                        <div class=\"row form-group\">\n" +
    "                            <div class=\"col-sm-4 col-md-3 text-right\">\n" +
    "                                Date of Birth\n" +
    "                            </div>\n" +
    "                            <div class=\"col-sm-8 col-md-4\">\n" +
    "                                <input\n" +
    "                                    class=\"form-control\"\n" +
    "                                    ng-model=\"newProfile.dob\"\n" +
    "                                    placeholder=\"DOB\"\n" +
    "                                    type=\"text\"\n" +
    "                                    disabled>\n" +
    "                            </div>\n" +
    "                        </div>\n" +
    "\n" +
    "                        <!-- Gender -->\n" +
    "                        <div class=\"row form-group\">\n" +
    "                            <div class=\"col-sm-4 col-md-3 text-right\">\n" +
    "                                Gender\n" +
    "                            </div>\n" +
    "                            <div class=\"col-sm-8 col-md-4\">\n" +
    "                                <select ng-model=\"newProfile.gender\" class=\"form-control\">\n" +
    "                                    <option value=\"\" ng-selected=\"newProfile.gender == ''\">(not specified)</option>\n" +
    "                                    <option value=\"female\" ng-selected=\"newProfile.gender == 'female'\">Female</option>\n" +
    "                                    <option value=\"male\" ng-selected=\"newProfile.gender == 'male'\">Male</option>\n" +
    "                                </select>\n" +
    "                            </div>\n" +
    "                        </div>\n" +
    "\n" +
    "                        <!-- Email -->\n" +
    "                        <div class=\"row form-group\">\n" +
    "                            <div class=\"col-sm-4 col-md-3 text-right\">\n" +
    "                                Email\n" +
    "                            </div>\n" +
    "                            <div class=\"col-sm-8 col-md-6\">\n" +
    "                                <input\n" +
    "                                    class=\"form-control\"\n" +
    "                                    ng-model=\"newProfile.email\"\n" +
    "                                    placeholder=\"email@example.com\"\n" +
    "                                    type=\"email\">\n" +
    "                            </div>\n" +
    "                        </div>\n" +
    "\n" +
    "                        <!-- Phone number -->\n" +
    "                        <div class=\"row form-group\">\n" +
    "                            <div class=\"col-sm-4 col-md-3 text-right\">\n" +
    "                                Phone\n" +
    "                            </div>\n" +
    "                            <div class=\"col-sm-8 col-md-4\">\n" +
    "                                <input\n" +
    "                                    class=\"form-control\"\n" +
    "                                    ng-model=\"newProfile.phone\"\n" +
    "                                    placeholder=\"555-555-5555\"\n" +
    "                                    type=\"tel\">\n" +
    "                            </div>\n" +
    "                        </div>\n" +
    "\n" +
    "                        <!-- Medical history -->\n" +
    "                        <div class=\"row form-group\">\n" +
    "                            <div class=\"col-sm-4 col-md-3 text-right\">\n" +
    "                                Medical History\n" +
    "                            </div>\n" +
    "                            <div class=\"col-sm-8 col-md-9\">\n" +
    "                                <textarea\n" +
    "                                    ng-model=\"newProfile.medicalHistory\"\n" +
    "                                    class=\"form-control\"\n" +
    "                                    placeholder=\"...\"></textarea>\n" +
    "                            </div>\n" +
    "                        </div>\n" +
    "\n" +
    "                        <!-- Previous injuries -->\n" +
    "                        <div class=\"row form-group\">\n" +
    "                            <div class=\"col-sm-4 col-md-3 text-right\">\n" +
    "                                Previous Injuries\n" +
    "                            </div>\n" +
    "                            <div class=\"col-sm-8 col-md-9\">\n" +
    "                                <textarea\n" +
    "                                    ng-model=\"newProfile.injuries\"\n" +
    "                                    class=\"form-control\"\n" +
    "                                    placeholder=\"...\"></textarea>\n" +
    "                            </div>\n" +
    "                        </div>\n" +
    "\n" +
    "                        <!-- Notes -->\n" +
    "                        <div class=\"row form-group\">\n" +
    "                            <div class=\"col-sm-4 col-md-3 text-right\">\n" +
    "                                Other Notes\n" +
    "                            </div>\n" +
    "                            <div class=\"col-sm-8 col-md-9\">\n" +
    "                                <textarea\n" +
    "                                    ng-model=\"newProfile.notes\"\n" +
    "                                    class=\"form-control\"\n" +
    "                                    placeholder=\"...\"></textarea>\n" +
    "                            </div>\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "                </div>\n" +
    "            </div>\n" +
    "        </div>\n" +
    "\n" +
    "        <!-- Action buttons -->\n" +
    "        <div class=\"row\">\n" +
    "            <div class=\"col-sm-12 text-center\">\n" +
    "                <button type=\"submit\" class=\"btn btn-default\">Submit</button>\n" +
    "            </div>\n" +
    "        </div>\n" +
    "    </form>\n" +
    "</div>\n" +
    "");
}]);

angular.module("profile/list.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("profile/list.html",
    "<!-- Copyright Heddoko(TM) 2015, all rights reserved. -->\n" +
    "\n" +
    "<div class=\"page profile-page\">\n" +
    "    <header>\n" +
    "        <h1>\n" +
    "            Athletes <i class=\"fa fa-user\"></i>\n" +
    "            <span></span>\n" +
    "        </h1>\n" +
    "    </header>\n" +
    "\n" +
    "    <div ng-if=\"global.data.isFetchingProfiles === false\" class=\"row\">\n" +
    "        <ui-filesystem\n" +
    "            data-id=\"profiles-explorer\"\n" +
    "            data-files=\"profileList\"\n" +
    "            data-default-layout=\"large-tiles\"\n" +
    "            data-hide-location=\"true\"\n" +
    "            data-config=\"uiFilesystemConfig\"\n" +
    "            data-is-loading=\"isLoading\"\n" +
    "            class=\"col-sm-12\">\n" +
    "        </ui-filesystem>\n" +
    "\n" +
    "        <!-- New profile modal form -->\n" +
    "        <ng-include src=\"'profile/partials/new-profile-modal.html'\"></ng-include>\n" +
    "    </div>\n" +
    "\n" +
    "    <div ng-if=\"global.data.isFetchingProfiles === true\" class=\"loading-notice\">\n" +
    "        <h3>Retrieving Athletes</h3>\n" +
    "        <i class=\"fa fa-spinner fa-spin fa-2x\"></i>\n" +
    "    </div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("profile/partials/current-fms-plot.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("profile/partials/current-fms-plot.html",
    "<div class=\"panel panel-default\">\n" +
    "    <div class=\"panel-heading\">\n" +
    "        Test FMS Score Graph\n" +
    "    </div>\n" +
    "\n" +
    "    <div class=\"panel-body\">\n" +
    "        <div\n" +
    "            morris-chart\n" +
    "            data-type=\"area\"\n" +
    "            data-data=\"fmsForms\"\n" +
    "            data-xkey=\"created_at\"\n" +
    "            data-ykeys='[\"Lhurdle\", \"Rhurdle\", \"Llunge\", \"Rlunge\"]'\n" +
    "            data-labels='[\"Left Hurdle\", \"Right Hurdle\", \"Left Lunge\", \"Right Lunge\"]'\n" +
    "            data-line-colors='[\"#383d43\",\"#db5031\",\"#c1bfc0\"]'\n" +
    "            data-line-width=\"3\">\n" +
    "        </div>\n" +
    "    </div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("profile/partials/demo-session.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("profile/partials/demo-session.html",
    "<!-- Copyright Heddoko(TM) 2015, all rights reserved. -->\n" +
    "\n" +
    "<section class=\"demo-trends-section\" style=\"margin: 50px auto\">\n" +
    "    <h2 class=\"title\">\n" +
    "        Training Session Review\n" +
    "        <span></span>\n" +
    "    </h2>\n" +
    "    <br>\n" +
    "    <br>\n" +
    "\n" +
    "    <!-- Demo data -->\n" +
    "    <div ng-show=\"session && metric && isFetchingSessionData === false\" class=\"animate-fade-up\">\n" +
    "\n" +
    "        <div class=\"row\">\n" +
    "            <div class=\"col-md-9 col-md-offset-3\">\n" +
    "                <h3 class=\"text-center\" style=\"margin-bottom: 35px\">\n" +
    "                    {{ metric.title }}\n" +
    "\n" +
    "                    <small ng-show=\"metric.title == 'Shoulder External Rotation'\" style=\"display: block; margin-top: 5px\">\n" +
    "                        100% = 90&deg;\n" +
    "                    </small>\n" +
    "                </h3>\n" +
    "            </div>\n" +
    "\n" +
    "\n" +
    "\n" +
    "            <!-- Left Panel -->\n" +
    "            <div class=\"col-md-3 demo-trends-info-2\">\n" +
    "                <div class=\"row\">\n" +
    "\n" +
    "                    <div class=\"col-sm-4 text-right\" style=\"line-height: 35px\">\n" +
    "                        Session:\n" +
    "                    </div>\n" +
    "                    <div class=\"col-sm-8\">\n" +
    "                        <selectize\n" +
    "                            ng-model=\"selectizeSessionModel\"\n" +
    "                            class=\"form-control text-center\"\n" +
    "                            config=\"selectizeSessionConfig\"\n" +
    "                            options=\"selectizeSessionOptions\">\n" +
    "                        </selectize>\n" +
    "                    </div>\n" +
    "\n" +
    "                    <div class=\"col-sm-4 text-right\" style=\"line-height: 35px\">\n" +
    "                        Conditions:\n" +
    "                    </div>\n" +
    "                    <div class=\"col-sm-8\" style=\"line-height: 35px\">\n" +
    "                        <b>Sunny, Light Drizzle</b>\n" +
    "                    </div>\n" +
    "\n" +
    "                    <div class=\"col-sm-4 text-right\" style=\"line-height: 35px\">\n" +
    "                        Wind:\n" +
    "                    </div>\n" +
    "                    <div class=\"col-sm-8\" style=\"line-height: 35px\">\n" +
    "                        <b>8 km/h</b>\n" +
    "                    </div>\n" +
    "\n" +
    "                    <div class=\"col-sm-4 text-right\" style=\"line-height: 35px\">\n" +
    "                        Metric:\n" +
    "                    </div>\n" +
    "                    <div class=\"col-sm-8\">\n" +
    "                        <selectize\n" +
    "                            ng-model=\"selectizeMetricModel\"\n" +
    "                            class=\"form-control text-center\"\n" +
    "                            config=\"selectizeMetricConfig\"\n" +
    "                            options=\"selectizeMetricOptions\">\n" +
    "                        </selectize>\n" +
    "                    </div>\n" +
    "                </div>\n" +
    "\n" +
    "                <!-- Extra Info -->\n" +
    "                <div ng-hide=\"isFetchingSelectedMetricData || isFetchingSessionData\">\n" +
    "\n" +
    "                    <div\n" +
    "                        ng-show=\"metric.title == 'Peak Elbow Angular Velocity'\"\n" +
    "                        class=\"row text-center\"\n" +
    "                        style=\"color: #ddd; margin-top: 10px\">\n" +
    "\n" +
    "                        <div class=\"col-xs-12\">\n" +
    "                            <div style=\"margin: 25px 0 0\">\n" +
    "                                <img src=\"/images/demo/qb throw.png\" style=\"height: 65px; margin-right: 15px; vertical-align: top\">\n" +
    "                                <span style=\"font-size: 3.5em; vertical-align: bottom;\">\n" +
    "                                    {{ flotElbowDrops[session.id - 1].num }}\n" +
    "                                </span>\n" +
    "                            </div>\n" +
    "\n" +
    "                            <span style=\"font-size: 1.3em\">\n" +
    "                                Throw Count <br>\n" +
    "                                Before Degradation\n" +
    "                            </span>\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "\n" +
    "                    <div ng-show=\"metric.title == 'Peak Forearm Snap Velocity'\" class=\"row text-center\">\n" +
    "                        <div class=\"col-xs-12\">\n" +
    "                            The snapping of the wrist from supinated to pronated is what gives the ball its\n" +
    "                            spin. The faster this happens, the tighter the spin will be, and the higher the\n" +
    "                            chances the ball will travel faster and further.\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "\n" +
    "                    <!-- <div ng-show=\"false && metric.title == 'Stride Length'\" class=\"row text-center\">\n" +
    "                        <div class=\"col-xs-12\">\n" +
    "                            The proper stride length is approximately the athlete’s tibial\n" +
    "                            tuberosity X 1.5, or about 26-30 inches in most quarterbacks.\n" +
    "                        </div>\n" +
    "                    </div> -->\n" +
    "\n" +
    "                    <div ng-show=\"metric.title == 'Shoulder External Rotation'\" class=\"col-xs-12\">\n" +
    "\n" +
    "                        <br>\n" +
    "                        <br>\n" +
    "                        <canvas\n" +
    "                            data-theme-gauge-chart\n" +
    "                            data-gauge-data=\"demoGaugeOne.data\"\n" +
    "                            data-live-gauge-value=\"demoGaugeOneValue\"\n" +
    "                            data-gauge-options=\"demoGaugeOne.options\"\n" +
    "                            style=\"width: 100%;\">\n" +
    "                        </canvas>\n" +
    "\n" +
    "                        <div style=\"position: absolute; left: 45%; bottom: 15px; font-size: 1.2em\">\n" +
    "                            {{ demoGaugeOneValuePercent }}\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "\n" +
    "                    <div ng-show=\"metric.title == 'Stride Length'\" class=\"col-xs-12\">\n" +
    "\n" +
    "                        <br>\n" +
    "                        <br>\n" +
    "                        <canvas\n" +
    "                            data-theme-gauge-chart\n" +
    "                            data-gauge-data=\"demoGaugeTwo.data\"\n" +
    "                            data-live-gauge-value=\"demoGaugeTwoValue\"\n" +
    "                            data-gauge-options=\"demoGaugeTwo.options\"\n" +
    "                            style=\"width: 100%;\">\n" +
    "                        </canvas>\n" +
    "\n" +
    "                        <div style=\"position: absolute; left: 46%; bottom: 15px; font-size: 1.2em\">\n" +
    "                            {{ demoGaugeTwoValuePercent }}\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "                </div>\n" +
    "            </div>\n" +
    "\n" +
    "\n" +
    "\n" +
    "            <!-- Metric plots -->\n" +
    "            <div ng-show=\"isFetchingSelectedMetricData === false\">\n" +
    "\n" +
    "\n" +
    "                <!-- Peak Elbow Angular Velocity -->\n" +
    "                <div ng-show=\"metric.title == 'Peak Elbow Angular Velocity'\" class=\"col-md-9\">\n" +
    "                    <div ng-show=\"session.id === 5\">\n" +
    "                        <div\n" +
    "                            data-theme-flot-chart\n" +
    "                            data-data=\"flotElbowData[4]\"\n" +
    "                            data-options=\"flotElbowOptions[4]\"\n" +
    "                            data-plot-hover=\"flotElbowHover\"\n" +
    "                            data-plot-labels=\"flotElbowLabels[4]\"\n" +
    "                            data-threshold-label=\"'Pause Training'\"\n" +
    "                            style=\"width: 100%; height: 400px;\">\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "                    <div ng-show=\"session.id === 4\">\n" +
    "                        <div\n" +
    "                            data-theme-flot-chart\n" +
    "                            data-data=\"flotElbowData[3]\"\n" +
    "                            data-options=\"flotElbowOptions[3]\"\n" +
    "                            data-plot-hover=\"flotElbowHover\"\n" +
    "                            data-plot-labels=\"flotElbowLabels[3]\"\n" +
    "                            data-threshold-label=\"'Pause Training'\"\n" +
    "                            style=\"width: 100%; height: 400px;\">\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "                    <div ng-show=\"session.id === 3\">\n" +
    "                        <div\n" +
    "                            data-theme-flot-chart\n" +
    "                            data-data=\"flotElbowData[2]\"\n" +
    "                            data-options=\"flotElbowOptions[2]\"\n" +
    "                            data-plot-hover=\"flotElbowHover\"\n" +
    "                            data-plot-labels=\"flotElbowLabels[2]\"\n" +
    "                            data-threshold-label=\"'Pause Training'\"\n" +
    "                            style=\"width: 100%; height: 400px;\">\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "                    <div ng-show=\"session.id === 2\">\n" +
    "                        <div\n" +
    "                            data-theme-flot-chart\n" +
    "                            data-data=\"flotElbowData[1]\"\n" +
    "                            data-options=\"flotElbowOptions[1]\"\n" +
    "                            data-plot-hover=\"flotElbowHover\"\n" +
    "                            data-plot-labels=\"flotElbowLabels[1]\"\n" +
    "                            data-threshold-label=\"'Pause Training'\"\n" +
    "                            style=\"width: 100%; height: 400px;\">\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "                    <div ng-show=\"session.id === 1\">\n" +
    "                        <div\n" +
    "                            data-theme-flot-chart\n" +
    "                            data-data=\"flotElbowData[0]\"\n" +
    "                            data-options=\"flotElbowOptions[0]\"\n" +
    "                            data-plot-hover=\"flotElbowHover\"\n" +
    "                            data-plot-labels=\"flotElbowLabels[0]\"\n" +
    "                            data-threshold-label=\"'Pause Training'\"\n" +
    "                            style=\"width: 100%; height: 400px;\">\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "\n" +
    "\n" +
    "                    <div class=\"row text-center\">\n" +
    "                        <div class=\"col-md-4\" style=\"margin-top: 30px\">\n" +
    "                            <div class=\"col-md-10 col-md-offset-2\">\n" +
    "                                <h4 style=\"display: inline-block\">\n" +
    "                                    Fastest Throw\n" +
    "                                </h4>\n" +
    "                                <canvas\n" +
    "                                    data-gauge-chart\n" +
    "                                    data-gauge-data=\"elbowMaxGauge.data\"\n" +
    "                                    data-gauge-options=\"elbowMaxGauge.options\"\n" +
    "                                    style=\"width: 100%; vertical-align: bottom\">\n" +
    "                                </canvas>\n" +
    "\n" +
    "                                <div style=\"position: absolute; left: 39%; bottom: 15px;\">\n" +
    "                                    {{ elbowMaxVel | number:0 }} &deg;/s\n" +
    "                                </div>\n" +
    "                            </div>\n" +
    "                        </div>\n" +
    "\n" +
    "                        <div class=\"col-md-4\" style=\"margin-top: 30px\">\n" +
    "                            <div class=\"col-md-10 col-md-offset-1\">\n" +
    "                                <h4 style=\"display: inline-block\">\n" +
    "                                    Average\n" +
    "                                </h4>\n" +
    "                                <canvas\n" +
    "                                    data-gauge-chart\n" +
    "                                    data-gauge-data=\"elbowAvgGauge.data\"\n" +
    "                                    data-gauge-options=\"elbowAvgGauge.options\"\n" +
    "                                    style=\"width: 100%; vertical-align: bottom\">\n" +
    "                                </canvas>\n" +
    "\n" +
    "                                <div style=\"position: absolute; left: 39%; bottom: 15px;\">\n" +
    "                                    {{ elbowAvgVel | number:0 }} &deg;/s\n" +
    "                                </div>\n" +
    "                            </div>\n" +
    "                        </div>\n" +
    "\n" +
    "                        <div class=\"col-md-4\" style=\"margin-top: 30px\">\n" +
    "                            <div class=\"col-md-10\">\n" +
    "                                <h4 style=\"display: inline-block\">\n" +
    "                                    Slowest Throw\n" +
    "                                </h4>\n" +
    "                                <canvas\n" +
    "                                    data-gauge-chart\n" +
    "                                    data-gauge-data=\"elbowMinGauge.data\"\n" +
    "                                    data-gauge-options=\"elbowMinGauge.options\"\n" +
    "                                    style=\"width: 100%; vertical-align: bottom\">\n" +
    "                                </canvas>\n" +
    "\n" +
    "                                <div style=\"position: absolute; left: 39%; bottom: 15px;\">\n" +
    "                                    {{ elbowMinVel | number:0 }} &deg;/s\n" +
    "                                </div>\n" +
    "                            </div>\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "                </div>\n" +
    "\n" +
    "                <!-- Shoulder External Rotation -->\n" +
    "                <div ng-show=\"metric.title == 'Shoulder External Rotation'\" class=\"col-md-9\">\n" +
    "                    <div ng-show=\"session.id === 5\">\n" +
    "                        <div\n" +
    "                            data-theme-flot-chart\n" +
    "                            data-data=\"flotShoulderData[4]\"\n" +
    "                            data-options=\"flotShoulderOptions[4]\"\n" +
    "                            data-plot-hover=\"flotShoulderRotHover\"\n" +
    "                            style=\"width: 100%; height: 400px;\">\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "                    <div ng-show=\"session.id === 4\">\n" +
    "                        <div\n" +
    "                            data-theme-flot-chart\n" +
    "                            data-data=\"flotShoulderData[3]\"\n" +
    "                            data-options=\"flotShoulderOptions[3]\"\n" +
    "                            data-plot-hover=\"flotShoulderRotHover\"\n" +
    "                            style=\"width: 100%; height: 400px;\">\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "                    <div ng-show=\"session.id === 3\">\n" +
    "                        <div\n" +
    "                            data-theme-flot-chart\n" +
    "                            data-data=\"flotShoulderData[2]\"\n" +
    "                            data-options=\"flotShoulderOptions[2]\"\n" +
    "                            data-plot-hover=\"flotShoulderRotHover\"\n" +
    "                            style=\"width: 100%; height: 400px;\">\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "                    <div ng-show=\"session.id === 2\">\n" +
    "                        <div\n" +
    "                            data-theme-flot-chart\n" +
    "                            data-data=\"flotShoulderData[1]\"\n" +
    "                            data-options=\"flotShoulderOptions[1]\"\n" +
    "                            data-plot-hover=\"flotShoulderRotHover\"\n" +
    "                            style=\"width: 100%; height: 400px;\">\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "                    <div ng-show=\"session.id === 1\">\n" +
    "                        <div\n" +
    "                            data-theme-flot-chart\n" +
    "                            data-data=\"flotShoulderData[0]\"\n" +
    "                            data-options=\"flotShoulderOptions[0]\"\n" +
    "                            data-plot-hover=\"flotShoulderRotHover\"\n" +
    "                            style=\"width: 100%; height: 400px;\">\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "                </div>\n" +
    "\n" +
    "                <!-- Stride Length -->\n" +
    "                <div ng-show=\"metric.title == 'Stride Length'\" class=\"col-md-9\">\n" +
    "                    <div ng-show=\"session.id === 5\">\n" +
    "                        <div\n" +
    "                            data-theme-flot-chart\n" +
    "                            data-data=\"flotStrideData[4]\"\n" +
    "                            data-options=\"flotStrideOptions[4]\"\n" +
    "                            data-plot-hover=\"flotStrideHover\"\n" +
    "                            style=\"width: 100%; height: 400px;\">\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "                    <div ng-show=\"session.id === 4\">\n" +
    "                        <div\n" +
    "                            data-theme-flot-chart\n" +
    "                            data-data=\"flotStrideData[3]\"\n" +
    "                            data-options=\"flotStrideOptions[3]\"\n" +
    "                            data-plot-hover=\"flotStrideHover\"\n" +
    "                            style=\"width: 100%; height: 400px;\">\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "                    <div ng-show=\"session.id === 3\">\n" +
    "                        <div\n" +
    "                            data-theme-flot-chart\n" +
    "                            data-data=\"flotStrideData[2]\"\n" +
    "                            data-options=\"flotStrideOptions[2]\"\n" +
    "                            data-plot-hover=\"flotStrideHover\"\n" +
    "                            style=\"width: 100%; height: 400px;\">\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "                    <div ng-show=\"session.id === 2\">\n" +
    "                        <div\n" +
    "                            data-theme-flot-chart\n" +
    "                            data-data=\"flotStrideData[1]\"\n" +
    "                            data-options=\"flotStrideOptions[1]\"\n" +
    "                            data-plot-hover=\"flotStrideHover\"\n" +
    "                            style=\"width: 100%; height: 400px;\">\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "                    <div ng-show=\"session.id === 1\">\n" +
    "                        <div\n" +
    "                            data-theme-flot-chart\n" +
    "                            data-data=\"flotStrideData[0]\"\n" +
    "                            data-options=\"flotStrideOptions[0]\"\n" +
    "                            data-plot-hover=\"flotStrideHover\"\n" +
    "                            style=\"width: 100%; height: 400px;\">\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "                </div>\n" +
    "\n" +
    "            </div>\n" +
    "\n" +
    "            <!-- Loading metric plots -->\n" +
    "            <div ng-show=\"isFetchingSelectedMetricData === true\" class=\"col-md-9 text-center text-muted\">\n" +
    "                <i class=\"fa fa-spinner fa-spin fa-3x\" style=\"margin: 200px auto\"></i>\n" +
    "            </div>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "\n" +
    "    <!-- Loading data -->\n" +
    "    <div ng-show=\"isFetchingSessionData === true\" class=\"row text-center text-muted\">\n" +
    "        <i class=\"fa fa-spinner fa-spin fa-3x\" style=\"display: block; margin: 200px 0 300px\"></i>\n" +
    "    </div>\n" +
    "\n" +
    "    <!-- No data selected -->\n" +
    "    <div ng-show=\"(!session || !metric) && isFetchingSessionData === false\" class=\"row\" style=\"margin: 0 auto 250px;\">\n" +
    "\n" +
    "        <div class=\"col-md-12\">\n" +
    "            Select a training session and metric to get started.\n" +
    "        </div>\n" +
    "        <br>\n" +
    "        <br>\n" +
    "\n" +
    "        <div class=\"col-md-4 col-md-offset-2\">\n" +
    "            <selectize\n" +
    "                ng-model=\"selectizeSessionModel\"\n" +
    "                class=\"form-control text-center\"\n" +
    "                config=\"selectizeSessionConfig\"\n" +
    "                options=\"selectizeSessionOptions\"\n" +
    "                placeholder=\"Select a training session.\">\n" +
    "            </selectize>\n" +
    "        </div>\n" +
    "\n" +
    "        <div class=\"col-md-4\">\n" +
    "            <selectize\n" +
    "                ng-model=\"selectizeMetricModel\"\n" +
    "                class=\"form-control text-center\"\n" +
    "                config=\"selectizeMetricConfig\"\n" +
    "                options=\"selectizeMetricOptions\"\n" +
    "                placeholder=\"Select a metric.\">\n" +
    "            </selectize>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "\n" +
    "</section>\n" +
    "");
}]);

angular.module("profile/partials/final-fms-plot.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("profile/partials/final-fms-plot.html",
    "<div class=\"panel panel-default\">\n" +
    "    <div class=\"panel-heading\">\n" +
    "        Total FMS Score Graph\n" +
    "    </div>\n" +
    "\n" +
    "    <div class=\"panel-body\">\n" +
    "        <div class=\"panel-body ng-scope\" data-ng-controller=\"morrisChartCtrl\">\n" +
    "\n" +
    "            <div\n" +
    "                morris-chart\n" +
    "                data-type=\"line\"\n" +
    "                data-data=\"fmsForms\"\n" +
    "                data-xkey=\"created_at\"\n" +
    "                data-ykeys='[\"totalscore\"]'\n" +
    "                data-labels='[\"total score\"]'\n" +
    "                data-line-colors='[\"#383d43\"]'\n" +
    "                data-line-width=\"3\">\n" +
    "            </div>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("profile/partials/movement-indicators.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("profile/partials/movement-indicators.html",
    "<!-- Copyright Heddoko(TM) 2015, all rights reserved. -->\n" +
    "\n" +
    "<section class=\"demo-trends-section\" style=\"margin: 50px auto\">\n" +
    "    <header>\n" +
    "        <h2 class=\"title\">\n" +
    "            Performance Indicators\n" +
    "            <span></span>\n" +
    "        </h2>\n" +
    "\n" +
    "        <div class=\"h1-append\">\n" +
    "            Single Movements\n" +
    "        </div>\n" +
    "    </header>\n" +
    "\n" +
    "    <h1 class=\"text-center text-muted\" style=\"margin: 200px auto\">In Development</h1>\n" +
    "</section>\n" +
    "");
}]);

angular.module("profile/partials/new-profile-modal.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("profile/partials/new-profile-modal.html",
    "<!-- Copyright Heddoko(TM) 2016, all rights reserved. -->\n" +
    "\n" +
    "<div class=\"modal fade\" id=\"createProfileForm\">\n" +
    "    <div class=\"modal-dialog\">\n" +
    "        <div class=\"modal-content\">\n" +
    "            <div class=\"modal-header\">\n" +
    "                Add an Athlete\n" +
    "            </div>\n" +
    "\n" +
    "            <div class=\"modal-body\">\n" +
    "\n" +
    "                <!-- First & last name -->\n" +
    "                <div class=\"row\">\n" +
    "                    <div class=\"col-sm-4 col-md-3 text-right\">\n" +
    "                        First &amp; Last Name *\n" +
    "                    </div>\n" +
    "                    <div class=\"col-sm-4 col-md-4\">\n" +
    "                        <div class=\"form-group\">\n" +
    "                            <input\n" +
    "                                type=\"text\"\n" +
    "                                ng-model=\"newProfile.firstName\"\n" +
    "                                placeholder=\"First Name\"\n" +
    "                                class=\"form-control\"\n" +
    "                                required>\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "                    <div class=\"col-sm-4 col-md-5\">\n" +
    "                        <div class=\"form-group\">\n" +
    "                            <input\n" +
    "                                type=\"text\"\n" +
    "                                ng-model=\"newProfile.lastName\"\n" +
    "                                placeholder=\"Last Name\"\n" +
    "                                class=\"form-control\"\n" +
    "                                required>\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "                </div>\n" +
    "\n" +
    "                <!-- Height -->\n" +
    "                <div class=\"row form-group\">\n" +
    "                    <div class=\"col-sm-4 col-md-3 text-right\">\n" +
    "                        Height (ft / in) *\n" +
    "                    </div>\n" +
    "                    <div class=\"col-sm-4 col-md-2\">\n" +
    "                        <input\n" +
    "                            type=\"number\"\n" +
    "                            min=\"1\" max=\"10\"\n" +
    "                            ng-model=\"newProfile.feet\"\n" +
    "                            placeholder=\"Feet\"\n" +
    "                            class=\"form-control\"\n" +
    "                            required>\n" +
    "                    </div>\n" +
    "                    <div class=\"col-sm-4 col-md-2\">\n" +
    "                        <input\n" +
    "                            type=\"number\"\n" +
    "                            min=\"0\" max=\"11\"\n" +
    "                            ng-model=\"newProfile.inches\"\n" +
    "                            placeholder=\"Inches\"\n" +
    "                            class=\"form-control\">\n" +
    "                    </div>\n" +
    "                </div>\n" +
    "\n" +
    "                <!-- Weight -->\n" +
    "                <div class=\"row form-group\">\n" +
    "                    <div class=\"col-sm-4 col-md-3 text-right\">\n" +
    "                        Weight (lbs) *\n" +
    "                    </div>\n" +
    "                    <div class=\"col-sm-8 col-md-2\">\n" +
    "                        <input\n" +
    "                            class=\"form-control\"\n" +
    "                            ng-model=\"newProfile.weightInPounds\"\n" +
    "                            placeholder=\"Weight\"\n" +
    "                            type=\"number\"\n" +
    "                            min=\"1\" max=\"1000\" step=\"0.01\"\n" +
    "                            required>\n" +
    "                    </div>\n" +
    "                </div>\n" +
    "\n" +
    "                <!-- Date of birth -->\n" +
    "                <div class=\"row form-group\">\n" +
    "                    <div class=\"col-sm-4 col-md-3 text-right\">\n" +
    "                        Date of Birth\n" +
    "                    </div>\n" +
    "                    <div class=\"col-sm-8 col-md-4\">\n" +
    "                        <input\n" +
    "                            class=\"form-control\"\n" +
    "                            ng-model=\"newProfile.dob\"\n" +
    "                            placeholder=\"DOB\"\n" +
    "                            type=\"text\"\n" +
    "                            disabled>\n" +
    "                    </div>\n" +
    "                </div>\n" +
    "\n" +
    "                <!-- Gender -->\n" +
    "                <div class=\"row form-group\">\n" +
    "                    <div class=\"col-sm-4 col-md-3 text-right\">\n" +
    "                        Gender\n" +
    "                    </div>\n" +
    "                    <div class=\"col-sm-8 col-md-4\">\n" +
    "                        <select ng-model=\"newProfile.gender\" class=\"form-control\">\n" +
    "                            <option value=\"\" ng-selected=\"newProfile.gender == ''\">(not specified)</option>\n" +
    "                            <option value=\"female\" ng-selected=\"newProfile.gender == 'female'\">Female</option>\n" +
    "                            <option value=\"male\" ng-selected=\"newProfile.gender == 'male'\">Male</option>\n" +
    "                        </select>\n" +
    "                    </div>\n" +
    "                </div>\n" +
    "\n" +
    "                <!-- Email -->\n" +
    "                <div class=\"row form-group\">\n" +
    "                    <div class=\"col-sm-4 col-md-3 text-right\">\n" +
    "                        Email\n" +
    "                    </div>\n" +
    "                    <div class=\"col-sm-8 col-md-6\">\n" +
    "                        <input\n" +
    "                            class=\"form-control\"\n" +
    "                            ng-model=\"newProfile.email\"\n" +
    "                            placeholder=\"email@example.com\"\n" +
    "                            type=\"email\">\n" +
    "                    </div>\n" +
    "                </div>\n" +
    "\n" +
    "                <!-- Phone number -->\n" +
    "                <div class=\"row form-group\">\n" +
    "                    <div class=\"col-sm-4 col-md-3 text-right\">\n" +
    "                        Phone\n" +
    "                    </div>\n" +
    "                    <div class=\"col-sm-8 col-md-4\">\n" +
    "                        <input\n" +
    "                            class=\"form-control\"\n" +
    "                            ng-model=\"newProfile.phone\"\n" +
    "                            placeholder=\"555-555-5555\"\n" +
    "                            type=\"tel\">\n" +
    "                    </div>\n" +
    "                </div>\n" +
    "\n" +
    "                <!-- Medical history -->\n" +
    "                <div class=\"row form-group\">\n" +
    "                    <div class=\"col-sm-4 col-md-3 text-right\">\n" +
    "                        Medical History\n" +
    "                    </div>\n" +
    "                    <div class=\"col-sm-8 col-md-9\">\n" +
    "                        <textarea\n" +
    "                            ng-model=\"newProfile.medicalHistory\"\n" +
    "                            class=\"form-control\"\n" +
    "                            placeholder=\"...\"></textarea>\n" +
    "                    </div>\n" +
    "                </div>\n" +
    "\n" +
    "                <!-- Previous injuries -->\n" +
    "                <div class=\"row form-group\">\n" +
    "                    <div class=\"col-sm-4 col-md-3 text-right\">\n" +
    "                        Previous Injuries\n" +
    "                    </div>\n" +
    "                    <div class=\"col-sm-8 col-md-9\">\n" +
    "                        <textarea\n" +
    "                            ng-model=\"newProfile.injuries\"\n" +
    "                            class=\"form-control\"\n" +
    "                            placeholder=\"...\"></textarea>\n" +
    "                    </div>\n" +
    "                </div>\n" +
    "\n" +
    "                <!-- Notes -->\n" +
    "                <div class=\"row form-group\">\n" +
    "                    <div class=\"col-sm-4 col-md-3 text-right\">\n" +
    "                        Other Notes\n" +
    "                    </div>\n" +
    "                    <div class=\"col-sm-8 col-md-9\">\n" +
    "                        <textarea\n" +
    "                            ng-model=\"newProfile.notes\"\n" +
    "                            class=\"form-control\"\n" +
    "                            placeholder=\"...\"></textarea>\n" +
    "                    </div>\n" +
    "                </div>\n" +
    "            </div>\n" +
    "\n" +
    "            <div class=\"modal-footer text-center\">\n" +
    "                <button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\">\n" +
    "                    Cancel\n" +
    "                </button>\n" +
    "                <button ng-click=\"createProfile()\" type=\"button\" class=\"btn btn-primary\" data-dismiss=\"modal\">\n" +
    "                    Create\n" +
    "                </button>\n" +
    "            </div>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("profile/partials/screening-indicators.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("profile/partials/screening-indicators.html",
    "<!-- Copyright Heddoko(TM) 2015, all rights reserved. -->\n" +
    "\n" +
    "<section class=\"demo-trends-section\" style=\"margin: 50px auto\">\n" +
    "    <header>\n" +
    "        <h2 class=\"title\">\n" +
    "            Performance Indicators\n" +
    "            <span></span>\n" +
    "        </h2>\n" +
    "\n" +
    "        <div class=\"h1-append\">\n" +
    "            Movement Tests\n" +
    "        </div>\n" +
    "    </header>\n" +
    "\n" +
    "    <h1 class=\"text-center text-muted\" style=\"margin: 200px auto\">In Development</h1>\n" +
    "</section>\n" +
    "");
}]);

angular.module("profile/partials/upload-photo.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("profile/partials/upload-photo.html",
    "<div class=\"panel panel-default\">\n" +
    "    <div class=\"panel-heading\">\n" +
    "        Avatar\n" +
    "    </div>\n" +
    "\n" +
    "    <div class=\"panel-body\">\n" +
    "\n" +
    "        <!-- Avatar preview -->\n" +
    "        <div class=\"row\">\n" +
    "            <div class=\"col-sm-12\">\n" +
    "                <img\n" +
    "                    ng-show=\"avatar || profile.photo_src\"\n" +
    "                    ngf-src=\"avatar || profile.photo_src\"\n" +
    "                    style=\"width: 100%\">\n" +
    "            </div>\n" +
    "        </div>\n" +
    "        <br>\n" +
    "\n" +
    "        <!-- Action buttons -->\n" +
    "        <div class=\"row\">\n" +
    "            <div class=\"col-sm-12 text-center\">\n" +
    "                <span ng-show=\"profile.id < 1\">\n" +
    "                    Please create the profile <br>\n" +
    "                    before uploading a photo. <br><br>\n" +
    "                </span>\n" +
    "\n" +
    "                <span ng-show=\"profile.id >= 1\">\n" +
    "                    Temporarily disabled. <br><br>\n" +
    "                </span>\n" +
    "\n" +
    "                <button\n" +
    "                    ngf-select=\"uploadPhoto($file)\"\n" +
    "                    ngf-drop\n" +
    "                    ng-model=\"avatar\"\n" +
    "                    accept=\"image/*\"\n" +
    "                    ngf-pattern=\"'image/*'\"\n" +
    "                    ngf-capture=\"'camera'\"\n" +
    "                    ngf-min-height=\"100\"\n" +
    "                    ngf-max-size=\"2MB\"\n" +
    "                    ng-class=\"{'btn btn-default': true, 'disabled': profile.id < 1 || true}\">\n" +
    "\n" +
    "                    Select or drag &amp; drop a photo\n" +
    "                </button>\n" +
    "            </div>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("profile/view.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("profile/view.html",
    "<!-- Copyright Heddoko(TM) 2015, all rights reserved. -->\n" +
    "\n" +
    "<div ng-if=\"profile.id > 0\" class=\"page profile-page\">\n" +
    "    <header>\n" +
    "        <h1>\n" +
    "            {{ profile.firstName }} {{ profile.lastName }} <i class=\"fa fa-user\"></i>\n" +
    "            <span></span>\n" +
    "        </h1>\n" +
    "\n" +
    "        <!-- Append main group to profile name -->\n" +
    "        <div ng-if=\"profile.groups.length > 0\" class=\"h1-append\">\n" +
    "            Team:\n" +
    "\n" +
    "            <a href=\"#/groups/{{ profile.groups[0].id }}\">\n" +
    "                {{ profile.groups[0].name }}\n" +
    "            </a>\n" +
    "        </div>\n" +
    "    </header>\n" +
    "\n" +
    "    <section ng-if=\"profile.id > 0\">\n" +
    "    	<div class=\"row\">\n" +
    "\n" +
    "    		<div class=\"col-xs-12 col-md-5 col-lg-3\">\n" +
    "\n" +
    "                <!-- Avatar -->\n" +
    "                <div class=\"text-center\">\n" +
    "                    <ui-avatar\n" +
    "                        data-upload-endpoint=\"uploadAvatarEndpoint\"\n" +
    "                        data-success-callback=\"uploadAvatarCallback\"\n" +
    "                        data-src=\"profile.avatarSrc\">\n" +
    "                    </ui-avatar>\n" +
    "                </div>\n" +
    "\n" +
    "                <!-- Profile shortcuts -->\n" +
    "                <div class=\"profile-shortcuts\">\n" +
    "\n" +
    "                    <!-- Placeholder for football position (NFL TechCrunch demo) -->\n" +
    "                    <span ng-if=\"false\" style=\"line-height: 60px; font-size: 1.2em;\">\n" +
    "                        Position: Quarterback\n" +
    "                    </span>\n" +
    "\n" +
    "                    <div class=\"pull-right\">\n" +
    "\n" +
    "                        <!-- New screening button -->\n" +
    "                        <a\n" +
    "                            ng-if=\"profile.id > 0\"\n" +
    "                            ng-click=\"startNewScreening()\"\n" +
    "                            href=\"javascript:;\">\n" +
    "\n" +
    "                            <button class=\"btn btn-default btn-circle btn-lg\">\n" +
    "                                <i class=\"fa fa-pencil-square-o\"></i>\n" +
    "                            </button>\n" +
    "                        </a>\n" +
    "\n" +
    "                        <!-- Delete group button -->\n" +
    "                        <a\n" +
    "                            ng-if=\"profile.id > 0\"\n" +
    "                            data-toggle=\"modal\"\n" +
    "                            data-target=\"#deleteProfileConfirmation\"\n" +
    "                            href=\"javascript:;\">\n" +
    "\n" +
    "                            <button class=\"btn btn-danger btn-circle btn-lg\">\n" +
    "                                <i class=\"fa fa-trash\"></i>\n" +
    "                            </button>\n" +
    "                        </a>\n" +
    "\n" +
    "                        <!-- Delete confirmation -->\n" +
    "                        <div class=\"modal fade\" id=\"deleteProfileConfirmation\">\n" +
    "                            <div class=\"modal-dialog\">\n" +
    "                                <div class=\"modal-content\">\n" +
    "                                    <div class=\"modal-body\">\n" +
    "                                        <p>\n" +
    "                                            Are you sure you want to delete <b>{{ profile.firstName }}\n" +
    "                                            {{ profile.lastName }}</b>'s profile?\n" +
    "                                        </p>\n" +
    "                                    </div>\n" +
    "                                    <div class=\"modal-footer text-center\">\n" +
    "                                        <button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\">\n" +
    "                                            Cancel\n" +
    "                                        </button>\n" +
    "                                        <button ng-click=\"deleteProfile()\" type=\"button\" class=\"btn btn-primary\" data-dismiss=\"modal\">\n" +
    "                                            Yes, delete {{ profile.firstName }}\n" +
    "                                        </button>\n" +
    "                                    </div>\n" +
    "                                </div>\n" +
    "                            </div>\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "                </div>\n" +
    "    		</div>\n" +
    "\n" +
    "            <!-- Profile details -->\n" +
    "            <div class=\"col-xs-12 col-md-7 col-lg-5\">\n" +
    "                <ui-editable-fields\n" +
    "                    data-id=\"profileDetails\"\n" +
    "                    data-model=\"profile\"\n" +
    "                    data-save=\"saveProfileDetails\"\n" +
    "                    data-save-callback=\"saveProfileDetailsCallback\"\n" +
    "                    data-heading=\"Details\">\n" +
    "\n" +
    "                    <div class=\"row\">\n" +
    "\n" +
    "                        <!-- First name -->\n" +
    "                        <div class=\"col-xs-12 col-sm-6\">\n" +
    "                            <ui-editable-field\n" +
    "                                data-label=\"First Name\"\n" +
    "                                data-key=\"firstName\"\n" +
    "                                data-required=\"true\">\n" +
    "                            </ui-editable-field>\n" +
    "                        </div>\n" +
    "\n" +
    "                        <!-- Last name -->\n" +
    "                        <div class=\"col-xs-12 col-sm-6\">\n" +
    "                            <ui-editable-field\n" +
    "                                data-label=\"Last Name\"\n" +
    "                                data-key=\"lastName\">\n" +
    "                            </ui-editable-field>\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "\n" +
    "                    <div class=\"row\">\n" +
    "\n" +
    "                        <!-- Height -->\n" +
    "                        <div class=\"col-xs-12 col-sm-6\">\n" +
    "                            <ui-editable-field\n" +
    "                                data-label=\"Height\"\n" +
    "                                data-key=\"height\"\n" +
    "                                data-type=\"length\">\n" +
    "                            </ui-editable-field>\n" +
    "                        </div>\n" +
    "\n" +
    "                        <!-- Weight -->\n" +
    "                        <div class=\"col-xs-12 col-sm-6\">\n" +
    "                            <ui-editable-field\n" +
    "                                data-label=\"Weight\"\n" +
    "                                data-key=\"mass\"\n" +
    "                                data-type=\"mass\">\n" +
    "                            </ui-editable-field>\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "\n" +
    "                    <!-- Placeholder for NFL Techrunch demo -->\n" +
    "                    <div ng-if=\"false\" class=\"row\">\n" +
    "                        <div class=\"col-xs-12 col-sm-6\">\n" +
    "\n" +
    "                            <!-- Hand size -->\n" +
    "                            <div class=\"row ui-editable-field ui-editable-field-vertical\">\n" +
    "                                <div class=\"col-xs-12\">\n" +
    "                                    <div class=\"ui-editable-field-value\">\n" +
    "                                        <a href=\"javascript:;\" class=\"edit-text\">\n" +
    "                                            9.6\"\n" +
    "                                        </a>\n" +
    "                                        <a href=\"javascript:;\" class=\"edit-icon fa fa-pencil\"></a>\n" +
    "                                    </div>\n" +
    "                                </div>\n" +
    "                                <div class=\"col-xs-12 ui-editable-field-label\">\n" +
    "                                    Hand Size\n" +
    "                                </div>\n" +
    "                            </div>\n" +
    "                        </div>\n" +
    "\n" +
    "                        <div class=\"col-xs-12 col-sm-6\">\n" +
    "\n" +
    "                            <!-- Arm length -->\n" +
    "                            <div class=\"row ui-editable-field ui-editable-field-vertical\">\n" +
    "                                <div class=\"col-xs-12\">\n" +
    "                                    <div class=\"ui-editable-field-value\">\n" +
    "                                        <a href=\"javascript:;\" class=\"edit-text\">\n" +
    "                                            33\"\n" +
    "                                        </a>\n" +
    "                                        <a href=\"javascript:;\" class=\"edit-icon fa fa-pencil\"></a>\n" +
    "                                    </div>\n" +
    "                                </div>\n" +
    "                                <div class=\"col-xs-12 ui-editable-field-label\">\n" +
    "                                    Arm Length\n" +
    "                                </div>\n" +
    "                            </div>\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "\n" +
    "                    <div class=\"row\">\n" +
    "\n" +
    "                        <!-- Date of Birth -->\n" +
    "                        <div class=\"col-xs-12 col-sm-6\">\n" +
    "                            <ui-editable-field\n" +
    "                                data-label=\"Date of Birth\"\n" +
    "                                data-key=\"dob\"\n" +
    "                                data-type=\"date\">\n" +
    "                            </ui-editable-field>\n" +
    "                        </div>\n" +
    "\n" +
    "                        <!-- Gender -->\n" +
    "                        <div class=\"col-xs-12 col-sm-6\">\n" +
    "                            <ui-editable-field\n" +
    "                                data-label=\"Gender\"\n" +
    "                                data-key=\"gender\"\n" +
    "                                data-type=\"gender\">\n" +
    "                            </ui-editable-field>\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "\n" +
    "                    <div class=\"row\">\n" +
    "\n" +
    "                        <!-- Phone -->\n" +
    "                        <div class=\"col-xs-12 col-sm-6\">\n" +
    "                            <ui-editable-field\n" +
    "                                data-label=\"Phone #\"\n" +
    "                                data-key=\"phone\"\n" +
    "                                data-empty=\"(xxx) xxx-xxxx\">\n" +
    "                            </ui-editable-field>\n" +
    "                        </div>\n" +
    "\n" +
    "                        <!-- Email -->\n" +
    "                        <div class=\"col-xs-12 col-sm-6\">\n" +
    "                            <ui-editable-field\n" +
    "                                data-label=\"Email\"\n" +
    "                                data-key=\"email\"\n" +
    "                                data-empty=\"address@example.com\">\n" +
    "                            </ui-editable-field>\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "\n" +
    "                    <div class=\"row\">\n" +
    "\n" +
    "                        <!-- Primary Tag -->\n" +
    "                        <div class=\"col-xs-12 col-sm-6\">\n" +
    "                            <ui-editable-field\n" +
    "                                data-label=\"Primary Sport\"\n" +
    "                                data-key=\"mainTag\"\n" +
    "                                data-type=\"tag\"\n" +
    "                                data-empty=\"Select a Sport\">\n" +
    "                            </ui-editable-field>\n" +
    "                        </div>\n" +
    "\n" +
    "                        <!-- Secondary tags -->\n" +
    "                        <div class=\"col-xs-12 col-sm-6\">\n" +
    "                            <ui-editable-field\n" +
    "                                data-label=\"Other Sports\"\n" +
    "                                data-key=\"tags\"\n" +
    "                                data-type=\"tag\"\n" +
    "                                data-max-tags=\"10\"\n" +
    "                                data-empty=\"Select a Sport\">\n" +
    "                            </ui-editable-field>\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "\n" +
    "                    <!-- Profile creation date -->\n" +
    "                    <div class=\"row\">\n" +
    "                        <div class=\"col-xs-12\">\n" +
    "                            <ui-editable-field\n" +
    "                                data-label=\"Profile Created\"\n" +
    "                                data-key=\"createdAt\"\n" +
    "                                data-type=\"datetime\"\n" +
    "                                data-disabled=\"true\">\n" +
    "                            </ui-editable-field>\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "\n" +
    "                    <!-- Placeholder info (NFL TechCrunch demo) -->\n" +
    "                    <div ng-if=\"false\" class=\"row\">\n" +
    "\n" +
    "                        <!-- Experience -->\n" +
    "                        <div class=\"col-xs-12 col-md-6\">\n" +
    "                            <div class=\"row ui-editable-field ui-editable-field-vertical\">\n" +
    "                                <div class=\"col-xs-12\">\n" +
    "                                    <div class=\"ui-editable-field-value\">\n" +
    "                                        <a href=\"javascript:;\" class=\"edit-text\">\n" +
    "                                            {{ profile.id }} Seasons\n" +
    "                                        </a>\n" +
    "                                        <a href=\"javascript:;\" class=\"edit-icon fa fa-pencil\"></a>\n" +
    "                                    </div>\n" +
    "                                </div>\n" +
    "                                <div class=\"col-xs-12 ui-editable-field-label\">\n" +
    "                                    Experience\n" +
    "                                </div>\n" +
    "                            </div>\n" +
    "                        </div>\n" +
    "\n" +
    "                        <!-- Snap count -->\n" +
    "                        <div class=\"col-xs-12 col-md-6\">\n" +
    "                            <div class=\"row ui-editable-field ui-editable-field-vertical\">\n" +
    "                                <div class=\"col-xs-12\">\n" +
    "                                    <div class=\"ui-editable-field-value\">\n" +
    "                                        <a href=\"javascript:;\" class=\"edit-text\">\n" +
    "                                            {{ profile.id * 59 | number:0 }}\n" +
    "                                        </a>\n" +
    "                                        <a href=\"javascript:;\" class=\"edit-icon fa fa-pencil\"></a>\n" +
    "                                    </div>\n" +
    "                                </div>\n" +
    "                                <div class=\"col-xs-12 ui-editable-field-label\">\n" +
    "                                    Snap Count\n" +
    "                                </div>\n" +
    "                            </div>\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "                </ui-editable-fields>\n" +
    "            </div>\n" +
    "\n" +
    "            <!-- General notes -->\n" +
    "            <div class=\"col-xs-12 col-lg-4\">\n" +
    "        		<div class=\"col-xs-12\" style=\"margin-bottom: 15px\">\n" +
    "                    <ui-editable-standalone-field\n" +
    "                        data-heading=\"Medical Information\"\n" +
    "                        data-model=\"profile.meta.data\"\n" +
    "                        data-key=\"medicalHistory\"\n" +
    "                        data-empty=\"Medical information:\"\n" +
    "                        data-save=\"saveProfileDetails\"\n" +
    "                        data-save-callback=\"saveProfileDetailsCallback\">\n" +
    "                    </ui-editable-standalone-field>\n" +
    "                </div>\n" +
    "\n" +
    "                <div class=\"col-xs-12\" style=\"margin-bottom: 15px\">\n" +
    "                    <ui-editable-standalone-field\n" +
    "                        data-heading=\"Previous Injuries\"\n" +
    "                        data-model=\"profile.meta.data\"\n" +
    "                        data-key=\"injuries\"\n" +
    "                        data-empty=\"Injuries:\"\n" +
    "                        data-save=\"saveProfileDetails\"\n" +
    "                        data-save-callback=\"saveProfileDetailsCallback\">\n" +
    "                    </ui-editable-standalone-field>\n" +
    "\n" +
    "                    <!-- Placeholder for previous injuries (NFL TechCrunch demo) -->\n" +
    "                    <div ng-if=\"false\" class=\"ui-editable-list-container\">\n" +
    "                        <h3 class=\"title\">\n" +
    "                            Previous Injuries\n" +
    "                            <span></span>\n" +
    "                        </h3>\n" +
    "                        <br>\n" +
    "\n" +
    "                        <div ng-if=\"profile.lastName == 'Todd'\">\n" +
    "                            Rotator cuff tear on Nov 17, 2015\n" +
    "                            <br>\n" +
    "                            (surgery Nov 21, 2015).\n" +
    "                            <br>\n" +
    "\n" +
    "                            <a href=\"#/demo/trends\" style=\"display: inline-block; margin: 10px 0 0 5px;\">\n" +
    "                                <i class=\"fa fa-angle-double-right fa-fw\"></i> Track Rehabilitation\n" +
    "                            </a>\n" +
    "                        </div>\n" +
    "\n" +
    "                        <div ng-if=\"profile.lastName != 'Todd'\">\n" +
    "                            No previous injuries.\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "                </div>\n" +
    "\n" +
    "                <div class=\"col-xs-12\" style=\"margin-bottom: 15px\">\n" +
    "                    <ui-editable-standalone-field\n" +
    "                        data-heading=\"Other Notes\"\n" +
    "                        data-model=\"profile.meta.data\"\n" +
    "                        data-key=\"notes\"\n" +
    "                        data-empty=\"Notes:\"\n" +
    "                        data-save=\"saveProfileDetails\"\n" +
    "                        data-save-callback=\"saveProfileDetailsCallback\">\n" +
    "                    </ui-editable-standalone-field>\n" +
    "        		</div>\n" +
    "        	</div>\n" +
    "        </div>\n" +
    "\n" +
    "        <!-- Demo: training sesion (NFL TechCrunch demo) -->\n" +
    "        <div ng-if=\"false\">\n" +
    "            <div ng-controller=\"DemoTrainingSessionController\" id=\"trendsDemo\">\n" +
    "                <ng-include src=\"'profile/partials/demo-session.html'\"></ng-include>\n" +
    "            </div>\n" +
    "        </div>\n" +
    "\n" +
    "        <!-- Performance indicators for single movements -->\n" +
    "        <div ng-controller=\"TestController\">\n" +
    "            <ng-include src=\"'profile/partials/movement-indicators.html'\"></ng-include>\n" +
    "        </div>\n" +
    "\n" +
    "        <!-- Performance indicators for movement screenings -->\n" +
    "        <div ng-controller=\"TestController\">\n" +
    "            <ng-include src=\"'profile/partials/screening-indicators.html'\"></ng-include>\n" +
    "        </div>\n" +
    "    </section>\n" +
    "</div>\n" +
    "\n" +
    "<!-- No profile selected -->\n" +
    "<div ng-if=\"profile.id === 0 && !global.data.isFetchingProfiles\" ng-include=\"'partials/select-profile.html'\" class=\"page\"></div>\n" +
    "\n" +
    "<!-- Loading profiles -->\n" +
    "<div ng-if=\"global.data.isFetchingProfiles\" class=\"page\">\n" +
    "    <ng-include src=\"'partials/loading.html'\"></ng-include>\n" +
    "</div>\n" +
    "");
}]);

angular.module("screenings/index.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("screenings/index.html",
    "<!-- Copyright Heddoko(TM) 2015, all rights reserved. -->\n" +
    "\n" +
    "<div class=\"page\">\n" +
    "    <header>\n" +
    "        <h1>\n" +
    "            Movement Tests <i class=\"fa fa-pencil-square-o\"></i>\n" +
    "            <span></span>\n" +
    "        </h1>\n" +
    "    </header>\n" +
    "\n" +
    "    <!-- List of screenings -->\n" +
    "    <div ng-if=\"!global.data.isFetchingScreeningData\" class=\"table-responsive\">\n" +
    "        <table ng-if=\"global.data.screening.length > 0\" class=\"table table-striped\">\n" +
    "\n" +
    "            <!-- Heading -->\n" +
    "            <thead>\n" +
    "                <tr>\n" +
    "                    <td class=\"text-center\">Title</td>\n" +
    "                    <td class=\"text-center\">Date</td>\n" +
    "                    <td class=\"text-center\">Athlete</td>\n" +
    "                    <td class=\"text-center\">Score</td>\n" +
    "                </tr>\n" +
    "            </thead>\n" +
    "\n" +
    "            <!-- Screenings -->\n" +
    "            <tbody>\n" +
    "                <tr ng-repeat=\"screening in global.data.screening track by screening.id\" ng-if=\"screening.id\">\n" +
    "                    <td class=\"text-center\">\n" +
    "                        <a href=\"#/screenings/{{ screening.id }}\">\n" +
    "                            {{ screening.title | characters:60 }}\n" +
    "                        </a>\n" +
    "\n" +
    "                        <a\n" +
    "                            ng-if=\"!screening.meta.isComplete\"\n" +
    "                            ng-click=\"editScreening(screening)\"\n" +
    "                            href=\"javascript:;\"\n" +
    "                            style=\"margin-left: 10px\">\n" +
    "\n" +
    "                            <i class=\"fa fa-pencil-square-o\"></i>\n" +
    "                        </a>\n" +
    "                    </td>\n" +
    "                    <td class=\"text-center\">\n" +
    "                        {{ screening.createdAt | mysqlDate }}\n" +
    "                    </td>\n" +
    "                    <td class=\"text-center\">\n" +
    "                        <a href=\"#/profiles/{{ screening.profile.id }}\">\n" +
    "                            {{ screening.profile.firstName + ' ' + screening.profile.lastName | characters:40 }}\n" +
    "                        </a>\n" +
    "                    </td>\n" +
    "                    <td class=\"text-center\">\n" +
    "                        {{ screening.score | number:0 }}\n" +
    "                    </td>\n" +
    "                </tr>\n" +
    "            </tbody>\n" +
    "        </table>\n" +
    "\n" +
    "        <!-- No screenings to show -->\n" +
    "        <div ng-if=\"global.data.screening.length === 0\" class=\"text-center\">\n" +
    "\n" +
    "            <a href=\"#/screenings/current\" class=\"btn btn-primary btn-circle btn-lg\">\n" +
    "                <i class=\"fa fa-plus\"></i>\n" +
    "            </a>\n" +
    "\n" +
    "            <h3>There are no tests to display.</h3>\n" +
    "            Once you create a new test, it will show up here.\n" +
    "            <br><br>\n" +
    "\n" +
    "            <b>Go ahead and use the <i class=\"fa fa-plus fa-lg fa-fw\"></i> button above to start.</b>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "\n" +
    "    <!-- Loading data -->\n" +
    "    <div ng-if=\"global.data.isFetchingScreeningData\" class=\"loading-notice\">\n" +
    "        <ng-include src=\"'partials/loading.html'\"></ng-include>\n" +
    "    </div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("screenings/live/index.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("screenings/live/index.html",
    "<!-- Copyright Heddoko(TM) 2015, all rights reserved. -->\n" +
    "\n" +
    "<div class=\"page screening-page\">\n" +
    "    <header>\n" +
    "        <h1>\n" +
    "            Live Test <i class=\"fa fa-list-alt\"></i>\n" +
    "            <span></span>\n" +
    "        </h1>\n" +
    "\n" +
    "        <!-- Append profile name to page title -->\n" +
    "        <div ng-if=\"screening.id > 0\" class=\"h1-append\">\n" +
    "            <a ng-click=\"resetScreening()\" href=\"javascript:;\">\n" +
    "                (save and close)\n" +
    "            </a>\n" +
    "        </div>\n" +
    "    </header>\n" +
    "\n" +
    "    <div ng-if=\"!global.data.isFetchingLiveScreening && screening.id > 0\" class=\"page-padded\">\n" +
    "\n" +
    "        <!-- Container -->\n" +
    "        <div class=\"row no-gutter screening-container\">\n" +
    "\n" +
    "            <!-- Test title -->\n" +
    "            <div class=\"row\">\n" +
    "                <div class=\"col-lg-8\">\n" +
    "                    <h3 class=\"title\">\n" +
    "                        <ui-editable-standalone-field\n" +
    "                            data-model=\"screening\"\n" +
    "                            data-key=\"title\"\n" +
    "                            data-empty=\"Title\"\n" +
    "                            data-input-type=\"text\"\n" +
    "                            data-save=\"saveScreening\"\n" +
    "                            data-save-callback=\"saveScreeningCallback\">\n" +
    "                        </ui-editable-standalone-field>\n" +
    "\n" +
    "                        <span></span>\n" +
    "                    </h3>\n" +
    "                </div>\n" +
    "            </div>\n" +
    "            <br>\n" +
    "\n" +
    "            <!-- Movement views -->\n" +
    "            <div class=\"col-lg-9\">\n" +
    "\n" +
    "                <div class=\"row no-gutter\">\n" +
    "                    <div class=\"col-lg-5 screening-example-view\">\n" +
    "                        <ui-movement-playback\n" +
    "                            data-title=\"screeningMovement.title\"\n" +
    "                            data-title-align=\"center\"\n" +
    "                            data-height=\"'600px'\"\n" +
    "                            data-previous-movement=\"previousMovement\"\n" +
    "                            data-next-movement=\"nextMovement\">\n" +
    "                        </ui-movement-playback>\n" +
    "                    </div>\n" +
    "\n" +
    "                    <div class=\"col-lg-7 screening-live-view\">\n" +
    "                        <ui-movement-playback\n" +
    "                            data-title=\"'Live View'\"\n" +
    "                            data-height=\"'600px'\"\n" +
    "                            data-previous-movement=\"false\"\n" +
    "                            data-next-movement=\"false\">\n" +
    "                        </ui-movement-playback>\n" +
    "                    </div>\n" +
    "                </div>\n" +
    "\n" +
    "                <div class=\"row screening-meta\">\n" +
    "\n" +
    "                    <!-- Profile details -->\n" +
    "                    <div class=\"col-lg-5\">\n" +
    "                        <div class=\"row no-gutter\">\n" +
    "                            <div class=\"col-md-5\">\n" +
    "                                <a\n" +
    "                                    href=\"#/profiles/{{ profile.id }}\"\n" +
    "                                    class=\"aspect-ratio aspect-4-3 card profile-avatar\"\n" +
    "                                    style=\"background-image: url({{ profile.avatarSrc || '' }})\">\n" +
    "                                </a>\n" +
    "                            </div>\n" +
    "\n" +
    "                            <div class=\"col-md-7\">\n" +
    "                                <div class=\"profile-details\">\n" +
    "                                    <div>\n" +
    "                                        <a href=\"#/profiles/{{ profile.id }}\">\n" +
    "                                            {{ profile.lastName.toUpperCase() }}\n" +
    "                                        </a>\n" +
    "                                    </div>\n" +
    "\n" +
    "                                    <!-- Main group -->\n" +
    "                                    <div ng-if=\"profile.groups.length > 0\">\n" +
    "                                        <a href=\"#/groups/{{ profile.groups[0].id }}\">\n" +
    "                                            {{ profile.groups[0].name }}\n" +
    "                                        </a>\n" +
    "                                    </div>\n" +
    "\n" +
    "                                    <!-- Test date -->\n" +
    "                                    <div>\n" +
    "                                        Tested on: {{ screening.createdAt | mysqlDate:'d / M / yyyy' }}\n" +
    "                                    </div>\n" +
    "                                </div>\n" +
    "                            </div>\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "\n" +
    "                    <div class=\"col-lg-7 screening-details\">\n" +
    "\n" +
    "                        <!-- Screening title edit field -->\n" +
    "                        <div class=\"row\">\n" +
    "                            <div class=\"col-sm-12\">\n" +
    "                                <ui-editable-standalone-field\n" +
    "                                    data-model=\"screeningMovement\"\n" +
    "                                    data-key=\"title\"\n" +
    "                                    data-empty=\"Title\"\n" +
    "                                    data-input-type=\"text\"\n" +
    "                                    data-save=\"saveScreeningMovement\"\n" +
    "                                    data-save-callback=\"saveScreeningMovementCallback\">\n" +
    "                                </ui-editable-standalone-field>\n" +
    "                            </div>\n" +
    "                        </div>\n" +
    "\n" +
    "                        <!-- Scoring, Saving -->\n" +
    "                        <div class=\"row\" style=\"margin-top: 10px; margin-bottom: 10px\">\n" +
    "                            <div class=\"col-sm-12\">\n" +
    "\n" +
    "                                <!-- Latest movements (demo) -->\n" +
    "                                <div class=\"screening-latest-movement-demo\"></div>\n" +
    "\n" +
    "                                <div class=\"screeening-scoring-container\">\n" +
    "\n" +
    "                                    <!-- Pain indicator -->\n" +
    "                                    <a\n" +
    "                                        href=\"javascript:;\"\n" +
    "                                        class=\"btn-score text-danger\">\n" +
    "\n" +
    "                                        <i class=\"fa fa-dot-circle-o\"></i>\n" +
    "                                        <span>Pain</span>\n" +
    "                                    </a>\n" +
    "\n" +
    "                                    <!-- Scoring -->\n" +
    "                                    <a\n" +
    "                                        ng-repeat=\"score in [0, 1, 2, 3]\"\n" +
    "                                        ng-click=\"setScore(screeningMovement, score)\"\n" +
    "                                        href=\"javascript:;\"\n" +
    "                                        class=\"btn-score\">\n" +
    "\n" +
    "                                        <i\n" +
    "                                            ng-if=\"screeningMovement.meta.score != score\"\n" +
    "                                            class=\"fa fa-circle-o\"></i>\n" +
    "                                        <i\n" +
    "                                            ng-if=\"screeningMovement.meta.score == score\"\n" +
    "                                            class=\"fa fa-dot-circle-o\"></i>\n" +
    "\n" +
    "                                        <span>{{ score }}</span>\n" +
    "                                    </a>\n" +
    "\n" +
    "                                    <!-- Actions -->\n" +
    "                                    <a\n" +
    "                                        ng-if=\"!global.data.isRecordingLiveScreening\"\n" +
    "                                        ng-click=\"startLiveRecording()\"\n" +
    "                                        href=\"javascript:;\"\n" +
    "                                        class=\"btn-flat btn-danger\">\n" +
    "\n" +
    "                                        <i class=\"fa fa-video-camera\"></i>\n" +
    "                                        <span>Record</span>\n" +
    "                                    </a>\n" +
    "                                    <a\n" +
    "                                        ng-if=\"global.data.isRecordingLiveScreening\"\n" +
    "                                        ng-click=\"pauseLiveRecording()\"\n" +
    "                                        href=\"javascript:;\"\n" +
    "                                        class=\"btn-flat btn-danger\">\n" +
    "\n" +
    "                                        <i class=\"fa fa-pause\"></i>\n" +
    "                                        <span>Pause</span>\n" +
    "                                    </a>\n" +
    "                                    <a\n" +
    "                                        ng-if=\"!global.data.isSavingLiveScreeningScore\"\n" +
    "                                        ng-click=\"saveScreeningScore()\"\n" +
    "                                        href=\"javascript:;\"\n" +
    "                                        class=\"btn-flat btn-default\">\n" +
    "\n" +
    "                                        <i class=\"fa fa-floppy-o\"></i>\n" +
    "                                        <span>Save</span>\n" +
    "                                    </a>\n" +
    "                                    <a\n" +
    "                                        ng-if=\"global.data.isSavingLiveScreeningScore\"\n" +
    "                                        href=\"javascript:;\"\n" +
    "                                        class=\"btn-flat btn-default\">\n" +
    "\n" +
    "                                        <i class=\"fa fa-spinner fa-spin\"></i>\n" +
    "                                        <span>Saving</span>\n" +
    "                                    </a>\n" +
    "                                    <a\n" +
    "                                        href=\"javascript:;\"\n" +
    "                                        class=\"btn-flat btn-default\">\n" +
    "\n" +
    "                                        <i class=\"fa fa-trash\"></i>\n" +
    "                                        <span>Delete</span>\n" +
    "                                    </a>\n" +
    "                                </div>\n" +
    "                            </div>\n" +
    "                        </div>\n" +
    "\n" +
    "                        <!-- Screening notes edit field -->\n" +
    "                        <div class=\"row\">\n" +
    "                            <div class=\"col-sm-12\">\n" +
    "                                <ui-editable-standalone-field\n" +
    "                                    data-model=\"screeningMovement.meta\"\n" +
    "                                    data-key=\"notes\"\n" +
    "                                    data-empty=\"Notes:\"\n" +
    "                                    data-input-type=\"textarea\"\n" +
    "                                    data-save=\"saveScreeningMovement\"\n" +
    "                                    data-save-callback=\"saveScreeningMovementCallback\">\n" +
    "                                </ui-editable-standalone-field>\n" +
    "                            </div>\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "                </div>\n" +
    "            </div>\n" +
    "\n" +
    "            <!-- Test list and progress -->\n" +
    "            <div class=\"col-lg-3\">\n" +
    "                <div class=\"col-sm-12\">\n" +
    "                    <div class=\"screening-progress\">\n" +
    "\n" +
    "                        <br>\n" +
    "                        <div class=\"row\">\n" +
    "                            <div class=\"col-sm-12\">\n" +
    "                                <div class=\"screening-progress-heading\">\n" +
    "                                    <div class=\"movement-title active\">Test</div>\n" +
    "                                    <div class=\"movement-score\">Score</div>\n" +
    "                                </div>\n" +
    "                            </div>\n" +
    "                        </div>\n" +
    "\n" +
    "                        <div class=\"row\">\n" +
    "                            <div class=\"col-sm-12\">\n" +
    "                                <div class=\"divider-dot\"></div>\n" +
    "                                <div class=\"divider-end\"></div>\n" +
    "                            </div>\n" +
    "                        </div>\n" +
    "\n" +
    "                        <div class=\"row\">\n" +
    "                            <div class=\"col-sm-12\">\n" +
    "                                <div\n" +
    "                                    ng-repeat=\"movement in screening.movements track by movement.id\"\n" +
    "                                    class=\"screening-progress-movement-row\">\n" +
    "\n" +
    "                                    <a\n" +
    "                                        ng-click=\"setScreeningMovement(movement)\"\n" +
    "                                        href=\"javascript:;\"\n" +
    "                                        class=\"movement-title\"\n" +
    "                                        ng-class=\"{active: movement.id == screeningMovement.id}\">\n" +
    "\n" +
    "                                        {{ movement.title }}\n" +
    "                                    </a>\n" +
    "\n" +
    "                                    <div class=\"movement-score\">\n" +
    "\n" +
    "                                    </div>\n" +
    "                                </div>\n" +
    "                            </div>\n" +
    "                        </div>\n" +
    "\n" +
    "                        <div class=\"row\">\n" +
    "                            <div class=\"col-sm-12\">\n" +
    "                                <div class=\"divider-end\"></div>\n" +
    "                                <div class=\"divider-dot\"></div>\n" +
    "                            </div>\n" +
    "                        </div>\n" +
    "\n" +
    "                        <!-- Score -->\n" +
    "                        <div class=\"row\">\n" +
    "                            <div class=\"col-sm-12\">\n" +
    "                            </div>\n" +
    "                        </div>\n" +
    "\n" +
    "                        <!-- Percent completion -->\n" +
    "                        <div class=\"row\">\n" +
    "                            <div class=\"col-sm-12\">\n" +
    "                            </div>\n" +
    "                        </div>\n" +
    "                    </div>\n" +
    "                </div>\n" +
    "            </div>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "\n" +
    "    <!-- Select a screening template -->\n" +
    "    <div ng-if=\"!global.data.isFetchingLiveScreening && screening.id === 0\">\n" +
    "\n" +
    "        <!-- Create a screening -->\n" +
    "        <div\n" +
    "            ng-show=\"global.getSelectedProfile().id > 0 && !global.data.isPreparingNewScreening\"\n" +
    "            class=\"text-center\">\n" +
    "\n" +
    "            <a\n" +
    "                ng-click=\"createFunctionalMovementScreening()\"\n" +
    "                href=\"javascript:;\"\n" +
    "                class=\"btn btn-primary btn-circle btn-lg\">\n" +
    "\n" +
    "                <i class=\"fa fa-plus\"></i>\n" +
    "            </a>\n" +
    "\n" +
    "            <h3>\n" +
    "                Start a movement test for\n" +
    "                <em>\n" +
    "                    {{ global.getSelectedProfile().firstName }}\n" +
    "                    {{ global.getSelectedProfile().lastName }}\n" +
    "                </em>.\n" +
    "            </h3>\n" +
    "            Go ahead and use the <i class=\"fa fa-plus fa-lg fa-fw\"></i> button above to start.\n" +
    "        </div>\n" +
    "\n" +
    "        <!-- Select a profile -->\n" +
    "        <div ng-show=\"global.getSelectedProfile().id === 0 && !global.data.isPreparingNewScreening\">\n" +
    "            <div ng-include=\"'partials/select-profile.html'\"></div>\n" +
    "        </div>\n" +
    "\n" +
    "        <!-- Preparing screening records -->\n" +
    "        <div ng-show=\"global.data.isPreparingNewScreening\" class=\"loading-notice\">\n" +
    "            <h2>Preparing your test.</h2>\n" +
    "            <i class=\"fa fa-spinner fa-spin fa-2x\"></i>\n" +
    "        </div>\n" +
    "    </div>\n" +
    "\n" +
    "    <!-- Loading screening data -->\n" +
    "    <div ng-if=\"global.data.isFetchingLiveScreening\">\n" +
    "        <ng-include src=\"'partials/loading.html'\"></ng-include>\n" +
    "    </div>\n" +
    "</div>\n" +
    "");
}]);

angular.module("screenings/view.html", []).run(["$templateCache", function($templateCache) {
  $templateCache.put("screenings/view.html",
    "<!-- Copyright Heddoko(TM) 2015, all rights reserved. -->\n" +
    "\n" +
    "<div class=\"page\">\n" +
    "    <header>\n" +
    "        <h1>\n" +
    "            <abbr title=\"Functional Movement Test\">FMT</abbr> Report <i class=\"fa fa-list-alt\"></i>\n" +
    "            <span></span>\n" +
    "        </h1>\n" +
    "\n" +
    "        <!-- Append profile name to page title -->\n" +
    "        <div ng-if=\"profile.id > 0\" class=\"h1-append\">\n" +
    "            {{ ::profile.firstName }} {{ ::profile.lastName }}\n" +
    "        </div>\n" +
    "    </header>\n" +
    "\n" +
    "    <!-- Screening results -->\n" +
    "    <div ng-if=\"!global.data.isFetchingSelectedScreening && screening.id > 0\" class=\"text-center\">\n" +
    "\n" +
    "        <button class=\"btn btn-default\">\n" +
    "            Test Score Table\n" +
    "        </button>\n" +
    "        <br>\n" +
    "        <br>\n" +
    "\n" +
    "        <button class=\"btn btn-default\">\n" +
    "            Test Score Graph\n" +
    "        </button>\n" +
    "        <br>\n" +
    "        <br>\n" +
    "\n" +
    "        <button class=\"btn btn-default\">\n" +
    "            Mobility Summary\n" +
    "        </button>\n" +
    "        <br>\n" +
    "        <br>\n" +
    "        <br>\n" +
    "\n" +
    "        <a href=\"javascript:;\">\n" +
    "            <i class=\"fa fa-angle-double-down fa-2x\"></i>\n" +
    "        </a>\n" +
    "    </div>\n" +
    "\n" +
    "    <div ng-if=\"screening.id === 0\">\n" +
    "        <h2 style=\"text-align: center; color: #ccc;\">\n" +
    "            An Error Occurred :(\n" +
    "\n" +
    "            <!-- TODO -->\n" +
    "        </h2>\n" +
    "    </div>\n" +
    "\n" +
    "    <div ng-if=\"global.data.isFetchingSelectedScreening\">\n" +
    "        <ng-include src=\"'partials/loading.html'\"></ng-include>\n" +
    "    </div>\n" +
    "</div>\n" +
    "");
}]);
