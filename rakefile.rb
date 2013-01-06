require 'albacore'
require 'nokogiri'

desc "Deploy"
task :deploy => [:build, :test] do
	endpoints = [
		{:endpoint => 'Contact.Commands.ApproveAccLead', :mappings => []},
		{:endpoint => 'Contact.Commands.CreateAccommodationLead', :mappings => []},
		{:endpoint => 'Contact.Commands.CreateAccSupplier', :mappings => [
			{:messages=>'Contact.Messages.Commands.ApproveAccLead, Contact.Messages',:endpoint=>'Contact.Commands.ApproveAccLead'},
			{:messages=>'Contact.Messages.Commands.CreateAccommodationLead, Contact.Messages',:endpoint=>'Contact.Commands.CreateAccommodationLead'},
			{:messages=>'Contact.Messages.Commands.CreateAccSupplier, Contact.Messages',:endpoint=>'Contact.Commands.CreateAccSupplier'},
			{:messages=>'Contact.Messages.Commands.CreateAuthenticationWithGeneratedPassword, Contact.Messages',:endpoint=>'Contact.Commands.CreateAuthenticationWithGeneratedPassword'},
			{:messages=>'Contact.Messages.Commands.CreateUser, Contact.Messages',:endpoint=>'Contact.Commands.CreateUser'},
			{:messages=>'Contact.Messages.Events.AccommodationLeadApproved, Contact.Messages',:endpoint=>'Contact.Commands.ApproveAccLead'},
			{:messages=>'Contact.Messages.Events.AccommodationLeadCreated, Contact.Messages',:endpoint=>'Contact.Commands.CreateAccommodationLead'},
			{:messages=>'Contact.Messages.Events.AccommodationSupplierCreated, Contact.Messages',:endpoint=>'Contact.Commands.CreateAccSupplier'},
			{:messages=>'Contact.Messages.Events.AuthenticationCreated, Contact.Messages',:endpoint=>'Contact.Commands.CreateAuthenticationWithGeneratedPassword'},
			{:messages=>'Contact.Messages.Events.UserCreated, Contact.Messages',:endpoint=>'Contact.Commands.CreateUser'}
		]},
		{:endpoint => 'Contact.Commands.CreateAuthenticationWithGeneratedPassword', :mappings =>[]},
		{:endpoint => 'Contact.Commands.CreateUser', :mappings => []}
	]
	
    src_path = File.join("src/contact/Contact/bin/Release/.")
	endpoints.each { |x|
		directory = "deploy/"+x[:endpoint].downcase
		filename = File.join(directory,"Contact.dll.config")
		FileUtils.rm_rf(directory)
		FileUtils.mkdir_p(directory)
		dest_path = File.join(directory)
		FileUtils.cp_r src_path, dest_path
	
		appConfig = Nokogiri::XML(File.open(filename))
		endpointMappings = appConfig.search('configuration/UnicastBusConfig/MessageEndpointMappings').first
		endpointMappings.children.remove
		x[:mappings].each { |mapping|
			endpointMappingElement = appConfig.create_element('add')
			endpointMappingElement["Messages"] = mapping[:messages] 
			endpointMappingElement["Endpoint"] = mapping[:endpoint]
			endpointMappings.add_child(endpointMappingElement)
		}
		File.open(filename,'w') {|f| appConfig.write_xml_to f}
	}
end

desc "Build"
msbuild :build do |msb|
  msb.properties = { :configuration => :Release }
  msb.targets = [ :Clean, :Build ]
  msb.solution = "Solution.sln"
end

desc "Test"
nunit :test => :build do |nunit|
  nunit.command = "nunit-console.exe"
  nunit.options "/framework v4.0.30319"
  nunit.assemblies FileList["tests/**/*/Release/*.UnitTests.dll", "tests/**/*/Release/*.IntegrationTests.dll"].exclude(/obj\//)
end




