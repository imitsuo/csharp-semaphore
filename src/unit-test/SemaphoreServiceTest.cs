using System;
using api.Domain.Services;
using Xunit;

namespace unit_test
{
    public class SemaphoreServiceTest
    {
        private ISemaphoreService _service;

        public SemaphoreServiceTest()
        {
            Setup();
        }

        private void Setup()
        {
            _service = new SemaphoreService();
        }
        
        [Fact]
        public void Test_Lock__new_resource__Expected_lock()
        {
            // FIXTURES
            Setup();
            var resource = "da39a3ee5e6b4b0d3255bfef95601890afd80709";

            // EXERCISE
            var result = _service.Lock(resource);

            // ASSERTS
            Assert.True(result);
        }

        [Fact]
        public void Test_Lock__already_locked_resource__Expected_not_lock()
        {
            // FIXTURES
            Setup();
            var resource = "da39a3ee5e6b4b0d3255bfef95601890afd80709";
            _service.Lock(resource);

            // EXERCISE
            var result = _service.Lock(resource);

            // ASSERTS
            Assert.False(result);
        }

        [Fact]
        public void Test_Release__locked_resource__Expected_release()
        {
            // FIXTURES
            Setup();
            var resource = "da39a3ee5e6b4b0d3255bfef95601890afd80709";
            _service.Lock(resource);

            // EXERCISE
            var result = _service.Release(resource);

            // ASSERTS
            Assert.True(result);
        }

                [Fact]
        public void Test_Release__not_locked_resource__Expected_not_release()
        {
            // FIXTURES
            Setup();
            var resource = "da39a3ee5e6b4b0d3255bfef95601890afd80709";

            // EXERCISE
            var result = _service.Release(resource);

            // ASSERTS
            Assert.False(result);
        }
    }
}
