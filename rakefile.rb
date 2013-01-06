require 'albacore'

desc "Deploy"
task :deploy do
	endpoints = [
		'Contact.Commands.ApproveAccLead',
		'Contact.Commands.CreateAccommodationLead',
		'Contact.Commands.CreateAccSupplier',
		'Contact.Commands.CreateAuthenticationWithGeneratedPassword',
		'Contact.Commands.CreateUser'
	]
    src_path = File.join("src/contact/Contact/bin/Release/.")
	endpoints.each { |x|
		directory = "deploy/"+x.downcase
		FileUtils.rm_rf(directory)
		FileUtils.mkdir_p(directory)
		dest_path = File.join(directory)
		FileUtils.cp_r src_path, dest_path
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




