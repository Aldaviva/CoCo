﻿using CoCo.Property;
using FluentAssertions;
using Xunit;

namespace Test
{
    public class TestStoredProperty
    {
        [Fact]
        public void InitialValueReturned()
        {
            var property = new StoredProperty<int>(4);
            property.Value.Should().Be(4);
        }

        [Fact]
        public void UpdatedValueReturned()
        {
            var property = new StoredProperty<string>("hello");
            property.Value.Should().Be("hello");
            property.Value = "world";
            property.Value.Should().Be("world");
        }

        [Fact]
        public void EventsTriggered()
        {
            var property = new StoredProperty<double>(8.0);
            property.Value.Should().Be(8.0);

            int eventsTriggeredCount = 0;
            property.PropertyChanged += (sender, args) =>
            {
                eventsTriggeredCount++;
                property.Value.Should().Be(9.0);
            };

            property.Value = 9.0;

            eventsTriggeredCount.Should().Be(1);
        }

        [Fact]
        public void SetUnchangedValueDoesNotTriggerEvents()
        {
            var property = new StoredProperty<double>(8.0);
            property.Value.Should().Be(8.0);

            int eventsTriggeredCount = 0;
            property.PropertyChanged += (sender, args) =>
            {
                eventsTriggeredCount++;
                property.Value.Should().Be(8.0);
            };

            property.Value = 8.0;

            eventsTriggeredCount.Should().Be(0);
        }
    }
}
