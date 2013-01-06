require 'albacore'

task :deploy do
    #src_path = File.join(BASE_PATH, "Output/bin/.")
    #dest_path = File.join(BASE_PATH, "Deploy")
    puts "Deploying files..."
    #FileUtils.cp_r src_path, dest_path
end

desc "Build"
msbuild :build do |msb|
  msb.properties = { :configuration => :Release }
  msb.targets = [ :Clean, :Build ]
  msb.solution = "NServiceBusMessagingExample.sln"
end

desc "Test"
nunit :test => :build do |nunit|
  nunit.command = "nunit-console.exe"
  nunit.options "/framework v4.0.30319"
  nunit.assemblies FileList["**/*/Debug/*.UnitTests.dll", "**/*/Debug/*.IntegrationTests.dll"].exclude(/obj\//)
end
